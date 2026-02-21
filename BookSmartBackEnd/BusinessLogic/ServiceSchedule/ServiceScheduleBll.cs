using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ServiceScheduleBll(
        IServiceRepository serviceRepository,
        IScheduleRepository scheduleRepository,
        IScheduleOverrideRepository scheduleOverrideRepository,
        IServiceScheduleRepository serviceScheduleRepository) : IServiceScheduleBll
    {
        public void AddServiceToSchedule(PostServiceScheduleModel data)
        {
            _ = serviceRepository.GetById(data.ServiceId)
                ?? throw new ArgumentException("Service not found.");

            _ = scheduleRepository.GetById(data.ScheduleId)
                ?? throw new ArgumentException("Schedule not found.");

            if (serviceScheduleRepository.LinkExists(data.ServiceId, data.ScheduleId))
                throw new ArgumentException("Service is already linked to this schedule.");

            serviceScheduleRepository.AddLink(new ServiceSchedule
            {
                SERVICESCHEDULE_ID = Guid.NewGuid(),
                SERVICESCHEDULE_SERVICEID = data.ServiceId,
                SERVICESCHEDULE_SCHEDULEID = data.ScheduleId,
                SERVICESCHEDULE_CREATED = DateTime.UtcNow,
                SERVICESCHEDULE_UPDATED = DateTime.UtcNow,
                SERVICESCHEDULE_DELETED = false
            });
        }

        public void RemoveServiceFromSchedule(Guid serviceId, Guid scheduleId)
        {
            ServiceSchedule link = serviceScheduleRepository.GetLink(serviceId, scheduleId)
                ?? throw new ArgumentException("Link between service and schedule not found.");

            link.SERVICESCHEDULE_DELETED = true;
            link.SERVICESCHEDULE_UPDATED = DateTime.UtcNow;

            serviceScheduleRepository.UpdateLink(link);
        }

        public List<ServiceResponse> GetServicesBySchedule(Guid scheduleId, bool excludeUnavailable = false)
        {
            List<Service> services = serviceScheduleRepository.GetServicesBySchedule(scheduleId);

            List<Guid> serviceIds = services.Select(s => s.SERVICE_ID).ToList();
            HashSet<Guid> availableIds = serviceRepository.GetAvailableIds(serviceIds);

            return services
                .Where(s => !excludeUnavailable || availableIds.Contains(s.SERVICE_ID))
                .Select(s => MapServiceToResponse(s, availableIds.Contains(s.SERVICE_ID)))
                .ToList();
        }

        public List<ScheduleResponse> GetSchedulesByService(Guid serviceId)
        {
            return serviceScheduleRepository.GetSchedulesByService(serviceId)
                .Select(s => MapScheduleToResponse(s))
                .ToList();
        }

        public void AddServiceToScheduleOverride(PostServiceScheduleOverrideModel data)
        {
            _ = serviceRepository.GetById(data.ServiceId)
                ?? throw new ArgumentException("Service not found.");

            _ = scheduleOverrideRepository.GetById(data.ScheduleOverrideId)
                ?? throw new ArgumentException("Schedule override not found.");

            if (serviceScheduleRepository.OverrideLinkExists(data.ServiceId, data.ScheduleOverrideId))
                throw new ArgumentException("Service is already linked to this schedule override.");

            serviceScheduleRepository.AddOverrideLink(new ServiceScheduleOverride
            {
                SERVICESCHEDULEOVERRIDE_ID = Guid.NewGuid(),
                SERVICESCHEDULEOVERRIDE_SERVICEID = data.ServiceId,
                SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID = data.ScheduleOverrideId,
                SERVICESCHEDULEOVERRIDE_CREATED = DateTime.UtcNow,
                SERVICESCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow,
                SERVICESCHEDULEOVERRIDE_DELETED = false
            });
        }

        public void RemoveServiceFromScheduleOverride(Guid serviceId, Guid scheduleOverrideId)
        {
            ServiceScheduleOverride link = serviceScheduleRepository.GetOverrideLink(serviceId, scheduleOverrideId)
                ?? throw new ArgumentException("Link between service and schedule override not found.");

            link.SERVICESCHEDULEOVERRIDE_DELETED = true;
            link.SERVICESCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow;

            serviceScheduleRepository.UpdateOverrideLink(link);
        }

        public List<ServiceResponse> GetServicesByScheduleOverride(Guid scheduleOverrideId, bool excludeUnavailable = false)
        {
            List<Service> services = serviceScheduleRepository.GetServicesByScheduleOverride(scheduleOverrideId);

            List<Guid> serviceIds = services.Select(s => s.SERVICE_ID).ToList();
            HashSet<Guid> availableIds = serviceRepository.GetAvailableIds(serviceIds);

            return services
                .Where(s => !excludeUnavailable || availableIds.Contains(s.SERVICE_ID))
                .Select(s => MapServiceToResponse(s, availableIds.Contains(s.SERVICE_ID)))
                .ToList();
        }

        public List<ScheduleOverrideResponse> GetScheduleOverridesByService(Guid serviceId)
        {
            return serviceScheduleRepository.GetScheduleOverridesByService(serviceId)
                .Select(s => MapScheduleOverrideToResponse(s))
                .ToList();
        }

        private static ServiceResponse MapServiceToResponse(Service service, bool isAvailable)
        {
            return new ServiceResponse
            {
                ServiceId = service.SERVICE_ID,
                Name = service.SERVICE_NAME,
                Description = service.SERVICE_DESCRIPTION,
                Duration = service.SERVICE_DURATION,
                Price = service.SERVICE_PRICE,
                Capacity = service.SERVICE_CAPACITY,
                Active = service.SERVICE_ACTIVE,
                IsAvailable = isAvailable
            };
        }

        private static ScheduleResponse MapScheduleToResponse(Schedule schedule)
        {
            return new ScheduleResponse
            {
                ScheduleId = schedule.SCHEDULE_ID,
                UserId = schedule.SCHEDULE_USERID,
                DayOfWeek = schedule.SCHEDULE_DAYOFWEEK,
                StartTime = schedule.SCHEDULE_STARTTIME,
                EndTime = schedule.SCHEDULE_ENDTIME,
                Active = schedule.SCHEDULE_ACTIVE
            };
        }

        private static ScheduleOverrideResponse MapScheduleOverrideToResponse(ScheduleOverride scheduleOverride)
        {
            return new ScheduleOverrideResponse
            {
                ScheduleOverrideId = scheduleOverride.SCHEDULEOVERRIDE_ID,
                UserId = scheduleOverride.SCHEDULEOVERRIDE_USERID,
                Date = scheduleOverride.SCHEDULEOVERRIDE_DATE,
                IsAvailable = scheduleOverride.SCHEDULEOVERRIDE_ISAVAILABLE,
                StartTime = scheduleOverride.SCHEDULEOVERRIDE_STARTTIME,
                EndTime = scheduleOverride.SCHEDULEOVERRIDE_ENDTIME
            };
        }
    }
}
