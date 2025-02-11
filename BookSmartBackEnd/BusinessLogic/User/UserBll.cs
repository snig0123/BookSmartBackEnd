using BookSmartBackEnd.BusinessLogic.User.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEndDatabase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookSmartBackEnd.BusinessLogic.User
{
    public class UserBll : IUserBll
    {
        public void RegisterUser(PostRegisterModel data)
        {
            BookSmartContext db = new BookSmartContext();
            db.Add(new BookSmartBackEndDatabase.User
            {
                USER_ID = Guid.NewGuid(),
                USER_FORENAME = data.FORENAME,
                USER_SURNAME = data.SURNAME,
                USER_EMAIL = data.EMAIL,
                USER_PASSWORD = data.PASSWORD
            });
            db.SaveChanges();
        }

        public string LoginUser(string email, string password)
        {
            //guard and email validation
            BookSmartContext db = new BookSmartContext();
            BookSmartBackEndDatabase.User? user = db.USER.FirstOrDefault(a => a.USER_EMAIL == email && a.USER_PASSWORD == password);

            // return null if user not found
            if (user == null)
            {
                return string.Empty;
            }

            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("MIICXgIBAAKBgQCeNoXeP3Povgq1KlA8t5duDH4XIegdAHNqc8TLmJA7HAFITPrW\nFpdzbBKLPrzrb+kqHhqy1/C4e7wPqXYoql+EzZAMyFaJ6hWgD9jrbEmVvB4jsCkb\newMnbB0A60V93zt5C0445TWO0Zg4GaHJZVftO0KDw/rBZrccUxWypgqoswIDAQAB\nAoGAMamzw9vvKnPdMJkjrquCoUzSl7hRACmQES5d6/rr62ITFPr1EhbtW5HlEEiV\nTOJIMqxYfSNDsOVGVzQ+nu08JhlswbDZAVBg57mn5yTUpcZgxHya6EbirHHBQ8Pq\njhUerkb6wPZL9ipcCXVkikuCHBFLm9j0eJSMePRsFgKs9OECQQDOkgTz/YPPCE8n\nhJr5n9QLUFzJVQ94AuHKHI9pTXZynKh4fgZfO2KGNa1P43n23xRPLuNaQPgIiPkO\nl36SsGUdAkEAxBJANMfmFQ0dmyF7i43MtA6zEtaTFSH6nU0t/wcvmQ+qUxYvBqNB\n99Ut7/JhWbXUJqriAuI8VrNe1rkbwBbsDwJBALrz2nm7+nv4KDM6x0uehDlNHPy1\n+A8EhLb3zC9ghQ/LiomqfTfZNh0DHXzNAogUc3wKkocPf6ux076KC2rVLF0CQQCZ\nXSU3o7yGbtHfi9sVF38sv+q2K3y0pPVgoQP/XWGPub8ialGyQXTSI79g1hfrkdw1\nuqg6VTeZIYhnMDdSkxtlAkEAlIVptHTX+dE3SEiYbKtynw0dfvYOmoajLJcoIVd8\nr9X3BVJLC7djUrDaD0WMFTP+4JVnZP5SY5lDsYHhmuW7ag==");

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, user.USER_FORENAME),
                }),

                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
