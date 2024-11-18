using Library.Models;
using Library.Models.Entity;

namespace Library.Core.Controllers;

internal class BookStateController(Book book)
{
    private readonly Dictionary<BookState, IEnumerable<BookState>> predecessorStates = new()
    {
        { BookState.OnTheShelf, [ BookState.Returned, BookState.Demaged ] },
        { BookState.Borrowed, [ BookState.OnTheShelf ] },
        { BookState.Returned, [ BookState.Borrowed ] },
        { BookState.Demaged, [ BookState.Returned, BookState.OnTheShelf ] },
    };

    private readonly IEnumerable<BookState> borrowProhibitionStates = [BookState.Demaged];

    public bool IsStatePermitted(BookState newState) => predecessorStates[newState].Contains((BookState)book.State);

    public bool IsBorrowPermited() => (BookState)book.State == BookState.OnTheShelf && !borrowProhibitionStates.Contains((BookState)book.State);

    public bool IsStateChanged(BookState state) => (BookState)book.State != state;
}
