using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Abstract;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFTasksRepository: ITasksRepository
    {
        private EFTaskContext _context = new EFTaskContext();

        public IQueryable<Entities.Task> Tasks
        {
            get { return _context.Tasks; }
        }

        public void SaveTask(Entities.Task task)
        {
            if (task.TaskID == 0)
            {
                _context.Tasks.Add(task);
            }
            else
            {
                Task dbEntry = _context.Tasks.Find(task.TaskID);
                if (dbEntry != null)
                {
                    dbEntry.Name = task.Name;
                    dbEntry.Description = task.Description;
                    dbEntry.Category = task.Category;
                    dbEntry.StartDate = task.StartDate;
                    dbEntry.EndDate = task.EndDate;
                    dbEntry.StatusID = task.StatusID;
                }
            }
            _context.SaveChanges();
        }

        public Entities.Task DeleteTask(int taskID)
        {
            Task dbEntry = _context.Tasks.Find(taskID);
            if (dbEntry != null)
            {
                _context.Tasks.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
