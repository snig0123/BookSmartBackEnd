using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IClientBll
    {
        void CreateClient(PostRegisterModel data);
        List<ClientResponse> GetAllClients();
        ClientResponse GetClientById(Guid clientId);
        void UpdateClient(Guid clientId, PostUpdateClientModel data);
        void DeleteClient(Guid clientId);
    }
}
