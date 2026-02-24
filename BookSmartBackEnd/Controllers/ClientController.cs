using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSmartBackEnd.Controllers;

[ApiController]
[Authorize(Policy = "Staff")]
[Route("[controller]/[action]")]
public class ClientController(IClientBll clientBll) : ControllerBase
{
    [HttpPost]
    public ActionResult Create(PostRegisterModel data)
    {
        clientBll.CreateClient(data);
        return Created();
    }

    [HttpGet]
    public ActionResult<List<ClientResponse>> GetAll()
    {
        return Ok(clientBll.GetAllClients());
    }

    [HttpGet]
    public ActionResult<ClientResponse> GetById(Guid clientId)
    {
        return Ok(clientBll.GetClientById(clientId));
    }

    [HttpPut]
    public ActionResult Update(Guid clientId, PostUpdateClientModel data)
    {
        clientBll.UpdateClient(clientId, data);
        return Ok();
    }

    [HttpDelete]
    public ActionResult Delete(Guid clientId)
    {
        clientBll.DeleteClient(clientId);
        return Ok();
    }
}
