using Chicadresse.Data.Base;
using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.UserActivation
{
    public class UserActivationRepository : GenericRepository<User_Activation>, IUserActivationRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public UserActivationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region methods

        #endregion
    }
}
