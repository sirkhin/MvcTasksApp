using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTasks.DomainModel.Abstract;

namespace MvcTasks.WebUI.Controllers
{
    public class NavController : Controller
    {
        private ITasksRepository _repository;

        public NavController(ITasksRepository repo)
        {
            _repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Tasks
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);

            return PartialView(categories);
        } 
    }
}
