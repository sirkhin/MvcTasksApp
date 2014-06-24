using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTasks.DomainModel.Entities;
using MvcTasks.DomainModel.Abstract;
using System.Web.Security;

namespace MvcTasks.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUsersRepository _usersRepository;

        public UserController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //public ActionResult Index(int? userId)
        //{
        //    var user = _usersRepository.Users.Where(p => p.UserID == userId).FirstOrDefault();
        //    return View(user);
        //}

        public ActionResult Index(string uniqueName)
        {
            var user = (User)_usersRepository.Users.Where(p => p.Email == uniqueName).First();
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypPass = crypto.Compute(user.Password);

                var newUser = new User();

                newUser.Name = user.Name;
                newUser.Surname = user.Surname;
                newUser.Password = encrypPass;
                newUser.PasswordSalt = crypto.Salt;
                newUser.Address = user.Address;
                newUser.Email = user.Email;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.ImageData = user.ImageData;
                newUser.ImageMimeType = user.ImageMimeType;
                _usersRepository.SaveUser(newUser);

                return RedirectToAction("Index", "User", new { uniqueName = newUser.Email});
            }
            else
            {
                ModelState.AddModelError("", "Data is not correct");
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            //if (ModelState.IsValid)
            //{
            if (IsValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return RedirectToAction("Index", "User", new { uniqueName = user.Email});
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("TaskList", "Home");
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            var user = _usersRepository.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public FileContentResult GetImage(int userID)
        {
            User user = _usersRepository.Users.FirstOrDefault(p => p.UserID == userID);
            if (user != null)
            {
                return File(user.ImageData, user.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}
