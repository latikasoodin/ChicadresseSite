using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class TaskService : ITaskService
    {
        #region Properties

        private readonly ITaskRepository _taskRepository;

        #endregion

        #region ctor

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        #endregion

        #region methods

        public IEnumerable<Entities.Domain.Task> GetTask()
        {
            return _taskRepository.Get();
        }

        public IEnumerable<Entities.Domain.Task> GetTaskByTimeMonth(HashSet<int> timeInMonth)
        {
            return _taskRepository.Get().Where(m => timeInMonth.Contains(Convert.ToInt32(m.TimeMonth))).ToList();
        }

        public IEnumerable<Entities.Domain.Task> GetByTaskId(HashSet<int> favIds)
        {
            return _taskRepository.Get().Where(m => favIds.Contains(m.TaskId)).ToList();
        }

        public Entities.Domain.Task AddTask(Entities.Domain.Task tsk)
        {
            return _taskRepository.InsertAndReturn(tsk);
        }

        public Entities.Domain.Task GetByTaskId(int taskId)
        {
            return _taskRepository.Get().Where(x=>x.TaskId.Equals(taskId)).FirstOrDefault();
        }

        public Entities.Domain.Task GetByUserIdTaskId(int userId, int taskId)
        {
            return _taskRepository.Get().Where(u => u.UserId.Equals(userId) && u.TaskId.Equals(taskId)).FirstOrDefault();
        }

        public void UpdateTask(Entities.Domain.Task obj)
        {
            _taskRepository.Update(obj);
        }

        public Entities.Domain.Task DeleteById(int taskid)
        {
            return _taskRepository.Delete(taskid);
        }

        #endregion
    }
}
