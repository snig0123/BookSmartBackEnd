using BookSmartBackEnd.Models;

namespace BookSmartBackEnd.BusinessLogic.User.Interfaces
{
    public interface IUserBll
    {
        void RegisterUser(PostRegisterModel data);
        string LoginUser(string email, string password);
    }
}
