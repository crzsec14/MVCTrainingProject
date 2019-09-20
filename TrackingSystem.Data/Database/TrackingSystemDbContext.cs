using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Data.Migrations;
using TrackingSystem.Data.Models;


namespace TrackingSystem.Data.Database
{
    public class TrackingSystemDbContext : DbContext        
    {
        public TrackingSystemDbContext() 
            : base(ConfigurationManager.ConnectionStrings["TrackingSystemDbContext"].ConnectionString)
        {
            //Database.SetInitializer();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
    }
}
