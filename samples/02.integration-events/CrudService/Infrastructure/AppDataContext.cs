using CrudService.Infrastructure.EntityConfigurations;
using CrudService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrudService.Infrastructure
{
    public class AppDataContext : DbContext
    {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        public DbSet<BookItem> BookItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookItemEntityTypeConfiguration());
        }



        public class AppDataContextDesignFactory : IDesignTimeDbContextFactory<AppDataContext>
        {
            public AppDataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,5433;Initial Catalog=Hooli.Services.AppDb; User Id=sa;Password=Pass@word");

                return new AppDataContext(optionsBuilder.Options);
            }
        }

    }
}
