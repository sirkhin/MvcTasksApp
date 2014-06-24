using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Abstract;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFStatusesRepository: IStatusesRepository
    {
        private EFStatusContext _context = new EFStatusContext();

        public IQueryable<Entities.Status> Statuses
        {
            get { return _context.Statuses; }
        }

        public void SaveStatus(Entities.Status status)
        {
            if (status.StatusID == 0)
            {
                _context.Statuses.Add(status);
            }
            else
            {
                Status dbEntry = _context.Statuses.Find(status.StatusID);
                if (dbEntry != null)
                {
                    dbEntry.Description = status.Description;
                }
            }
            _context.SaveChanges();
        }

        public Entities.Status DeleteStatus(int statusID)
        {
            Status dbEntry = _context.Statuses.Find(statusID);
            if (dbEntry != null)
            {
                _context.Statuses.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
