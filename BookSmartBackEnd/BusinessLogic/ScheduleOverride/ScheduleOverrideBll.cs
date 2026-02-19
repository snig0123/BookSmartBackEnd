using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Constants;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ScheduleOverrideBll(BookSmartContext bookSmartContext) : IScheduleOverrideBll
    {
        public void CreateScheduleOverride(PostScheduleOverrideModel data)
        {
            User user = bookSmartContext.USERS
                .Include(u => u.USER_ROLES)
                .FirstOrDefault(u => u.USER_ID == data.UserId)
                ?? throw new ArgumentException("User not found.");

            bool isStaff = user.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.STAFF);
            if (!isStaff)
            {
                throw new ArgumentException("User does not have the Staff role.");
            }

            ScheduleOverride scheduleOverride = new ScheduleOverride
            {
                SCHEDULEOVERRIDE_ID = Guid.NewGuid(),
                SCHEDULEOVERRIDE_USERID = data.UserId,
                SCHEDULEOVERRIDE_DATE = data.Date,
                SCHEDULEOVERRIDE_STARTTIME = data.StartTime,
                SCHEDULEOVERRIDE_ENDTIME = data.EndTime,
                SCHEDULEOVERRIDE_ISAVAILABLE = data.IsAvailable,
                SCHEDULEOVERRIDE_CREATED = DateTime.UtcNow,
                SCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow,
                SCHEDULEOVERRIDE_DELETED = false
            };

            bookSmartContext.SCHEDULEOVERRIDES.Add(scheduleOverride);
            bookSmartContext.SaveChanges();
        }

        public ScheduleOverrideResponse? GetScheduleOverride(Guid scheduleOverrideId)
        {
            ScheduleOverride? scheduleOverride = bookSmartContext.SCHEDULEOVERRIDES
                .FirstOrDefault(s => s.SCHEDULEOVERRIDE_ID == scheduleOverrideId && !s.SCHEDULEOVERRIDE_DELETED);

            if (scheduleOverride == null) return null;

            return MapToResponse(scheduleOverride);
        }

        public List<ScheduleOverrideResponse> GetScheduleOverridesByStaff(Guid staffUserId)
        {
            return bookSmartContext.SCHEDULEOVERRIDES
                .Where(s => s.SCHEDULEOVERRIDE_USERID == staffUserId && !s.SCHEDULEOVERRIDE_DELETED)
                .Select(s => MapToResponse(s))
                .ToList();
        }

        public void UpdateScheduleOverride(Guid scheduleOverrideId, PostScheduleOverrideModel data)
        {
            ScheduleOverride scheduleOverride = bookSmartContext.SCHEDULEOVERRIDES
                .FirstOrDefault(s => s.SCHEDULEOVERRIDE_ID == scheduleOverrideId && !s.SCHEDULEOVERRIDE_DELETED)
                ?? throw new ArgumentException("Schedule override not found.");

            bookSmartContext.SCHEDULEOVERRIDES.Entry(scheduleOverride).State = EntityState.Modified;

            scheduleOverride.SCHEDULEOVERRIDE_DATE = data.Date;
            scheduleOverride.SCHEDULEOVERRIDE_STARTTIME = data.StartTime;
            scheduleOverride.SCHEDULEOVERRIDE_ENDTIME = data.EndTime;
            scheduleOverride.SCHEDULEOVERRIDE_ISAVAILABLE = data.IsAvailable;
            scheduleOverride.SCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        public void DeleteScheduleOverride(Guid scheduleOverrideId)
        {
            ScheduleOverride scheduleOverride = bookSmartContext.SCHEDULEOVERRIDES
                .FirstOrDefault(s => s.SCHEDULEOVERRIDE_ID == scheduleOverrideId && !s.SCHEDULEOVERRIDE_DELETED)
                ?? throw new ArgumentException("Schedule override not found.");

            bookSmartContext.SCHEDULEOVERRIDES.Entry(scheduleOverride).State = EntityState.Modified;

            scheduleOverride.SCHEDULEOVERRIDE_DELETED = true;
            scheduleOverride.SCHEDULEOVERRIDE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        private static ScheduleOverrideResponse MapToResponse(ScheduleOverride scheduleOverride)
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
