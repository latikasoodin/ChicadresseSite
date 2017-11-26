using Chicadresse.Data.Base;
using Chicadresse.Data.UnitOfWork;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Data.Repositories.UserTasks
{
    public class UserTaskRepository : GenericRepository<User_Task>, IUserTaskRepository
    {
        #region fields

        private readonly IDbFactory _dbFactory;

        #endregion

        #region ctor

        public UserTaskRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion

        #region methods

        #endregion
    }
}
