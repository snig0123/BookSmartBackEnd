using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IScheduleOverrideRepository
{
    ScheduleOverride? GetById(Guid scheduleOverrideId);
    List<ScheduleOverride> GetByStaff(Guid staffUserId);
    void Add(ScheduleOverride scheduleOverride);
    void Update(ScheduleOverride scheduleOverride);
}
