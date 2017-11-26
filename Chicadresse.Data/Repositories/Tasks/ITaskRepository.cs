using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicadresse.Data.Base;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Data.Repositories
{
    public interface ITaskRepository : IRepository<Entities.Domain.Task>
    {
    }
}
