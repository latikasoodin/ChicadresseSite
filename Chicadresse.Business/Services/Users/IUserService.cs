using System.Collections.Generic;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface IUserService
    {
        User GetUser(string email, string password);

        IEnumerable<User> GetUsers();
    }
}
