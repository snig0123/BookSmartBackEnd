using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerBll _workerBll;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(IWorkerBll workerBll, ILogger<WorkerController> logger)
        {
            _workerBll = workerBll;
            _logger = logger;
        }

        [HttpPost(Name = "Create")]
        public ActionResult Create(PostRegisterModel data)
        {
            //_userBLL.RegisterUser(data);
            return Ok();
        }
    }
}