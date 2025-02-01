using jwt_auth_dotnet.Models;

namespace jwt_auth_dotnet.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
