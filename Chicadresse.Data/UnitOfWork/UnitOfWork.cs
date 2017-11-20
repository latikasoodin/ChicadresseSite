using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicadresse.Data.Base;

namespace Chicadresse.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        //private ChicadressEntities _context = new ChicadressEntities();
        private ChicadressEntities _context;
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public ChicadressEntities Context
        {
            get
            {
                return _context ?? (_context = this._dbFactory.Init());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //Free any other managed objects here. 
                _context.Dispose();
            }

            // Free any unmanaged objects here. 
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
