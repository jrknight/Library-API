using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Library.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("config.json");
            var config = configBuilder.Build();

            var connectionstring = config.GetConnectionString("LocalDb");

            var builder = new DbContextOptionsBuilder<LibraryDbContext>();

            builder.UseSqlServer(connectionstring);

            var context = new LibraryDbContext(builder.Options);

            return context;
        }
    }
}
