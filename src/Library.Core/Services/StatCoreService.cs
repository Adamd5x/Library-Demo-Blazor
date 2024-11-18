using Library.Abstracts;
using Library.Abstracts.Core;
using Library.Models;
using Library.Models.Dto;

namespace Library.Core.Services;

public class StatCoreService(IUnitOfWork unitOfWork) : IStatCoreService
{
    public async Task<StatDto> GetStatAsync ()
    {
        var books = await unitOfWork.BookRepository.GetAllAsync(offset:0, size: 1000);

        int booksTotal = books.Count();
        int bookAvailable = booksTotal -  books.Count(x => (BookState)x.State == BookState.Borrowed);
        int authorsTotal = books.GroupBy(x => x.Author).Count();

        var stat = new StatDto(booksTotal, bookAvailable, authorsTotal);

        return stat;
    }
}
