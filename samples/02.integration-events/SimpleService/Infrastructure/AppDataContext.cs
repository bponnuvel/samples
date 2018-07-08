using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SimpleService.Model;

namespace SimpleService.Infrastructure
{
    public class AppDataContext : DbContext
    {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        public DbSet<BookRating> BookRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookRatingEntityTypeConfiguration());
        }



        public class AppDataContextDesignFactory : IDesignTimeDbContextFactory<AppDataContext>
        {
            public AppDataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>()
                    .UseSqlServer("Server=tcp:127.0.0.1,5433;Initial Catalog=Hooli.Services.AppDb2; User Id=sa;Password=Pass@word");

                return new AppDataContext(optionsBuilder.Options);
            }
        }

    }
}
