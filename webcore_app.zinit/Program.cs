
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.IO;
using webnet_app.domain.Entities.Permission;

namespace webcore_app.zinit
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("develop");

            var dbOptions = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString);

            using (var ctx = new Core.Database.AppContext(dbOptions.Options))
            {

                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                ctx.PermissionTypes.Add(new PermissionType() { Description = "Enfermedad" });
                ctx.PermissionTypes.Add(new PermissionType() { Description = "diligencias" });
                ctx.PermissionTypes.Add(new PermissionType() { Description = "otros" });

                ctx.SaveChanges();


                ctx.Permissions.Add(new Permission()
                {
                    FirstName = "Dickerson",
                    LastName = "Macdonald",
                    PermissionDate = DateTime.Today,
                    PermissionTypeId = 1
                });

                ctx.Permissions.Add(new Permission()
                {
                    FirstName = "Larsen",
                    LastName = "Shaw",
                    PermissionDate = DateTime.Today,
                    PermissionTypeId = 1
                });


                ctx.Permissions.Add(new Permission()
                {
                    FirstName = "Geneva",
                    LastName = "Wilson",
                    PermissionDate = DateTime.Today,
                    PermissionTypeId = 1
                });

                ctx.Permissions.Add(new Permission()
                {
                    FirstName = "Geneva",
                    LastName = "Carney",
                    PermissionDate = DateTime.Today,
                    PermissionTypeId = 2
                });


                ctx.Permissions.Add(new Permission()
                {
                    FirstName = "Jami",
                    LastName = "Carney",
                    PermissionDate = DateTime.Today,
                    PermissionTypeId = 3
                });


                ctx.Permissions.Add(new Permission()
                {
                    FirstName = "Dickerson",
                    LastName = "Carney",
                    PermissionDate = DateTime.Today,
                    PermissionTypeId = 2
                });

                ctx.SaveChanges();
            }
        }
    }
}
