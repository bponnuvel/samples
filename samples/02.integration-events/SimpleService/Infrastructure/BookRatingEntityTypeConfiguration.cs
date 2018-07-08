using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleService.Model;

namespace SimpleService.Infrastructure
{
    internal class BookRatingEntityTypeConfiguration : IEntityTypeConfiguration<BookRating>
    {

        public void Configure(EntityTypeBuilder<BookRating> builder)
        {
            builder.ToTable("BookRating");

            builder.HasKey(bi => bi.Id);
        }

    }
}