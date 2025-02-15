using BookSmartBackEnd.Models;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IWorkerBll
    {
        void RegisterUser(PostRegisterModel data);
        string LoginUser(string email, string password);
    }
}
