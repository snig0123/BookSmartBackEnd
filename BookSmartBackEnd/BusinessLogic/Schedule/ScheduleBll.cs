using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Constants;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ScheduleBll(BookSmartContext bookSmartContext) : IScheduleBll
    {
        public void CreateSchedule(PostScheduleModel data)
        {
            User user = bookSmartContext.USERS
                .Include(u => u.USER_ROLES)
                .FirstOrDefault(u => u.USER_ID == data.StaffUserId)
                ?? throw new ArgumentException("User not found.");

            bool isStaff = user.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.STAFF);
            if (!isStaff)
            {
                throw new ArgumentException("User does not have the Staff role.");
            }

            Schedule schedule = new Schedule
            {
                SCHEDULE_ID = Guid.NewGuid(),
                SCHEDULE_USERID = data.StaffUserId,
                SCHEDULE_DAYOFWEEK = data.DayOfWeek,
                SCHEDULE_STARTTIME = data.StartTime,
                SCHEDULE_ENDTIME = data.EndTime,
                SCHEDULE_ACTIVE = true,
                SCHEDULE_CREATED = DateTime.UtcNow,
                SCHEDULE_UPDATED = DateTime.UtcNow,
                SCHEDULE_DELETED = false
            };

            bookSmartContext.SCHEDULES.Add(schedule);
            bookSmartContext.SaveChanges();
        }

        public void CreateBulkSchedules(List<PostScheduleModel> data)
        {
            if (data.Count == 0)
            {
                throw new ArgumentException("No schedules provided.");
            }

            Guid userId = data[0].StaffUserId;
            if (data.Any(d => d.StaffUserId != userId))
            {
                throw new ArgumentException("All schedules must belong to the same user.");
            }

            User user = bookSmartContext.USERS
                .Include(u => u.USER_ROLES)
                .FirstOrDefault(u => u.USER_ID == userId)
                ?? throw new ArgumentException("User not found.");

            bool isStaff = user.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.STAFF);
            if (!isStaff)
            {
                throw new ArgumentException("User does not have the Staff role.");
            }

            DateTime now = DateTime.UtcNow;

            foreach (PostScheduleModel entry in data)
            {
                Schedule schedule = new Schedule
                {
                    SCHEDULE_ID = Guid.NewGuid(),
                    SCHEDULE_USERID = userId,
                    SCHEDULE_DAYOFWEEK = entry.DayOfWeek,
                    SCHEDULE_STARTTIME = entry.StartTime,
                    SCHEDULE_ENDTIME = entry.EndTime,
                    SCHEDULE_ACTIVE = true,
                    SCHEDULE_CREATED = now,
                    SCHEDULE_UPDATED = now,
                    SCHEDULE_DELETED = false
                };

                bookSmartContext.SCHEDULES.Add(schedule);
            }

            bookSmartContext.SaveChanges();
        }

        public ScheduleResponse? GetSchedule(Guid scheduleId)
        {
            Schedule? schedule = bookSmartContext.SCHEDULES
                .FirstOrDefault(s => s.SCHEDULE_ID == scheduleId && !s.SCHEDULE_DELETED);

            if (schedule == null) return null;

            return MapToResponse(schedule);
        }

        public List<ScheduleResponse> GetSchedulesByStaff(Guid staffUserId)
        {
            return bookSmartContext.SCHEDULES
                .Where(s => s.SCHEDULE_USERID == staffUserId && !s.SCHEDULE_DELETED)
                .Select(s => MapToResponse(s))
                .ToList();
        }

        public void UpdateSchedule(Guid scheduleId, PostScheduleModel data)
        {
            Schedule schedule = bookSmartContext.SCHEDULES
                .FirstOrDefault(s => s.SCHEDULE_ID == scheduleId && !s.SCHEDULE_DELETED)
                ?? throw new ArgumentException("Schedule not found.");

            bookSmartContext.SCHEDULES.Entry(schedule).State = EntityState.Modified;

            schedule.SCHEDULE_DAYOFWEEK = data.DayOfWeek;
            schedule.SCHEDULE_STARTTIME = data.StartTime;
            schedule.SCHEDULE_ENDTIME = data.EndTime;
            schedule.SCHEDULE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        public void DeleteSchedule(Guid scheduleId)
        {
            Schedule schedule = bookSmartContext.SCHEDULES
                .FirstOrDefault(s => s.SCHEDULE_ID == scheduleId && !s.SCHEDULE_DELETED)
                ?? throw new ArgumentException("Schedule not found.");

            bookSmartContext.SCHEDULES.Entry(schedule).State = EntityState.Modified;

            schedule.SCHEDULE_DELETED = true;
            schedule.SCHEDULE_ACTIVE = false;
            schedule.SCHEDULE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        private static ScheduleResponse MapToResponse(Schedule schedule)
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
    }
}