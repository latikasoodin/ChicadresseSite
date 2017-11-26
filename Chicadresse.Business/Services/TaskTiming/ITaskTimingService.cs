using Chicadresse.Entities.Domain;
using System.Collections.Generic;

namespace Chicadresse.Business.Services
{
    public interface ITaskTimingService
    {
        IEnumerable<Task_Timing> GetTask();

        Task_Timing GetByTimeMonth(int? timemonth);
    }
}
