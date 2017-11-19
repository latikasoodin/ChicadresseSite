using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        ChicadressEntities Context { get; }
    }
}
