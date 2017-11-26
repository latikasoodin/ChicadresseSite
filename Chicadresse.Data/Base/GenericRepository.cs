using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

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

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual TEntity InsertAndReturn(TEntity entity)
        {
            TEntity ent = dbSet.Add(entity);
            context.SaveChanges();
            return ent;
        }

        public virtual TEntity Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            context.SaveChanges();
            return entityToDelete;
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> entityToDelete = dbSet.Where<TEntity>(where);
            foreach (TEntity obj in entityToDelete)
            {
                Delete(obj);
                context.SaveChanges();
            }
        }

        public virtual TEntity Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
            return entityToDelete;
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                context.SaveChanges();                
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        var exceptionData = string.Format("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
