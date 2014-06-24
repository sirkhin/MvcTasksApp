using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MvcTasks.DomainModel.Abstract;
using MvcTasks.DomainModel.Concrete;

namespace MvcTasks.WebUI.Attributes
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var repository = new EFUsersRepository();
            var userWithTheSameEmail = repository.Users.SingleOrDefault(
                u => u.Email == (string)value);
            return userWithTheSameEmail == null;
        }
    }
}