using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookSmartBackEndDatabase.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookSmartBackEnd.Authentication;

internal sealed class JwtHelper(IConfiguration configuration)
{
    public string CreateToken(User user)
    {
        // authentication successful so generate jwt token
        byte[] key = Encoding.ASCII.GetBytes(configuration["jwtCert"] ?? string.Empty);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "BookSmart",
            Audience = user.BUSINESS_ID.ToString(),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512),
            Subject = new ClaimsIdentity(
                [
                    new (JwtRegisteredClaimNames.Sub, user.USER_ID.ToString())
                ]
            ),
            Claims = user.USER_ROLES.ToDictionary(r => r.ROLE_ROLETYPE.ROLETYPE_NAME, r => (object)"Y")
        };
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}