using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class IDMDBContext : DbContext
    {
        public DbSet<DriverModel> DriverModels { get; set; }
        public DbSet<MessageModel> MessageModels { get; set; }
        public DbSet<ServerModel> ServerModels { get; set; }
        public DbSet<ServerGroupModel> ServerGroupModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=master;User Id=SA;Password=password;");
        }
            
    }
}