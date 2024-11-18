using Library.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public partial class LibraryDbContext
{
    public DbSet<Book> Books { get; set; }
}
