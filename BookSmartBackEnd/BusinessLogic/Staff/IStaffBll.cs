using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IStaffBll
    {
        void RegisterUser(PostRegisterModel data);
    }
}
