using System.Linq;
using System.Collections.Generic;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class UserActivationService : IUserActivationService
    {
        #region Properties

        private readonly IUserActivationRepository _userActivationRepository;

        #endregion

        #region ctor

        public UserActivationService(IUserActivationRepository userActivationRepository)
        {
            _userActivationRepository = userActivationRepository;
        }

        #endregion

        #region methods

        public void AddUser(User_Activation obj)
        {
            _userActivationRepository.Insert(obj);
        }

        public User_Activation GetUser(string activationCode)
        {
            // TODO: Password should be checked based on hash key
            return _userActivationRepository.GetMany(u => u.ActivationCode.Equals(activationCode)).FirstOrDefault();
        }

        #endregion
    }
}
