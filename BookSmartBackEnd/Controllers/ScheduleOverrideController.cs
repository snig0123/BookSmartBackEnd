using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ScheduleOverrideController(IScheduleOverrideBll scheduleOverrideBll, ILogger<ScheduleOverrideController> logger) : ControllerBase
    {
        [Authorize(Policy = "Staff")]
        [HttpPost(Name = "CreateScheduleOverride")]
        public ActionResult CreateScheduleOverride(PostScheduleOverrideModel data)
        {
            scheduleOverrideBll.CreateScheduleOverride(data);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetScheduleOverride")]
        public ActionResult<ScheduleOverrideResponse> GetScheduleOverride(Guid scheduleOverrideId)
        {
            ScheduleOverrideResponse? scheduleOverride = scheduleOverrideBll.GetScheduleOverride(scheduleOverrideId);

            if (scheduleOverride == null) return NotFound();

            return Ok(scheduleOverride);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetScheduleOverridesByStaff")]
        public ActionResult<List<ScheduleOverrideResponse>> GetScheduleOverridesByStaff(Guid staffUserId)
        {
            List<ScheduleOverrideResponse> overrides = scheduleOverrideBll.GetScheduleOverridesByStaff(staffUserId);
            return Ok(overrides);
        }

        [Authorize(Policy = "Staff")]
        [HttpPut(Name = "UpdateScheduleOverride")]
        public ActionResult UpdateScheduleOverride(Guid scheduleOverrideId, PostScheduleOverrideModel data)
        {
            scheduleOverrideBll.UpdateScheduleOverride(scheduleOverrideId, data);
            return Ok();
        }

        [Authorize(Policy = "Staff")]
        [HttpDelete(Name = "DeleteScheduleOverride")]
        public ActionResult DeleteScheduleOverride(Guid scheduleOverrideId)
        {
            scheduleOverrideBll.DeleteScheduleOverride(scheduleOverrideId);
            return Ok();
        }
    }
}
