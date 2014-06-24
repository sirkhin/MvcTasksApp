using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Abstract
{
    public interface IStatusesRepository
    {
        IQueryable<Status> Statuses { get; }

        void SaveStatus(Status status);

        Status DeleteStatus(int statusID);
    }
}
