using jwt_auth_dotnet.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt_auth_dotnet.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // 2 ways for generating tokens

            // First: using the JwtSecurityToken class
            //return handler.WriteToken(new JwtSecurityToken
            //(
            //    expires: DateTime.UtcNow.AddHours(2),
            //    signingCredentials: signingCredentials
            //));


            // Second: using the handler
            return handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = signingCredentials
            }));
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            claims.AddClaim(new Claim("CustomClaim", "Example"));
            return claims;
        }
    }
}
