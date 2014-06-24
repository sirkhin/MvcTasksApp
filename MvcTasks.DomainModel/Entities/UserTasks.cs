using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTasks.DomainModel.Entities
{
    public class UserTasks
    {
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Task")]
        public int TaskID { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}
