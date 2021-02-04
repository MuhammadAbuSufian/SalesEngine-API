using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Project.Model;

[assembly: OwinStartup(typeof(Project.Server.Startup))]

namespace Project.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BusinessDbContext, Model.Migrations.Configuration>());

            ConfigureAuth(app);
        }
    }
}
