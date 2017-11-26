using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface ITaskService
    {
        IEnumerable<Entities.Domain.Task> GetTask();

        IEnumerable<Entities.Domain.Task> GetTaskByTimeMonth(HashSet<int> timeInMonth);

        IEnumerable<Entities.Domain.Task> GetByTaskId(HashSet<int> favIds);

        Entities.Domain.Task AddTask(Entities.Domain.Task tsk);

        Entities.Domain.Task GetByTaskId(int taskId);

        Entities.Domain.Task GetByUserIdTaskId(int userId, int taskId);

        void UpdateTask(Entities.Domain.Task obj);

        Entities.Domain.Task DeleteById(int taskid);
    }
}
