using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Rookie.DataAccessor.Data
{
    public class EcomDbContextFactory : IDesignTimeDbContextFactory<EcomDbContext>
    {
        public EcomDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<EcomDbContext>();
            optionsBuilder.UseSqlServer("Data Source=blog.db");

            return new EcomDbContext(optionsBuilder.Options);

        }
    }
}
