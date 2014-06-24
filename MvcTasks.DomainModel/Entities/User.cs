using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MvcTasks.WebUI.Attributes;

namespace MvcTasks.DomainModel.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)] 
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter a user name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a user surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter a user address")]
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [UniqueEmail(ErrorMessage = "This e-mail is already registered")]
        public string Email { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }
    }
}
