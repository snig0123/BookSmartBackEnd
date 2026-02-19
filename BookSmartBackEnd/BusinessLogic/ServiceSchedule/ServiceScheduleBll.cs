using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ServiceScheduleBll(BookSmartContext bookSmartContext) : IServiceScheduleBll
    {
        public void AddServiceToSchedule(PostServiceScheduleModel data)
        {
            _ = bookSmartContext.SERVICES
                .FirstOrDefault(s => s.SERVICE_ID == data.ServiceId && !s.SERVICE_DELETED)
                ?? throw new ArgumentException("Service not found.");

            _ = bookSmartContext.SCHEDULES
                .FirstOrDefault(s => s.SCHEDULE_ID == data.ScheduleId && !s.SCHEDULE_DELETED)
                ?? throw new ArgumentException("Schedule not found.");

            bool linkExists = bookSmartContext.SERVICESCHEDULES
                .Any(ss => ss.SERVICESCHEDULE_SERVICEID == data.ServiceId
                        && ss.SERVICESCHEDULE_SCHEDULEID == data.ScheduleId
                        && !ss.SERVICESCHEDULE_DELETED);

            if (linkExists)
                throw new ArgumentException("Service is already linked to this schedule.");

            ServiceSchedule link = new ServiceSchedule
            {
                SERVICESCHEDULE_ID = Guid.NewGuid(),
                SERVICESCHEDULE_SERVICEID = data.ServiceId,
                SERVICESCHEDULE_SCHEDULEID = data.ScheduleId,
                SERVICESCHEDULE_CREATED = DateTime.UtcNow,
                SERVICESCHEDULE_UPDATED = DateTime.UtcNow,
                SERVICESCHEDULE_DELETED = false
            };

            bookSmartContext.SERVICESCHEDULES.Add(link);
            bookSmartContext.SaveChanges();
        }

        public void RemoveServiceFromSchedule(Guid serviceId, Guid scheduleId)
        {
            ServiceSchedule link = bookSmartContext.SERVICESCHEDULES
                .FirstOrDefault(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId
                                   && ss.SERVICESCHEDULE_SCHEDULEID == scheduleId
                                   && !ss.SERVICESCHEDULE_DELETED)
                ?? throw new ArgumentException("Link between service and schedule not found.");

            bookSmartContext.SERVICESCHEDULES.Entry(link).State = EntityState.Modified;
            link.SERVICESCHEDULE_DELETED = true;
            link.SERVICESCHEDULE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        public List<ServiceResponse> GetServicesBySchedule(Guid scheduleId, bool excludeUnavailable = false)
        {
            List<Service> services = bookSmartContext.SERVICESCHEDULES
                .Where(ss => ss.SERVICESCHEDULE_SCHEDULEID == scheduleId && !ss.SERVICESCHEDULE_DELETED)
                .Include(ss => ss.SERVICESCHEDULE_SERVICE)
                .Where(ss => !ss.SERVICESCHEDULE_SERVICE.SERVICE_DELETED)
                .Select(ss => ss.SERVICESCHEDULE_SERVICE)
                .ToList();

            List<Guid> serviceIds = services.Select(s => s.SERVICE_ID).ToList();
            HashSet<Guid> availableIds = GetAvailableServiceIds(serviceIds);

            return services
                .Where(s => !excludeUnavailable || availableIds.Contains(s.SERVICE_ID))
                .Select(s => MapServiceToResponse(s, availableIds.Contains(s.SERVICE_ID)))
                .ToList();
        }

        public List<ScheduleResponse> GetSchedulesByService(Guid serviceId)
        {
            return bookSmartContext.SERVICESCHEDULES
                .Where(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId && !ss.SERVICESCHEDULE_DELETED)
                .Include(ss => ss.SERVICESCHEDULE_SCHEDULE)
                .Where(ss => !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED)
                .Select(ss => MapScheduleToResponse(ss.SERVICESCHEDULE_SCHEDULE))
                .ToList();
        }

        public void AddServiceToScheduleOverride(PostServiceScheduleOverrideModel data)
        {
            _ = bookSmartContext.SERVICES
                .FirstOrDefault(s => s.SERVICE_ID == data.ServiceId && !s.SERVICE_DELETED)
                ?? throw new ArgumentException("Service not found.");

            _ = bookSmartContext.SCHEDULEOVERRIDES
                .FirstOrDefault(so => so.SCHEDULEOVERRIDE_ID == data.ScheduleOverrideId && !so.SCHEDULEOVERRIDE_DELETED)
                ?? throw new ArgumentException("Schedule override not found.");

            bool linkExists = bookSmartContext.SERVICESCHEDULEOVERRIDES
                .Any(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == data.ServiceId
                          && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID == data.ScheduleOverrideId
                          && !sso.SERVICESCHEDULEOVERRIDE_DELETED);

            if (linkExists)
                throw new ArgumentException("Service is already linked to this schedule override.");

            ServiceScheduleOverride link = new ServiceScheduleOverride
            {
                SERVICESCHEDULEOVERRIDE_ID = Guid.NewGuid(),
                SERVICESCHEDULEOVERRIDE_SERVICEID = data.ServiceId,
                SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID = data.ScheduleOverrideId,
                SERVICESCHEDULEOVERRIDE_CREATED = DateTime.UtcNow,
                SERVICESCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow,
                SERVICESCHEDULEOVERRIDE_DELETED = false
            };

            bookSmartContext.SERVICESCHEDULEOVERRIDES.Add(link);
            bookSmartContext.SaveChanges();
        }

        public void RemoveServiceFromScheduleOverride(Guid serviceId, Guid scheduleOverrideId)
        {
            ServiceScheduleOverride link = bookSmartContext.SERVICESCHEDULEOVERRIDES
                .FirstOrDefault(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId
                                    && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID == scheduleOverrideId
                                    && !sso.SERVICESCHEDULEOVERRIDE_DELETED)
                ?? throw new ArgumentException("Link between service and schedule override not found.");

            bookSmartContext.SERVICESCHEDULEOVERRIDES.Entry(link).State = EntityState.Modified;
            link.SERVICESCHEDULEOVERRIDE_DELETED = true;
            link.SERVICESCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        public List<ServiceResponse> GetServicesByScheduleOverride(Guid scheduleOverrideId, bool excludeUnavailable = false)
        {
            List<Service> services = bookSmartContext.SERVICESCHEDULEOVERRIDES
                .Where(sso => sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID == scheduleOverrideId && !sso.SERVICESCHEDULEOVERRIDE_DELETED)
                .Include(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICE)
                .Where(sso => !sso.SERVICESCHEDULEOVERRIDE_SERVICE.SERVICE_DELETED)
                .Select(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICE)
                .ToList();

            List<Guid> serviceIds = services.Select(s => s.SERVICE_ID).ToList();
            HashSet<Guid> availableIds = GetAvailableServiceIds(serviceIds);

            return services
                .Where(s => !excludeUnavailable || availableIds.Contains(s.SERVICE_ID))
                .Select(s => MapServiceToResponse(s, availableIds.Contains(s.SERVICE_ID)))
                .ToList();
        }

        public List<ScheduleOverrideResponse> GetScheduleOverridesByService(Guid serviceId)
        {
            return bookSmartContext.SERVICESCHEDULEOVERRIDES
                .Where(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId && !sso.SERVICESCHEDULEOVERRIDE_DELETED)
                .Include(sso => sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE)
                .Where(sso => !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED)
                .Select(sso => MapScheduleOverrideToResponse(sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE))
                .ToList();
        }

        private HashSet<Guid> GetAvailableServiceIds(List<Guid> serviceIds)
        {
            HashSet<Guid> result = bookSmartContext.SERVICESCHEDULES
                .Where(ss => serviceIds.Contains(ss.SERVICESCHEDULE_SERVICEID)
                          && !ss.SERVICESCHEDULE_DELETED
                          && !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED
                          && ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_ACTIVE)
                .Select(ss => ss.SERVICESCHEDULE_SERVICEID)
                .ToHashSet();

            result.UnionWith(bookSmartContext.SERVICESCHEDULEOVERRIDES
                .Where(sso => serviceIds.Contains(sso.SERVICESCHEDULEOVERRIDE_SERVICEID)
                           && !sso.SERVICESCHEDULEOVERRIDE_DELETED
                           && !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED
                           && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_ISAVAILABLE)
                .Select(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID)
                .ToHashSet());

            return result;
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
