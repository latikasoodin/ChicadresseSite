using Chicadresse.Data.Base;
using Chicadresse.Data.UnitOfWork;
using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.UserFavouriteBusiness
{
    public class UserFavouriteBusinessRepository : GenericRepository<User_FavouriteBusinessUser>, IUserFavouriteBusinessRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public UserFavouriteBusinessRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region methods

        #endregion
    }
}
