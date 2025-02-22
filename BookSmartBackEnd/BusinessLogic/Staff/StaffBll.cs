using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class StaffBll(BookSmartContext bookSmartContext) : IStaffBll
    {
        public void RegisterUser(PostRegisterModel data)
        {
            
        }
    }
}
