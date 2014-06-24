using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Abstract
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }

        void SaveUser(User user);

        User DeleteUser(int userID);
    }
}
