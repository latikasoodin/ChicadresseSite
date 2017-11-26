using System.Linq;
using System.Collections.Generic;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class UserFavouriteBusinessService : IUserFavouriteBusinessService
    {
        #region Properties

        private readonly IUserFavouriteBusinessRepository _userFavouriteBusinessRepository;

        #endregion

        #region ctor

        public UserFavouriteBusinessService(IUserFavouriteBusinessRepository userFavouriteBusinessRepository)
        {
            _userFavouriteBusinessRepository = userFavouriteBusinessRepository;
        }

        #endregion

        #region methods

        public IEnumerable<User_FavouriteBusinessUser> GetByUserId(int userId)
        {
            return _userFavouriteBusinessRepository.GetMany(u => u.UserId.Equals(userId));
        }

        #endregion

    }
}
