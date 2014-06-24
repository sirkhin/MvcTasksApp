using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFTaskContext: DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}
