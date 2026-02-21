using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IScheduleRepository
{
    Schedule? GetById(Guid scheduleId);
    List<Schedule> GetByStaff(Guid staffUserId);
    void Add(Schedule schedule);
    void AddRange(IEnumerable<Schedule> schedules);
    void Update(Schedule schedule);
}
