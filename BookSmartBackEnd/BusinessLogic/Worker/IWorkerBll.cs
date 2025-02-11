using BookSmartBackEnd.Models;

namespace BookSmartBackEnd.BusinessLogic.User.Interfaces
{
    public interface IWorkerBll
    {
        void RegisterUser(PostRegisterModel data);
        string LoginUser(string email, string password);
    }
}
