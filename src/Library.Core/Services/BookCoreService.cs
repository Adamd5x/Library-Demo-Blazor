using ErrorOr;
using Library.Abstracts;
using Library.Abstracts.Core;
using Library.Core.Controllers;
using Library.Core.Extensions;
using Library.Models;
using Library.Models.Dto;
using Library.Models.Entity;
using Library.Models.Errors;
using Mapster;
using MapsterMapper;

namespace Library.Core.Services;

public class BookCoreService(IUnitOfWork unitOfWork, IMapper mapper) : IBookCoreService
{
    public async Task<ErrorOr<BookDto>> CreateAsync (BookDto dto)
    {
        Book newBook = dto.Adapt<Book>();

        bool insertIsNotAvailable = await IsIsbnExistsAsync(dto.Isbn);
        if (insertIsNotAvailable) 
        {
            return BookErrors.AlreadyExists;
        }

        var result = await unitOfWork.BookRepository.InsertAsync(newBook);
        await unitOfWork.SaveAsync ();

        if (result is null)
        {
            return Error.Failure ();
        }

        return result.Adapt<BookDto>();
    }

    public async Task<ErrorOr<bool>> DeleteAsync (int id)
    {
        var found = await unitOfWork.BookRepository.GetAsync(x => x.Id == id && (BookState)x.State != BookState.Borrowed) ;

        if (found is null)
        {
            return Error.NotFound ();
        }

        await unitOfWork.BookRepository.DeleteAsync(id);
        await unitOfWork.SaveAsync ();
        
        return true;
    }

    public async Task<ErrorOr<IEnumerable<BookDto>>> GetAllAsync (string sortBy, SortOrder sortOrder, int offset, int size)
    {
        Dictionary<string, Func<IQueryable<Book>, IOrderedQueryable<Book>>> sorting = new() 
        {
            { "isbn+ascending", (x) => x.OrderBy(x => x.Isbn)},
            { "isbn+descending", (x) => x.OrderByDescending(x => x.Isbn)},
            { "title+ascending", (x) => x.OrderBy(x => x.Title)},
            { "title+descending", (x) => x.OrderByDescending(x => x.Title)},
            { "state+ascending", (x) => x.OrderBy(x => x.State)},
            { "state+descending", (x) => x.OrderByDescending(x => x.State)},
            { "author+ascending", (x) => x.OrderBy(x => x.Author)},
            { "author+descending", (x) => x.OrderByDescending(x => x.Author)},
        };

        string sortKey = $"{sortBy}+{sortOrder}".ToLower();

        var result = await unitOfWork.BookRepository.GetAllAsync(null, sorting[sortKey], null,offset, size);

        return result.Select(x => mapper.Map<BookDto>(x))
                     .ToList();
    }

    public async Task<ErrorOr<BookDto>> GetAsync (int id)
    {
        var result = await unitOfWork.BookRepository.GetAsync(x => x.Id == id);

        if (result is null)
        {
            return Error.NotFound ();
        }

        return result.Adapt<BookDto>();
    }

    public async Task<ErrorOr<bool>> SetState (int id, string state)
    {
        var found = await unitOfWork.BookRepository.GetAsync(x => x.Id == id);
        if (found is null)
        {
            return Error.NotFound ();
        }

        BookState newState = state.ToEnum<BookState>();
        BookStateController stateController = new(found);
        bool newStatePermitted = stateController.IsStatePermitted (newState);

        if (newState == BookState.Borrowed) 
        {
            bool isBorrowPermitted = stateController.IsBorrowPermited ();

            if (!isBorrowPermitted) 
            {
                return BookErrors.BorrorwIsNotPermitted;
            }
            newStatePermitted = newStatePermitted && isBorrowPermitted;
        }

        if (!newStatePermitted)
        {
            return BookErrors.StateNotPermitted;
        }

        found.State = (int)newState;

        unitOfWork.BookRepository.Update (found);
        await unitOfWork.SaveAsync ();

        return true;
    }

    public async Task<ErrorOr<BookDto>> UpdateAsync (int id, BookDto dto)
    {
        var found = await unitOfWork.BookRepository.GetAsync (x => x.Id == id);
        if (found is null)
        {
            return Error.NotFound();
        }

        BookStateController stateController = new(found);
        BookState requestedState = dto.State.ToEnum<BookState>();

        if (stateController.IsStateChanged(requestedState))
        {
            bool newStatePermitted = stateController.IsStatePermitted (requestedState);

            if (requestedState == BookState.Borrowed) 
            {
                bool isBorrowPermitted = stateController.IsBorrowPermited ();
                newStatePermitted = newStatePermitted && isBorrowPermitted;

                if (!newStatePermitted)
                {
                    return BookErrors.BorrorwIsNotPermitted;
                }
            }

            if (!newStatePermitted)
            {
                return BookErrors.StateNotPermitted;
            }
        }

        if (!found.Isbn.Equals (dto.Isbn, StringComparison.OrdinalIgnoreCase)) 
        { 
            bool canUpdateIsbn = await IsIsbnExistsAsync(dto.Isbn);
            if (canUpdateIsbn)
            { 
                return BookErrors.AlreadyExists;
            }
        }


        found.Isbn = dto.Isbn;
        found.Title = dto.Title;
        found.State = (int)requestedState;
        found.Author = dto.Author;
        
        unitOfWork.BookRepository.Update(found);
        await unitOfWork.SaveAsync();

        return await GetAsync(id);
    }

    private async Task<bool> IsIsbnExistsAsync(string isbn)
    {
        var found = await unitOfWork.BookRepository.GetAsync(x => x.Isbn == isbn);
        return found is not null;
    }
}
