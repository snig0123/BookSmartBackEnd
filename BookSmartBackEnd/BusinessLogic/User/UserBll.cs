using BookSmartBackEnd.Models;
using BookSmartBackEndDatabase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookSmartBackEnd.Authentication;
using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Constants;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEnd.Utilities;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class UserBll(BookSmartContext bookSmartContext, JwtHelper jwtHelper) : IUserBll
    {
        public void RegisterUser(PostRegisterModel data)
        {
            Guard.IsNotNull(data, nameof(data));
            
            Guid newUserId = Guid.NewGuid();
            
            bookSmartContext.Add(new User
            {
                USER_ID = newUserId,
                USER_TITLE = "Mr",
                USER_FORENAME = data.FORENAME,
                USER_SURNAME = data.SURNAME,
                USER_EMAIL = data.EMAIL,
                USER_PASSWORD = data.PASSWORD,
                BUSINESS_ID = Guid.NewGuid(), //This will come from the client
                USER_CREATED = DateTime.Now,
                USER_UPDATED = DateTime.Now,
                USER_DELETED = false,
                USER_LOCKED = false,
                USER_LASTLOGIN = null,
                USER_TELEPHONE = "0123456789",
                USER_PASSWORDEXPIRED = false
            });
            bookSmartContext.Add(new Role{
                ROLE_ID = Guid.NewGuid(),
                ROLE_USERID = newUserId,
                ROLE_ROLETYPEID = RoleTypes.CLIENT
                });
            bookSmartContext.SaveChanges();
        }

        public string LoginUser(string email, string password)
        {
            //guard and email validation
            
            //Get the business id from the jwt and pass that in the where clause
            
            BookSmartBackEndDatabase.Models.User? user = bookSmartContext.USERS
                .Include(userRole => userRole.USER_ROLES)
                .ThenInclude(role => role.ROLE_ROLETYPE)
                .FirstOrDefault(a => a.USER_EMAIL == email && a.USER_PASSWORD == password);
            
            // return null if user not found
            if (user == null)
            {
                return string.Empty;
            }

            return jwtHelper.CreateToken(user);
        }
    }
}
