﻿using System;
using Microsoft.Owin;
using Owin;
using LMDB.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LMDB.Models;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(LMDB.Web.Startup))]
namespace LMDB.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUser();
        }

        private void createRolesAndUser()
        {
            MoviesContext context = new MoviesContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any(r=>r.Name == "admin"))
            {
                var roleCreated = roleManager.Create(new IdentityRole("Admin"));
                if (roleCreated.Succeeded)
                {
                    if (!context.Users.Any(u=>u.UserName == "admin"))
                    {
                        var user = new ApplicationUser{ UserName = "admin"};
                        userManager.Create(user, "!Asd123");
                        userManager.AddToRole(user.Id, "Admin");
                    }                   
                }
            }          
        }
    }
}
