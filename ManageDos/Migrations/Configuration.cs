namespace ManageDos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ManageDos.Models;


    internal sealed class Configuration : DbMigrationsConfiguration<ManageDos.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ManageDos.Models.ApplicationDbContext";
        }

        protected override void Seed(ManageDos.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

        }
        void AddUser(ManageDos.Models.ApplicationDbContext context)
        {
            var user = new Models.ApplicationUser {UserName = "user1@email.com"};
            var um = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(context));
            um.Create(user, "password" );
        }
    }
}
