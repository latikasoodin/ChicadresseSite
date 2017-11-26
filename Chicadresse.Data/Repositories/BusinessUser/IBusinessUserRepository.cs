using Chicadresse.Data.Base;
using Chicadresse.Data.UnitOfWork;
using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories
{
    public interface IBusinessUserRepository : IRepository<Business_User>
    {
    }
}
