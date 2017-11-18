using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.GenericRepository;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private ChicadressEntities context = new ChicadressEntities();

        private GenericRepository<User> userRepository;
        private GenericRepository<User_Role> userRoleRepository;
        private GenericRepository<UserActivation> userActivationRepository;
        private GenericRepository<Task> taskRepository;
        private GenericRepository<Business_User> businessUserRepository;
        private GenericRepository<User_FavouriteBusinessUser> userFavouriteBusinessRepository;
        private GenericRepository<Task_Timing> taskTimingRepository;
        private GenericRepository<User_Task> userTaskRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<User>(context);
                return userRepository;
            }
        }

        public GenericRepository<User_Role> UserRoleRepository
        {
            get
            {
                if (this.userRoleRepository == null)
                    this.userRoleRepository = new GenericRepository<User_Role>(context);
                return userRoleRepository;
            }
        }

        public GenericRepository<UserActivation> UserActivationRepository
        {
            get
            {
                if (this.userActivationRepository == null)
                    this.userActivationRepository = new GenericRepository<UserActivation>(context);
                return userActivationRepository;
            }
        }

        public GenericRepository<Task> TaskRepository
        {
            get
            {
                if (this.taskRepository == null)
                    this.taskRepository = new GenericRepository<Task>(context);
                return taskRepository;
            }
        }

        public GenericRepository<Business_User> BusinessUserRepository
        {
            get
            {
                if (this.businessUserRepository == null)
                    this.businessUserRepository = new GenericRepository<Business_User>(context);
                return businessUserRepository;
            }
        }

        public GenericRepository<User_FavouriteBusinessUser> UserFavouriteBusinessRepository
        {
            get
            {
                if (this.userFavouriteBusinessRepository == null)
                    this.userFavouriteBusinessRepository = new GenericRepository<User_FavouriteBusinessUser>(context);
                return userFavouriteBusinessRepository;
            }
        }

        public GenericRepository<Task_Timing> TaskTimingRepository
        {
            get
            {
                if (this.taskTimingRepository == null)
                    this.taskTimingRepository = new GenericRepository<Task_Timing>(context);
                return taskTimingRepository;
            }
        }

        public GenericRepository<User_Task> UserTaskRepository
        {
            get
            {
                if (this.userTaskRepository == null)
                    this.userTaskRepository = new GenericRepository<User_Task>(context);
                return userTaskRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //Free any other managed objects here. 
                context.Dispose();
            }

            // Free any unmanaged objects here. 
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
