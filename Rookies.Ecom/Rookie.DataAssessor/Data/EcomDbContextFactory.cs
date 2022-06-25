using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Rookie.DataAccessor.Data
{
    public class EcomDbContextFactory : IDesignTimeDbContextFactory<EcomDbContext>
    {
        public EcomDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionstring = config.GetConnectionString("EComDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<EcomDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            return new EcomDbContext(optionsBuilder.Options);

        }

    }
}
