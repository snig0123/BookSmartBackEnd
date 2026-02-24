using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Constants;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ClientBll(IUserCreationService userCreationService, IClientRepository clientRepository) : IClientBll
    {
        public void CreateClient(PostRegisterModel data)
        {
            userCreationService.CreateUser(data, RoleTypes.CLIENT);
        }

        public List<ClientResponse> GetAllClients()
        {
            return clientRepository.GetAll()
                .Select(MapToResponse)
                .ToList();
        }

        public ClientResponse GetClientById(Guid clientId)
        {
            User client = clientRepository.GetById(clientId);
            return MapToResponse(client);
        }

        public void UpdateClient(Guid clientId, PostUpdateClientModel data)
        {
            User client = clientRepository.GetById(clientId);
            client.USER_FORENAME = data.FORENAME;
            client.USER_SURNAME = data.SURNAME;
            client.USER_EMAIL = data.EMAIL;
            client.USER_TELEPHONE = data.TELEPHONE;
            client.USER_UPDATED = DateTime.UtcNow;
            clientRepository.Update(client);
        }

        public void DeleteClient(Guid clientId)
        {
            User client = clientRepository.GetById(clientId);
            clientRepository.SoftDelete(client);
        }

        private static ClientResponse MapToResponse(User user) => new()
        {
            ClientId = user.USER_ID,
            Forename = user.USER_FORENAME,
            Surname = user.USER_SURNAME,
            Email = user.USER_EMAIL,
            Telephone = user.USER_TELEPHONE
        };
    }
}
