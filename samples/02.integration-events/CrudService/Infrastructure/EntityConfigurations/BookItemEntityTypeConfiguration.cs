using CrudService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudService.Infrastructure.EntityConfigurations
{
    internal class BookItemEntityTypeConfiguration : IEntityTypeConfiguration<BookItem>
    {
        public void Configure(EntityTypeBuilder<BookItem> builder)
        {
            builder.ToTable("BookItem");

            builder.HasKey(bi => bi.Id);
        }
    }
}