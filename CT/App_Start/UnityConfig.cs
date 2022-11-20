using CT.Controllers;
using CT.Core.BL.Service;
using CT.Core.BL.Service.ServiceImpl;
using CT.Core.DAL.Entity;
using CT.Core.DAL.Repository;
using CT.Core.DAL.Repository.RepositoryImpl;
using CT.Core.DAL.UnitOfWork;
using CT.Core.DAL.UnitOfWork.UnitOfWorkImpl;
using CT.CvsHelper.Services;
using CT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc5;

namespace CT.App_Start
{
    public static class UnityConfig
    {
        #region "RegisterComponents"
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // 1. Entity context klase 
            string connectionString =
               ConfigurationManager.ConnectionStrings["CTEntities"].ConnectionString;
            container.RegisterType<CTEntities, CTEntities>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(connectionString)
                );
            container.RegisterType<Models.ApplicationDbContext, Models.ApplicationDbContext>(
               new PerResolveLifetimeManager());

            // 2. Repository klase
            container.RegisterType<IPersonRepository, PersonRepository>(new PerResolveLifetimeManager(), new InjectionConstructor(new ResolvedParameter<CTEntities>()));

            // 3. UnitOfWork klasa
            container.RegisterType<IUnitOfWork, SqlUnitOfWork>(new HierarchicalLifetimeManager(),
                                           new InjectionConstructor(new ResolvedParameter<CTEntities>()));

            // 4. Service klase
            container.RegisterType<IPersonService, PersonService>(new InjectionConstructor(new ResolvedParameter<IUnitOfWork>(), new ResolvedParameter<IPersonRepository>()));
            container.RegisterType<ICSVService, CSVService>();

            // 5. ASP.NET Identity            
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new PerResolveLifetimeManager(), new InjectionConstructor(new ResolvedParameter<ApplicationDbContext>()));
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            // 6. Registruj dependency resolver - e za MVC i WebApi 
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = (new Unity.WebApi.UnityDependencyResolver(container));
        }
        #endregion

    }
}