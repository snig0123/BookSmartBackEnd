using BookSmartBackEnd.Authentication;
using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEndDatabase.Constants;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class UserBll(IUserCreationService userCreationService, IUserRepository userRepository, JwtHelper jwtHelper) : IUserBll
    {
        public void RegisterUser(PostRegisterModel data)
        {
            userCreationService.CreateUser(data, RoleTypes.CLIENT);
        }

        public UserProfile? GetUserProfile(Guid userId)
        {
            User? user = userRepository.GetByIdWithRolesAndRoleTypes(userId);

            if (user == null)
            {
                return null;
            }

            return new UserProfile
            {
                UserId = user.USER_ID,
                Forename = user.USER_FORENAME,
                Surname = user.USER_SURNAME,
                Roles = user.USER_ROLES.Select(r => r.ROLE_ROLETYPE.ROLETYPE_NAME).ToList()
            };
        }

        public LoginResult? LoginUser(string email, string password)
        {
            User? user = userRepository.GetByEmailAndPasswordWithRoles(email, password);

            if (user == null)
            {
                return null;
            }

            return new LoginResult
            {
                Token = jwtHelper.CreateToken(user),
                Profile = new UserProfile
                {
                    UserId = user.USER_ID,
                    Forename = user.USER_FORENAME,
                    Surname = user.USER_SURNAME,
                    Roles = user.USER_ROLES.Select(r => r.ROLE_ROLETYPE.ROLETYPE_NAME).ToList()
                }
            };
        }
    }
}
