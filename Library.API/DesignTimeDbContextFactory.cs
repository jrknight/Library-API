using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Library.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReCircleDbContext>
    {
        public ReCircleDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("config.json");
            var config = configBuilder.Build();

            var connectionstring = config.GetConnectionString("AzureDb");

            var builder = new DbContextOptionsBuilder<ReCircleDbContext>();

            builder.UseSqlServer(connectionstring);

            var context = new ReCircleDbContext(builder.Options);

            return context;
        }
    }
}
