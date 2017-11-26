using System.Collections.Generic;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface IUserService
    {
        User GetUser(string email, string password);

        IEnumerable<User> GetUsers();

        User GetUserById(int id);

        void AddUser(User obj);

        void UpdateUser(User obj);

        User GetUserByEmail(string email);
    }
}
