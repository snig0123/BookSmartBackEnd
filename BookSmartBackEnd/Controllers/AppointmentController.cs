using BookSmartBackEnd.BusinessLogic;
using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AppointmentController(IAppointmentBll appointmentBll) : ControllerBase
{
    [HttpPost("/Appointment/Create")]
    public ActionResult Create(PostAppointmentModel model)
    {
        appointmentBll.CreateAppointment(model);
        return Created();
        //return new CreatedResult(Request.GetEncodedUrl() + "/" + createdAppointment.ID);
    }
}