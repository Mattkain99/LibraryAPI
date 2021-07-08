using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Data.Configurations
{
    public class LibraryBookConfiguration : IEntityTypeConfiguration<LibraryBook>
    {
        public void Configure(EntityTypeBuilder<LibraryBook> builder)
        {
            builder.ToTable("LibraryBook", schema: "LibraryAPI");
            builder.HasKey(lb => new {lb.LibraryId, lb.BookId});
            builder.HasOne(lb => lb.Library).WithMany(l => l.LibraryBooks).HasForeignKey(lb => lb.LibraryId);
            builder.HasOne(lb => lb.Book).WithMany(b => b.LibraryBooks).HasForeignKey(lb => lb.BookId);
        }
    }
}