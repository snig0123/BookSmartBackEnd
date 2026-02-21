using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class ServiceScheduleRepository(BookSmartContext context) : IServiceScheduleRepository
{
    public bool LinkExists(Guid serviceId, Guid scheduleId)
    {
        return context.SERVICESCHEDULES
            .Any(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId
                    && ss.SERVICESCHEDULE_SCHEDULEID == scheduleId
                    && !ss.SERVICESCHEDULE_DELETED);
    }

    public void AddLink(ServiceSchedule link)
    {
        context.SERVICESCHEDULES.Add(link);
        context.SaveChanges();
    }

    public ServiceSchedule? GetLink(Guid serviceId, Guid scheduleId)
    {
        return context.SERVICESCHEDULES
            .FirstOrDefault(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId
                               && ss.SERVICESCHEDULE_SCHEDULEID == scheduleId
                               && !ss.SERVICESCHEDULE_DELETED);
    }

    public void UpdateLink(ServiceSchedule link)
    {
        context.Entry(link).State = EntityState.Modified;
        context.SaveChanges();
    }

    public List<Service> GetServicesBySchedule(Guid scheduleId)
    {
        return context.SERVICESCHEDULES
            .Where(ss => ss.SERVICESCHEDULE_SCHEDULEID == scheduleId && !ss.SERVICESCHEDULE_DELETED)
            .Include(ss => ss.SERVICESCHEDULE_SERVICE)
            .Where(ss => !ss.SERVICESCHEDULE_SERVICE.SERVICE_DELETED)
            .Select(ss => ss.SERVICESCHEDULE_SERVICE)
            .ToList();
    }

    public List<Schedule> GetSchedulesByService(Guid serviceId)
    {
        return context.SERVICESCHEDULES
            .Where(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId && !ss.SERVICESCHEDULE_DELETED)
            .Include(ss => ss.SERVICESCHEDULE_SCHEDULE)
            .Where(ss => !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED)
            .Select(ss => ss.SERVICESCHEDULE_SCHEDULE)
            .ToList();
    }

    public bool OverrideLinkExists(Guid serviceId, Guid scheduleOverrideId)
    {
        return context.SERVICESCHEDULEOVERRIDES
            .Any(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId
                      && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID == scheduleOverrideId
                      && !sso.SERVICESCHEDULEOVERRIDE_DELETED);
    }

    public void AddOverrideLink(ServiceScheduleOverride link)
    {
        context.SERVICESCHEDULEOVERRIDES.Add(link);
        context.SaveChanges();
    }

    public ServiceScheduleOverride? GetOverrideLink(Guid serviceId, Guid scheduleOverrideId)
    {
        return context.SERVICESCHEDULEOVERRIDES
            .FirstOrDefault(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId
                                && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID == scheduleOverrideId
                                && !sso.SERVICESCHEDULEOVERRIDE_DELETED);
    }

    public void UpdateOverrideLink(ServiceScheduleOverride link)
    {
        context.Entry(link).State = EntityState.Modified;
        context.SaveChanges();
    }

    public List<Service> GetServicesByScheduleOverride(Guid scheduleOverrideId)
    {
        return context.SERVICESCHEDULEOVERRIDES
            .Where(sso => sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID == scheduleOverrideId && !sso.SERVICESCHEDULEOVERRIDE_DELETED)
            .Include(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICE)
            .Where(sso => !sso.SERVICESCHEDULEOVERRIDE_SERVICE.SERVICE_DELETED)
            .Select(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICE)
            .ToList();
    }

    public List<ScheduleOverride> GetScheduleOverridesByService(Guid serviceId)
    {
        return context.SERVICESCHEDULEOVERRIDES
            .Where(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId && !sso.SERVICESCHEDULEOVERRIDE_DELETED)
            .Include(sso => sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE)
            .Where(sso => !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED)
            .Select(sso => sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE)
            .ToList();
    }
}
