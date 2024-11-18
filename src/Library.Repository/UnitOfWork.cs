using Library.Abstracts;
using Library.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

internal class UnitOfWork(DbContextOptions<LibraryDbContext> dbContextOptions) : IUnitOfWork
{
    private bool _disposed = false;
    private readonly LibraryDbContext dbContext = new(dbContextOptions);

    private IGenericReposiory<Book> _bookRepository;

    public IGenericReposiory<Book> BookRepository => _bookRepository ??= new GenericRepository<Book>(dbContext);

    public async Task SaveAsync ()
    {
        await dbContext.SaveChangesAsync();
    }

    #region Dispose
    public void Dispose ()
    {
        Dispose(true);
        GC.SuppressFinalize (this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed) 
        {
            if (disposing) 
            { 
                dbContext.Dispose();
            }
            _disposed = true;
        }
    }
    #endregion Dispose
}
