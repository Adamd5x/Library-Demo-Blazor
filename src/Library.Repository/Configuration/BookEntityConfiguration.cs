using Library.Models.Entity;
using Library.Repository.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Repository.Configuration;

internal class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure (EntityTypeBuilder<Book> builder)
    {
        builder.ToTable ($"{Prefixes.Table}{nameof(Book)}");

        builder.HasKey( x => x.Id );

        builder.HasIndex(x => x.Isbn)
               .IsUnique();

        builder.Property (p => p.Isbn)
               .HasMaxLength(20)
              .IsRequired ();

        builder.Property(p => p.Title)
               .HasMaxLength(200)
               .IsRequired ();

        builder.Property (p => p.Author)
               .HasMaxLength(200)
               .IsRequired ();

        builder.HasData (SeedDataInitializer.InitBookContent);
    }
}
