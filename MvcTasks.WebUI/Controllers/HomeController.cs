using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MvcTasks.DomainModel.Abstract;
using MvcTasks.WebUI.Models;
using MvcTasks.DomainModel.Entities;

namespace MvcTasks.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUsersRepository _usersRepository;
        private ITasksRepository _tasksRepository;
        private IUsersTasksRepository _usersTasksRepository;
        private int _taskPageSize = 7;
        private int _userPageSize = 3;

        public HomeController(IUsersRepository usersRepository, ITasksRepository tasksRepository, IUsersTasksRepository usersTasksRepository)
        {
            _usersRepository = usersRepository;
            _tasksRepository = tasksRepository;
            _usersTasksRepository = usersTasksRepository;
        }

        public ActionResult Index()
        {
            return View();
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
            return View(usersModel);
        }

        public ActionResult TaskList(string category = null, int page = 1)
        {
            TasksListViewModel tasksModel = new TasksListViewModel
            {
                Tasks = _tasksRepository.Tasks
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.TaskID)
                .Skip((page - 1) * _taskPageSize)
                .Take(_taskPageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _taskPageSize,
                    TotalItems = category == null ?
                        _tasksRepository.Tasks.Count() :
                        _tasksRepository.Tasks.Where(e => e.Category == category).Count()
                },

                CurrentCategory = category
            };
            return View(tasksModel);
        }

        public ActionResult DetailedTask(int taskID = 1)
        {
            TasksListViewModel taskModel = new TasksListViewModel
            {
                Tasks = _tasksRepository.Tasks
                .Where(p => p.TaskID == taskID)
            };
            Task task = (Task)taskModel.Tasks.FirstOrDefault();
            
            List<string> userNames = new List<string>();

            foreach (var user in _usersRepository.Users)
            {
                string nameWithSurname = user.Surname + ", " + user.Name;
                userNames.Add(nameWithSurname);
            }

            ViewBag.UserNames = userNames;
            return PartialView(task);
        }

        [HttpPost]
        public ActionResult DetailedTask(Task task, string userName)
        {
            return View();
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

    }
}
