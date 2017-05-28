using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Data.Identity;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Services.Services;
using OnlineSatisProje.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OnlineSatisProje.Web
{
    public partial class Startup
    {
        private const string ConnectionStringName = "LocalConnection";

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.Register(c => new ApplicationContext(ConnectionStringName)).As<IDbContext>().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<Kullanici>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<IdentityRepository>().As<IIdentityRepostitory>().InstancePerRequest();
            builder.RegisterType<KullaniciRepository>().As<IKullaniciRepository>().InstancePerRequest();
            builder.RegisterType<RolRepository>().As<IRolRepository>().InstancePerRequest();
            builder.RegisterType<UrunRepository>().As<IUrunRepository>().InstancePerRequest();
            builder.RegisterType<SaticiRepository>().As<ISaticiRepository>().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);

            SeedRoles();
            CreateAdminUser();
        }

        private void SeedRoles()
        {
            var context = new ApplicationContext(ConnectionStringName);
            if (!context.Roles.Any(r => r.Name == "Admin"))
                CreateRole("Admin", context);

            if (!context.Roles.Any(r => r.Name == "Satıcı"))
                CreateRole("Satıcı", context);

            if (!context.Roles.Any(r => r.Name == "Standard"))
                CreateRole("Standard", context);
        }

        private void CreateRole(string roleName, ApplicationContext context)
        {
            var store = new ApplicationRoleStore(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole {Name = roleName};
            manager.Create(role);
        }

        private void CreateAdminUser()
        {
            var context = new ApplicationContext(ConnectionStringName);
            var userStore = new UserStore<Kullanici>(context);
            var userManager = new UserManager<Kullanici>(userStore);
            if (userManager.FindByEmail("admin@gmail.com") == null)
            {
                var user = new Kullanici
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    Ad = "Admin",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                };
                var result = userManager.Create(user, "admin123");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    userManager.AddToRole(user.Id, "Standard");
                }
            }
        }
    }
}