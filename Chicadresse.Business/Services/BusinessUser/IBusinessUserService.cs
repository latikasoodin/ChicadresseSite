using System.Collections.Generic;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface IBusinessUserService
    {
        IEnumerable<Business_User> Get();
    }
}
