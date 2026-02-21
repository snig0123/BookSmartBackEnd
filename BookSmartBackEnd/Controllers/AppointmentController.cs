using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSmartBackEnd.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AppointmentController(IAppointmentBll appointmentBll) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public ActionResult Book(PostAppointmentModel model)
    {
        Guid clientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        appointmentBll.CreateAppointment(clientUserId, model);
        return Created();
    }

    [HttpPost]
    [Authorize(Policy = "Staff")]
    public ActionResult BookForClient(PostAppointmentForClientModel model)
    {
        appointmentBll.CreateAppointmentForClient(model);
        return Created();
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<AppointmentResponse>> GetMyAppointments()
    {
        Guid clientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(appointmentBll.GetMyAppointments(clientUserId));
    }

    [HttpGet]
    [Authorize(Policy = "Staff")]
    public ActionResult<List<AppointmentResponse>> GetAppointmentsByStaff(Guid staffUserId)
    {
        return Ok(appointmentBll.GetAppointmentsByStaff(staffUserId));
    }

    [HttpPut]
    [Authorize]
    public ActionResult Cancel(Guid appointmentId)
    {
        Guid clientUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        appointmentBll.CancelAppointment(clientUserId, appointmentId);
        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = "Staff")]
    public ActionResult UpdateStatus(Guid appointmentId, PostUpdateAppointmentStatusModel model)
    {
        appointmentBll.UpdateAppointmentStatus(appointmentId, model.Status);
        return Ok();
    }
}
