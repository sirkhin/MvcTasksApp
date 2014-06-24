using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Abstract
{
    public interface IUsersTasksRepository
    {
        IQueryable<UserTasks> UserTasks { get; }

        void SaveUserTask(UserTasks userTask);

        UserTasks DeleteUserTask(int userTaskID);
    }
}
