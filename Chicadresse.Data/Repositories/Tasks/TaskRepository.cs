using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicadresse.Data.Base;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Data.Repositories.Tasks
{
    public class TaskRepository : GenericRepository<Entities.Domain.Task>, ITaskRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public TaskRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region methods

        #endregion
    }
}
