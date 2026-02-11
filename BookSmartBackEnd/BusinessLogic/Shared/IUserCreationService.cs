using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IUserCreationService
    {
        void CreateUser(PostRegisterModel data, Guid roleTypeId);
    }
}
