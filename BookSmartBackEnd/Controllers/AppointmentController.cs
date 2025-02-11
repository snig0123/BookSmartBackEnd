using BookSmartBackEnd.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers;

public class AppointmentController : Controller
{
    // GET
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