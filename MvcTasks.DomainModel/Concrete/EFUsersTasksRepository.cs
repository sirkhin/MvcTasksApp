using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Abstract;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFUsersTasksRepository: IUsersTasksRepository
    {
        private EFUserTaskContext _context = new EFUserTaskContext();

        public IQueryable<Entities.UserTasks> UserTasks
        {
            get { return _context.UsersTasks; }
        }

        public void SaveUserTask(Entities.UserTasks userTask)
        {
            if (userTask.ID == 0)
            {
                _context.UsersTasks.Add(userTask);
            }
            else
            {
                Entities.UserTasks dbEntry = _context.UsersTasks.Find(userTask.ID);
                if (dbEntry != null)
                {
                    dbEntry.UserID = userTask.UserID;
                    dbEntry.TaskID = userTask.TaskID;
                }
            }
            _context.SaveChanges();
        }

        public Entities.UserTasks DeleteUserTask(int userTaskID)
        {
            Entities.UserTasks dbEntry = _context.UsersTasks.Find(userTaskID);
            if (dbEntry != null)
            {
                _context.UsersTasks.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
