using BookSmartBackEnd.Models;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IUserBll
    {
        void RegisterUser(PostRegisterModel data);
        string LoginUser(string email, string password);
    }
}
