using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure
{
    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var builder = new DbContextOptionsBuilder<ProductContext>();
            var conString = configuration.GetConnectionString("DefaultConnection");
            builder.UseMySql(conString, ServerVersion.AutoDetect(conString));


            return new ProductContext(builder.Options);
        }
    }
}
