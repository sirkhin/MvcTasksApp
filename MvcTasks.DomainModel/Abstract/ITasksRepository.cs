using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Abstract
{
    public interface ITasksRepository
    {
        IQueryable<Task> Tasks { get; }

        void SaveTask(Task task);

        Task DeleteTask(int taskID);
    }
}
