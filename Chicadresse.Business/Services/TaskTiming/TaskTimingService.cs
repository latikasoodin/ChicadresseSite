using System.Linq;
using System.Collections.Generic;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class TaskTimingService : ITaskTimingService
    {
        #region Properties

        private readonly ITaskTimingRepository _taskTimingRepository;

        #endregion

        #region ctor

        public TaskTimingService(ITaskTimingRepository taskTimingRepository)
        {
            _taskTimingRepository = taskTimingRepository;
        }

        #endregion

        #region methods

        public IEnumerable<Task_Timing> GetTask()
        {
            return _taskTimingRepository.Get();
        }

        public Task_Timing GetByTimeMonth(int? timemonth)
        {
            return _taskTimingRepository.Get().Where(m => m.Timing.Equals(timemonth)).FirstOrDefault();
            //return _taskTimingRepository.GetMany(u => u.Timing.Equals(timemonth)).FirstOrDefault();
        }

        #endregion
    }
}
