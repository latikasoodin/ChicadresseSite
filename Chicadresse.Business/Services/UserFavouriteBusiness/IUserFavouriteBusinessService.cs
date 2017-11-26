using System.Collections.Generic;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface IUserFavouriteBusinessService
    {
        IEnumerable<User_FavouriteBusinessUser> GetByUserId(int userId);
    }
}
