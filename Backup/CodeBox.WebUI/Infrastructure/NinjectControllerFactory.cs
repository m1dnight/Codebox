using System;
using System.Web.Mvc;
using System.Web.Security;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete;
using CodeBox.WebUI.Infrastructure.Concrete;
using Ninject;

namespace CodeBox.WebUI.Infrastructure
{
    //New Controllerfactory to use DI with ninject
    //Config set at Global.asax
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<ISnippetRepository>().To<EFSnippetRepository>();
            ninjectKernel.Bind<ILanguageRepository>().To<EFLanguageRepository>();
            ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
            ninjectKernel.Bind<IRoleRepository>().To<EFRoleRepository>();
            ninjectKernel.Bind<IGroupRepository>().To<EFGroupRepository>();
            ninjectKernel.Bind<MembershipProvider>().To<CustomMembershipProvider>();
            
        }
    }
}