using System.Linq;
using System.Collections.Generic;
using Chicadresse.Entities.Domain;
using Chicadresse.Data.Repositories;

namespace Chicadresse.Business.Services
{
    public class UserTaskService : IUserTaskService
    {
        #region Properties

        private readonly IUserTaskRepository _userTaskRepository;

        #endregion

        #region ctor

        public UserTaskService(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        #endregion

        #region methods

        public void Add(IEnumerable<User_Task> obj)
        {
            foreach (var entity in obj)
            {
                _userTaskRepository.Insert(entity);
            }
        }

        public void Insert(User_Task obj)
        {
            _userTaskRepository.Insert(obj);
        }

        public IEnumerable<User_Task> GetById(int userid)
        {
            return _userTaskRepository.GetMany(u => u.UserId.Equals(userid));
        }

        public User_Task GetByUserIdTaskId(int userId, int taskId)
        {
            return _userTaskRepository.GetMany(u => u.UserId.Equals(userId) && u.TaskId.Equals(taskId)).FirstOrDefault();
        }

        public void Update(User_Task obj)
        {
            _userTaskRepository.Update(obj);
        }

        public void DeleteByUserIdandTaskId(int userId, int taskId)
        {
            _userTaskRepository.Delete(u => u.UserId.Equals(userId) && u.TaskId.Equals(taskId));
        }

        #endregion
    }
}
