using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.WebUI.Models
{
    public class UserTasksListViewModel
    {
        public IEnumerable<UserTasks> UsersTasks { get; set; }
    }
}