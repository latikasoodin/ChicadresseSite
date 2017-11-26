using Chicadresse.Data.Base;
using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Data.Repositories.TaskTiming
{
    public class TaskTimingRepository : GenericRepository<Task_Timing>, ITaskTimingRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public TaskTimingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region methods

        #endregion
    }
}
