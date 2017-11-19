using System.Linq;
using System.Collections.Generic;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class UserService : IUserService
    {
        #region Properties

        private readonly IUserRepository _userRepository;

        #endregion

        #region ctor

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region methods

        public User GetUser(string email, string password)
        {
            // TODO: Password should be checked based on hash key
            return _userRepository.GetMany(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();

        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get();
        }

        #endregion
    }
}
