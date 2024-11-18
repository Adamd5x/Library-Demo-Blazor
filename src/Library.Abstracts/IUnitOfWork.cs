using Library.Models.Entity;

namespace Library.Abstracts;

public interface IUnitOfWork : IDisposable
{
    IGenericReposiory<Book> BookRepository { get; }
    Task SaveAsync ();
}
