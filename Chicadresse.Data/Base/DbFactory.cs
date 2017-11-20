using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Base
{
    public interface IDbFactory
    {
        ChicadressEntities Init();
    }

    public class DbFactory : IDbFactory, IDisposable
    {
        ChicadressEntities _context;

        #region Implementations

        public ChicadressEntities Init()
        {
            return this._context ?? (this._context = new ChicadressEntities());
        }

        public void Dispose()
        {
            if (_context != null)
            {
                this._context.Dispose();
            }
        }

        #endregion
    }
}
