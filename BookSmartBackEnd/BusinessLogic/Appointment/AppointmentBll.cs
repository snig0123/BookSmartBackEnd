using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Constants;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class AppointmentBll(
        IAppointmentRepository appointmentRepository,
        IServiceRepository serviceRepository,
        IScheduleRepository scheduleRepository,
        IScheduleOverrideRepository scheduleOverrideRepository) : IAppointmentBll
    {
        public void CreateAppointment(Guid clientUserId, PostAppointmentModel data)
        {
            CreateAppointmentInternal(
                clientUserId,
                data.ServiceId,
                data.ScheduleId,
                data.ScheduleOverrideId,
                data.RequestedStartTime,
                data.Comment);
        }

        public void CreateAppointmentForClient(PostAppointmentForClientModel data)
        {
            CreateAppointmentInternal(
                data.ClientUserId,
                data.ServiceId,
                data.ScheduleId,
                data.ScheduleOverrideId,
                data.RequestedStartTime,
                data.Comment);
        }

        public List<AppointmentResponse> GetMyAppointments(Guid clientUserId)
        {
            return appointmentRepository.GetByClient(clientUserId)
                .Select(MapToResponse)
                .ToList();
        }

        public List<AppointmentResponse> GetAppointmentsByStaff(Guid staffUserId)
        {
            return appointmentRepository.GetByStaff(staffUserId)
                .Select(MapToResponse)
                .ToList();
        }

        public void CancelAppointment(Guid clientUserId, Guid appointmentId)
        {
            Appointment appointment = appointmentRepository.GetById(appointmentId)
                ?? throw new ArgumentException("Appointment not found.");

            if (appointment.APPOINTMENT_CLIENTUSERID != clientUserId)
                throw new UnauthorizedAccessException("You can only cancel your own appointments.");

            if (appointment.APPOINTMENT_STATUS == AppointmentStatuses.Cancelled)
                throw new InvalidOperationException("Appointment is already cancelled.");

            appointment.APPOINTMENT_STATUS = AppointmentStatuses.Cancelled;
            appointment.APPOINTMENT_UPDATED = DateTime.UtcNow;
            appointmentRepository.Update(appointment);
        }

        public void UpdateAppointmentStatus(Guid appointmentId, string status)
        {
            Appointment appointment = appointmentRepository.GetById(appointmentId)
                ?? throw new ArgumentException("Appointment not found.");

            appointment.APPOINTMENT_STATUS = status;
            appointment.APPOINTMENT_UPDATED = DateTime.UtcNow;
            appointmentRepository.Update(appointment);
        }

        private void CreateAppointmentInternal(
            Guid clientUserId,
            Guid serviceId,
            Guid? scheduleId,
            Guid? scheduleOverrideId,
            DateTime requestedStartTime,
            string? comment)
        {
            ValidateSlotSelection(scheduleId, scheduleOverrideId);

            Service service = serviceRepository.GetById(serviceId)
                ?? throw new ArgumentException("Service not found.");

            if (!service.SERVICE_ACTIVE)
                throw new InvalidOperationException("Service is not active.");

            Guid staffUserId;

            if (scheduleId.HasValue)
            {
                Schedule schedule = scheduleRepository.GetById(scheduleId.Value)
                    ?? throw new ArgumentException("Schedule not found.");

                ValidateScheduleSlot(schedule, requestedStartTime, service.SERVICE_DURATION);
                staffUserId = schedule.SCHEDULE_USERID;
            }
            else
            {
                ScheduleOverride scheduleOverride = scheduleOverrideRepository.GetById(scheduleOverrideId!.Value)
                    ?? throw new ArgumentException("Schedule override not found.");

                ValidateScheduleOverrideSlot(scheduleOverride, requestedStartTime, service.SERVICE_DURATION);
                staffUserId = scheduleOverride.SCHEDULEOVERRIDE_USERID;
            }

            int bookedCount = appointmentRepository.CountActiveForSlot(serviceId, scheduleId, scheduleOverrideId, requestedStartTime);
            if (bookedCount >= service.SERVICE_CAPACITY)
                throw new InvalidOperationException("Service is at capacity for this slot.");

            appointmentRepository.Add(new Appointment
            {
                APPOINTMENT_ID = Guid.NewGuid(),
                APPOINTMENT_CLIENTUSERID = clientUserId,
                APPOINTMENT_STAFFUSERID = staffUserId,
                APPOINTMENT_SERVICEID = serviceId,
                APPOINTMENT_SCHEDULEID = scheduleId,
                APPOINTMENT_SCHEDULEOVERRIDEID = scheduleOverrideId,
                APPOINTMENT_STARTDATETIME = requestedStartTime,
                APPOINTMENT_ENDDATETIME = requestedStartTime.AddMinutes(service.SERVICE_DURATION),
                APPOINTMENT_STATUS = AppointmentStatuses.Pending,
                APPOINTMENT_COMMENT = comment,
                APPOINTMENT_CREATED = DateTime.UtcNow,
                APPOINTMENT_UPDATED = DateTime.UtcNow,
                APPOINTMENT_DELETED = false
            });
        }

        private static void ValidateSlotSelection(Guid? scheduleId, Guid? scheduleOverrideId)
        {
            if (!scheduleId.HasValue && !scheduleOverrideId.HasValue)
                throw new ArgumentException("Either a ScheduleId or ScheduleOverrideId must be provided.");

            if (scheduleId.HasValue && scheduleOverrideId.HasValue)
                throw new ArgumentException("Only one of ScheduleId or ScheduleOverrideId may be provided.");
        }

        private static void ValidateScheduleSlot(Schedule schedule, DateTime requestedStart, int durationMinutes)
        {
            if (schedule.SCHEDULE_DELETED || !schedule.SCHEDULE_ACTIVE)
                throw new InvalidOperationException("Schedule is not active.");

            if ((int)requestedStart.DayOfWeek != schedule.SCHEDULE_DAYOFWEEK)
                throw new ArgumentException("Requested start time does not fall on the schedule's day of week.");

            TimeOnly requestedTime = TimeOnly.FromDateTime(requestedStart);
            TimeOnly requestedEnd = requestedTime.AddMinutes(durationMinutes);

            if (requestedTime < schedule.SCHEDULE_STARTTIME || requestedEnd > schedule.SCHEDULE_ENDTIME)
                throw new ArgumentException("Requested time slot falls outside the schedule window.");
        }

        private static void ValidateScheduleOverrideSlot(ScheduleOverride scheduleOverride, DateTime requestedStart, int durationMinutes)
        {
            if (scheduleOverride.SCHEDULEOVERRIDE_DELETED)
                throw new InvalidOperationException("Schedule override not found.");

            if (!scheduleOverride.SCHEDULEOVERRIDE_ISAVAILABLE)
                throw new InvalidOperationException("This schedule override marks the staff as unavailable.");

            if (DateOnly.FromDateTime(requestedStart) != scheduleOverride.SCHEDULEOVERRIDE_DATE)
                throw new ArgumentException("Requested start time does not match the schedule override date.");

            if (scheduleOverride.SCHEDULEOVERRIDE_STARTTIME.HasValue && scheduleOverride.SCHEDULEOVERRIDE_ENDTIME.HasValue)
            {
                TimeOnly requestedTime = TimeOnly.FromDateTime(requestedStart);
                TimeOnly requestedEnd = requestedTime.AddMinutes(durationMinutes);

                if (requestedTime < scheduleOverride.SCHEDULEOVERRIDE_STARTTIME.Value ||
                    requestedEnd > scheduleOverride.SCHEDULEOVERRIDE_ENDTIME.Value)
                    throw new ArgumentException("Requested time slot falls outside the schedule override window.");
            }
        }

        private static AppointmentResponse MapToResponse(Appointment appointment)
        {
            return new AppointmentResponse
            {
                AppointmentId = appointment.APPOINTMENT_ID,
                ClientUserId = appointment.APPOINTMENT_CLIENTUSERID,
                ClientName = appointment.APPOINTMENT_CLIENTUSER.USER_FORENAME + " " + appointment.APPOINTMENT_CLIENTUSER.USER_SURNAME,
                StaffUserId = appointment.APPOINTMENT_STAFFUSERID,
                ServiceId = appointment.APPOINTMENT_SERVICEID,
                ServiceName = appointment.APPOINTMENT_SERVICE.SERVICE_NAME,
                ScheduleId = appointment.APPOINTMENT_SCHEDULEID,
                ScheduleOverrideId = appointment.APPOINTMENT_SCHEDULEOVERRIDEID,
                StartDateTime = appointment.APPOINTMENT_STARTDATETIME,
                EndDateTime = appointment.APPOINTMENT_ENDDATETIME,
                Status = appointment.APPOINTMENT_STATUS,
                Comment = appointment.APPOINTMENT_COMMENT,
                Created = appointment.APPOINTMENT_CREATED
            };
        }
    }
}
