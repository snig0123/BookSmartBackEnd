using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class ScheduleOverrideRepository(BookSmartContext context) : IScheduleOverrideRepository
{
    public ScheduleOverride? GetById(Guid scheduleOverrideId)
    {
        return context.SCHEDULEOVERRIDES
            .FirstOrDefault(s => s.SCHEDULEOVERRIDE_ID == scheduleOverrideId && !s.SCHEDULEOVERRIDE_DELETED);
    }

    public List<ScheduleOverride> GetByStaff(Guid staffUserId)
    {
        return context.SCHEDULEOVERRIDES
            .Where(s => s.SCHEDULEOVERRIDE_USERID == staffUserId && !s.SCHEDULEOVERRIDE_DELETED)
            .ToList();
    }

    public void Add(ScheduleOverride scheduleOverride)
    {
        context.SCHEDULEOVERRIDES.Add(scheduleOverride);
        context.SaveChanges();
    }

    public void Update(ScheduleOverride scheduleOverride)
    {
        context.Entry(scheduleOverride).State = EntityState.Modified;
        context.SaveChanges();
    }
}
