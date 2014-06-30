using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using MvcTasks.DomainModel.Concrete;
using MvcTasks.DomainModel.Abstract;

namespace MvcTasks.WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {

            return controllerType == null
            ? null
            : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IStatusesRepository>().To<EFStatusesRepository>();
            _ninjectKernel.Bind<ITasksRepository>().To<EFTasksRepository>();
            _ninjectKernel.Bind<IUsersRepository>().To<EFUsersRepository>();
            _ninjectKernel.Bind<IUsersTasksRepository>().To<EFUsersTasksRepository>();
        }
    }
}