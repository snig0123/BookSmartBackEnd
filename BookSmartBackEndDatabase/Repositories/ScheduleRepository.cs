using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class ScheduleRepository(BookSmartContext context) : IScheduleRepository
{
    public Schedule? GetById(Guid scheduleId)
    {
        return context.SCHEDULES
            .FirstOrDefault(s => s.SCHEDULE_ID == scheduleId && !s.SCHEDULE_DELETED);
    }

    public List<Schedule> GetByStaff(Guid staffUserId)
    {
        return context.SCHEDULES
            .Where(s => s.SCHEDULE_USERID == staffUserId && !s.SCHEDULE_DELETED)
            .ToList();
    }

    public void Add(Schedule schedule)
    {
        context.SCHEDULES.Add(schedule);
        context.SaveChanges();
    }

    public void AddRange(IEnumerable<Schedule> schedules)
    {
        context.SCHEDULES.AddRange(schedules);
        context.SaveChanges();
    }

    public void Update(Schedule schedule)
    {
        context.Entry(schedule).State = EntityState.Modified;
        context.SaveChanges();
    }
}
