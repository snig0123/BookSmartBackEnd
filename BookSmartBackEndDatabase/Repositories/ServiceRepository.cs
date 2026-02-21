using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class ServiceRepository(BookSmartContext context) : IServiceRepository
{
    public Service? GetById(Guid serviceId)
    {
        return context.SERVICES
            .FirstOrDefault(s => s.SERVICE_ID == serviceId && !s.SERVICE_DELETED);
    }

    public List<Service> GetByBusiness(Guid businessId)
    {
        return context.SERVICES
            .Where(s => s.SERVICE_BUSINESSID == businessId && !s.SERVICE_DELETED)
            .ToList();
    }

    public void Add(Service service)
    {
        context.SERVICES.Add(service);
        context.SaveChanges();
    }

    public void Update(Service service)
    {
        context.Entry(service).State = EntityState.Modified;
        context.SaveChanges();
    }

    public bool IsAvailable(Guid serviceId)
    {
        return context.SERVICESCHEDULES
            .Any(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId
                    && !ss.SERVICESCHEDULE_DELETED
                    && !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED
                    && ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_ACTIVE)
            ||
            context.SERVICESCHEDULEOVERRIDES
            .Any(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId
                     && !sso.SERVICESCHEDULEOVERRIDE_DELETED
                     && !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED
                     && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_ISAVAILABLE);
    }

    public HashSet<Guid> GetAvailableIds(List<Guid> serviceIds)
    {
        HashSet<Guid> result = context.SERVICESCHEDULES
            .Where(ss => serviceIds.Contains(ss.SERVICESCHEDULE_SERVICEID)
                      && !ss.SERVICESCHEDULE_DELETED
                      && !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED
                      && ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_ACTIVE)
            .Select(ss => ss.SERVICESCHEDULE_SERVICEID)
            .ToHashSet();

        result.UnionWith(context.SERVICESCHEDULEOVERRIDES
            .Where(sso => serviceIds.Contains(sso.SERVICESCHEDULEOVERRIDE_SERVICEID)
                       && !sso.SERVICESCHEDULEOVERRIDE_DELETED
                       && !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED
                       && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_ISAVAILABLE)
            .Select(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID)
            .ToHashSet());

        return result;
    }
}
