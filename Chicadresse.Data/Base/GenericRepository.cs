using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Base
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ChicadressEntities context;

        internal DbSet<TEntity> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ChicadressEntities Context
        {
            get { return context ?? (context = DbFactory.Init()); }
        }


        public GenericRepository(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            this.dbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = dbSet;
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public virtual TEntity Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            return Delete(entityToDelete);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> entityToDelete = dbSet.Where<TEntity>(where);
            foreach (TEntity obj in entityToDelete)
            {
                Delete(obj);
            }
        }

        public virtual TEntity Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            return dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
