using System.Collections.Generic;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface IUserTaskService
    {
        void Add(IEnumerable<User_Task> obj);

        IEnumerable<User_Task> GetById(int userid);

        void Insert(User_Task obj);

        User_Task GetByUserIdTaskId(int userId, int taskId);

        void Update(User_Task obj);

        void DeleteByUserIdandTaskId(int userId, int taskId);
    }
}
