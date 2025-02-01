using jwt_auth_dotnet.Models;

namespace jwt_auth_dotnet.Repositories
{
    public interface IUserRepository
    {
        User? GetUserByEmailAndPassword(string email, string password);
    }
}
