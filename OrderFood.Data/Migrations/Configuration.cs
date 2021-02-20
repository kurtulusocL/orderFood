namespace OrderFood.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OrderFood.Data.Data;
    using OrderFood.Data.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //CreateRoles(context);
            //CreateAdmin(context);
        }

        private void CreateAdmin(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = userManager.FindByNameAsync("Admin").Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "ocL.1972",
                    NameLastname = "Kurtuluş Öcal",
                    Email = "kurtulusocal@protonmail.com",
                    Country = "Turkey",
                    Gender = "Man",
                    Birthdate = DateTime.Now.ToLocalTime(),
                    City = "Ankara",
                    Province = "Ankara",
                    IsConfirmed = true,
                    CreatedDate = DateTime.Now.ToLocalTime(),
                    PhoneNumber = "+905444939494",
                    RespondTitle = "Admin",
                    EmailConfirmed = true
                };
                userManager.Create(user, "#ocL_2514#");
                userManager.AddToRole(user.Id, "Admin");
            }
            var userInRole = userManager.IsInRole(user.Id, "Admin");
            if (!userInRole)
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }

        private void CreateRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleName = { "Admin", "Users", "Helper", "Asistant", "Tienda" };
            foreach (var role in roleName)
            {
                if (roleManager.RoleExists(role) == false)
                {
                    var newRole = new IdentityRole { Name = role };
                    roleManager.Create(newRole);
                }
            }
        }
    }
}
