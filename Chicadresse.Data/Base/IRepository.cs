using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets List of entity based on condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Gets a list of given entity type.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Gets a given entity by Id.
        /// </summary>
        /// <param name="id">Unique id for a entity.</param>
        /// <returns>Selected entity.</returns>
        TEntity GetByID(object id);

        /// <summary>
        /// Inserts a new entity to database.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        /// <returns>Inserted entity.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entityToDelete">Entity to be deleted.</param>
        /// <returns>Deleted entity.</returns>
        TEntity Delete(TEntity entityToDelete);


        /// <summary>
        /// Delete an entity based on condition
        /// </summary>
        /// <param name="where"></param>
        void Delete(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// updates a entity to database.
        /// </summary>
        /// <param name="entityToUpdate">Entity to be Updated.</param>

        void Update(TEntity entityToUpdate);
    }
}
