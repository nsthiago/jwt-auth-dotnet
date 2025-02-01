using jwt_auth_dotnet.Models;

namespace jwt_auth_dotnet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;
        public UserRepository()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Password = "123",
                    Role = "admin"
                },
                new User
                {
                    Id = 2,
                    Email = "user@user.com",
                    Password = "123",
                    Role = "user"
                },
                new User
                {
                    Id = 3,
                    Email = "manager@manager.com",
                    Password = "123",
                    Role =  "manager"
                }
            };
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            return _users.FirstOrDefault(user => user.Email.ToLower() == email.ToLower() && user.Password == password);
        }
    }
}
