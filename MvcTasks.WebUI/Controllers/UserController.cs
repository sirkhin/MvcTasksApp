using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTasks.DomainModel.Entities;
using MvcTasks.DomainModel.Abstract;
using System.Web.Security;
using MvcTasks.WebUI.Models;

namespace MvcTasks.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUsersRepository _usersRepository;
        private ITasksRepository _tasksRepository;
        private int _userPageSize = 7;

        public UserController(IUsersRepository usersRepository, ITasksRepository tasksRepository)
        {
            _usersRepository = usersRepository;
            _tasksRepository = tasksRepository;
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
        public PartialViewResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //if (ModelState.IsValid)
            //{
            if (IsValid(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "User", new { uniqueName = username });
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View();
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

        public ActionResult UserList(int page = 1)
        {
            UsersListViewModel usersModel = new UsersListViewModel
            {
                Users = _usersRepository.Users
                .OrderBy(p => p.UserID)
                .Skip((page - 1) * _userPageSize)
                .Take(_userPageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _userPageSize,
                    TotalItems = _usersRepository.Users.Count()
                },
            };
            return View(usersModel.Users);
        }

        public ActionResult DetailedUser(int userID = 1)
        {
            UsersListViewModel userModel = new UsersListViewModel
            {
                Users = _usersRepository.Users
                .Where(p => p.UserID == userID)
            };
            User user = (User)userModel.Users.FirstOrDefault();

            List<Task> tasks = new List<Task>();

            foreach (var task in _tasksRepository.Tasks)
            {
                tasks.Add(task);
            }

            ViewBag.Tasks = tasks;
            return PartialView(user);
        }

        public ActionResult Details(int id = 0)
        {
            var userListViewModel = new UsersListViewModel();
            var userList = _usersRepository.Users.ToList<User>();
            User user = userList.Find(u => u.UserID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(int id = 0)
        {
            var userList = _usersRepository.Users.ToList<User>();
            User user = userList.Find(u => u.UserID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User();

                newUser.Name = user.Name;
                newUser.Surname = user.Surname;
                newUser.Address = user.Address;
                newUser.Email = user.Email;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.ImageData = user.ImageData;
                newUser.ImageMimeType = user.ImageMimeType;
                _usersRepository.SaveUser(newUser);

                TempData["Msg"] = "Data has been updated succeessfully";
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
