using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcTasks.DomainModel.Abstract;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.DomainModel.Concrete
{
    public class EFUsersRepository: IUsersRepository
    {
        private EFUserContext _context = new EFUserContext();

        public IQueryable<Entities.User> Users
        {
            get { return _context.Users; }
        }

        public void SaveUser(Entities.User user)
        {
            if (user.UserID == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                User dbEntry = _context.Users.Find(user.UserID);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.Surname = user.Surname;
                    dbEntry.Address = user.Address;
                    dbEntry.PhoneNumber = user.PhoneNumber;
                    dbEntry.Email = user.Email;
                    dbEntry.ImageData = user.ImageData;
                    dbEntry.ImageMimeType = user.ImageMimeType;
                    dbEntry.Password = user.Password;
                    dbEntry.PasswordSalt = user.PasswordSalt;
                }
            }
            _context.SaveChanges();
        }

        public Entities.User DeleteUser(int userID)
        {
            User dbEntry = _context.Users.Find(userID);
            if (dbEntry != null)
            {
                _context.Users.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
