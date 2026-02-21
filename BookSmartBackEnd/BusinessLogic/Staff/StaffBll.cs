using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEndDatabase.Constants;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class StaffBll(IUserCreationService userCreationService) : IStaffBll
    {
        public void RegisterUser(PostRegisterModel data)
        {
            userCreationService.CreateUser(data, RoleTypes.STAFF);
        }
    }
}
