﻿using System.Data.Entity.Migrations;

namespace Peoples.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PopiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}