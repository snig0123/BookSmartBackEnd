using BookSmartBackEnd.Authentication;
using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Constants;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class UserBll(IUserCreationService userCreationService, BookSmartContext bookSmartContext, JwtHelper jwtHelper) : IUserBll
    {
        public void RegisterUser(PostRegisterModel data)
        {
            userCreationService.CreateUser(data, RoleTypes.CLIENT);
        }

        public UserProfile? GetUserProfile(Guid userId)
        {
            User? user = bookSmartContext.USERS
                .Include(u => u.USER_ROLES)
                .ThenInclude(r => r.ROLE_ROLETYPE)
                .FirstOrDefault(u => u.USER_ID == userId);

            if (user == null)
            {
                return null;
            }

            return new UserProfile
            {
                Forename = user.USER_FORENAME,
                Surname = user.USER_SURNAME,
                Roles = user.USER_ROLES.Select(r => r.ROLE_ROLETYPE.ROLETYPE_NAME).ToList()
            };
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
