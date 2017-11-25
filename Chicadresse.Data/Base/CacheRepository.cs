using Chicadresse.Core.Caching;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Base
{
    public interface ICacheRepository
    {
        IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        IEnumerable<TEntity> Get<TEntity>() where TEntity : class;
    }

    public class CacheRepository : ICacheRepository
    {
        internal ChicadressEntities context;
        internal ICacheManager _cacheManager;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ChicadressEntities Context
        {
            get { return context ?? (context = DbFactory.Init()); }
        }


        public CacheRepository(IDbFactory dbFactory, ICacheManager cacheManager)
        {
            this.DbFactory = dbFactory;
            this._cacheManager = cacheManager;
        }

        public IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            IEnumerable<TEntity> data;
            if (where != null)
            {
                data = dbSet.Where(where).ToList();
            }
            else
            {
                data = dbSet.ToList();
            }
            return _cacheManager.Set(typeof(TEntity).FullName, data, 10);
        }

        public IEnumerable<TEntity> Get<TEntity>() where TEntity : class
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            var data = dbSet.ToList();
            return _cacheManager.Set(typeof(TEntity).FullName, data, 10);
        }
    }

}