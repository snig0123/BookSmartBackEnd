using BookSmartBackEnd.Models;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IUserBll
    {
        void RegisterUser(PostRegisterModel data);
        string LoginUser(string email, string password);
    }
}
