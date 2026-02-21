using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IServiceScheduleRepository
{
    bool LinkExists(Guid serviceId, Guid scheduleId);
    void AddLink(ServiceSchedule link);
    ServiceSchedule? GetLink(Guid serviceId, Guid scheduleId);
    void UpdateLink(ServiceSchedule link);
    List<Service> GetServicesBySchedule(Guid scheduleId);
    List<Schedule> GetSchedulesByService(Guid serviceId);

    bool OverrideLinkExists(Guid serviceId, Guid scheduleOverrideId);
    void AddOverrideLink(ServiceScheduleOverride link);
    ServiceScheduleOverride? GetOverrideLink(Guid serviceId, Guid scheduleOverrideId);
    void UpdateOverrideLink(ServiceScheduleOverride link);
    List<Service> GetServicesByScheduleOverride(Guid scheduleOverrideId);
    List<ScheduleOverride> GetScheduleOverridesByService(Guid serviceId);
}
