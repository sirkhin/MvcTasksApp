using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFUserTaskContext: DbContext
    {
        public DbSet<UserTasks> UsersTasks { get; set; }
    }
}
