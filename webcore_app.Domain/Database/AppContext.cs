using Microsoft.EntityFrameworkCore;
using webnet_app.domain.Entities.Permission;

namespace webcore_app.Core.Database
{
    public class AppContext : DbContext, IMyDbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionType> PermissionTypes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

 

        }


    }
}

