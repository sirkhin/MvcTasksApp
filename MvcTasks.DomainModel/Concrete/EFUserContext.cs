using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFUserContext: DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
