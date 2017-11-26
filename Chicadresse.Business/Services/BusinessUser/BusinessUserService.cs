using System.Linq;
using System.Collections.Generic;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class BusinessUserService : IBusinessUserService
    {
        #region Properties

        private readonly IBusinessUserRepository _businessUserRepository;

        #endregion

        #region ctor

        public BusinessUserService(IBusinessUserRepository businessUserRepository)
        {
            _businessUserRepository = businessUserRepository;
        }

        #endregion

        #region methods

        public IEnumerable<Business_User> Get()
        {
            return _businessUserRepository.Get();
        }

        #endregion
    }
}
