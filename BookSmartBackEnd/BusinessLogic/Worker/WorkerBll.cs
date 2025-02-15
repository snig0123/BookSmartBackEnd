using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookSmartBackEnd.BusinessLogic
{
    public class WorkerBll : IWorkerBll
    {
        private BookSmartContext _bookSmartContext;
        public WorkerBll(IServiceProvider serviceProvider)
        {
            _bookSmartContext = serviceProvider.GetService<BookSmartContext>();
        }
        public void RegisterUser(PostRegisterModel data)
        {
            /*var db = new BookSmartContext();
            db.Add(new User
            {
                USER_ID = Guid.NewGuid(),
                USER_FORENAME = data.FORENAME,
                USER_SURNAME = data.SURNAME,
                USER_EMAIL = data.EMAIL,
                USER_PASSWORD = data.PASSWORD
            });
            db.SaveChanges();*/
        }

        public string LoginUser(string email, string password)
        {
            //guard and email validation
            User? user = _bookSmartContext.USERS.FirstOrDefault(a => a.USER_EMAIL == email && a.USER_PASSWORD == password);

            // return empty string if user not found
            if (user?.USER_FORENAME == null)
            {
                return string.Empty;
            }

            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(",XXyKY(&qq-8&`f9y8(#");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.USER_FORENAME)
                }),

                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
