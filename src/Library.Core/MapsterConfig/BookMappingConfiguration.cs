using Library.Core.Extensions;
using Library.Models;
using Library.Models.Dto;
using Library.Models.Entity;
using Mapster;

namespace Library.Core.MapsterConfig;

internal class BookMappingConfiguration : IRegister
{
    public void Register (TypeAdapterConfig config)
    {
        config.NewConfig<BookDto, Book> ()
              .Map(dest => dest.Id, src => src.Id)
              .Map (dest => dest.Isbn, src => src.Isbn)
              .Map (dest => dest.Title, src => src.Title)
              .Map (dest => dest.Author, src => src.Author)
              .Map (dest => dest.State, src => src.State.ToEnum<BookState> ());

        config.NewConfig<Book, BookDto>()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.Isbn, src => src.Isbn)
              .Map(dest => dest.Title, src => src.Title)
              .Map(dest => dest.Author, src => src.Author)
              .Map(dest => dest.State, src => ((BookState)src.State).ToString());
    }
}
