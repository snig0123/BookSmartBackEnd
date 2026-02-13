using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IUserBll
    {
        void RegisterUser(PostRegisterModel data);
        string LoginUser(string email, string password);
        UserProfile? GetUserProfile(Guid userId);
    }
}
