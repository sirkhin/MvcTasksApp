using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcTasks.DomainModel.Entities
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Description {get; set;}
        public string Category { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [ForeignKey("TaskStatus")]
        public int StatusID { get; set; }

        public virtual Status TaskStatus { get; set; }
    }
}
