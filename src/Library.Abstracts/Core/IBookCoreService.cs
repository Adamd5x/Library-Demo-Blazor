using ErrorOr;
using Library.Models;
using Library.Models.Dto;

namespace Library.Abstracts.Core;

public interface IBookCoreService
{
    Task<ErrorOr<BookDto>> GetAsync (int id);

    Task<ErrorOr<BookDto>> CreateAsync(BookDto dto);

    Task<ErrorOr<BookDto>> UpdateAsync(int id, BookDto dto);

    Task<ErrorOr<bool>> DeleteAsync(int id);

    Task<ErrorOr<IEnumerable<BookDto>>> GetAllAsync(string sortBy, SortOrder sortOrder, int offset, int size);

    Task<ErrorOr<bool>> SetState(int id, string state);
}
