using Chicadresse.Data.Base;
using Chicadresse.Data.UnitOfWork;
using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.BusinessUser
{
    public class BusinessUserRepository : GenericRepository<Business_User>, IBusinessUserRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public BusinessUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region methods

        #endregion
    }
}
