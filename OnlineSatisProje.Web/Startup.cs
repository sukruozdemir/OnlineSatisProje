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
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.Register(c => new ApplicationContext()).As<IDbContext>().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<Kullanici>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<KullaniciRepository>().As<IKullaniciRepository>().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);

            //SeedRoles();
        }

        private void SeedRoles()
        {
            var context = new ApplicationContext();
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new ApplicationRoleStore(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Satıcı"))
            {
                var store = new ApplicationRoleStore(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Satıcı" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Standard"))
            {
                var store = new ApplicationRoleStore(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Standard" };
                manager.Create(role);
            }

            if (context.Users.Any(u => u.Email == "sukruozdmr97@gmail.com"))
            {
                var store = new UserStore<Kullanici>(context);
                var manager = new UserManager<Kullanici>(store);
                var user = manager.FindByEmail("sukruozdmr97@gmail.com");
                if (!manager.IsInRole(user.Id, "Admin"))
                    manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}