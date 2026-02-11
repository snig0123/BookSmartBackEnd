using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEnd.Utilities;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class UserCreationService(BookSmartContext bookSmartContext) : IUserCreationService
    {
        public void CreateUser(PostRegisterModel data, Guid roleTypeId)
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
                USER_PASSWORDEXPIRED = false,
                USER_ROLES = { new Role
                    {
                        ROLE_ID = Guid.NewGuid(),
                        ROLE_USERID = newUserId,
                        ROLE_ROLETYPEID = roleTypeId
                    }
                }
            });

            bookSmartContext.SaveChanges();
        }
    }
}
