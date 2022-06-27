using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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
