using Chicadresse.Data.Base;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.Guests
{
    public class GuestTableRepository : GenericRepository<Guest_Table>, IGuestTableRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public GuestTableRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion


        #region Implementation


        #endregion

    }
}
