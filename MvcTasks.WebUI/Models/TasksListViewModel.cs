using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.WebUI.Models
{
    public class TasksListViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public string CurrentCategory { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}