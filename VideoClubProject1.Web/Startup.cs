using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using VideoClubProject1.Core.Entities;
using VideoClubProject1.Infrastructure.Data;

[assembly: OwinStartupAttribute(typeof(VideoClubProject1.Web.Startup))]
namespace VideoClubProject1.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdmin();
        }

        public void CreateAdmin()
        {

            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@gmail.com"
            };
            string password = "Admin123!";

            using (var db = new ApplicationDbContext())
            {
                //Initialize default role
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);


                var role = new IdentityRole() { Name = "Admin" };
                var roleExist = roleManager.RoleExists("Admin");
                if (!roleExist)
                {
                    roleManager.Create(role);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Role Admin already exists");
                }
                var store = new UserStore<ApplicationUser>(db);
                var manager = new UserManager<ApplicationUser, string>(store);

                //Initialize default user
                var userExist = manager.FindByName("Admin");

                if (userExist == null)
                {
                    var result = manager.Create(user, password);
                    if (!result.Succeeded)
                        throw new ApplicationException("Unable to create user");



                    result = manager.AddToRole(user.Id, "Admin");
                    if (!result.Succeeded)
                        throw new ApplicationException("Unable to add user to a role.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("User Admin already exists");
                }
            }

        }

    }
}
