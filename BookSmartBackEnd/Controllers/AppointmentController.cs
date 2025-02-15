using BookSmartBackEnd.BusinessLogic;
using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers;

public class AppointmentController : Controller
{
    private readonly IAppointmentBll _appointmentBll;
    public AppointmentController(IAppointmentBll appointmentBll)
    {
        _appointmentBll = appointmentBll;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/Appointment/Create")]
    public ActionResult Create(PostAppointmentModel model)
    {

        return null;
        //return new CreatedResult(Request.GetEncodedUrl() + "/" + createdAppointment.ID);
    }
}