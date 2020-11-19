
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Peoples.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Peoples.Dal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}