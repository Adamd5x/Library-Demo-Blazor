using Library.Models.Entity;

namespace Library.Repository.Seeder;

internal static class SeedDataInitializer
{
    private static IList<string> authors = [
        "Vincent V. Severski",
        "Piotr Wroński",
        "Ian Flemming"
    ];


    public static IEnumerable<Book> InitBookContent => [
        new Book(){Id = 1, Title = "Nielegalni", Isbn = "9788375542967", Author = authors[0], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 2, Title = "Niepokorni", Isbn = "9788380156227", Author = authors[0], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 3, Title = "Niewierni", Isbn = "9788375546460", Author = authors[0], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 4, Title = "Nieśmiertelni", Isbn = "9788375549836", Author = authors[0], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 5, Title = "Plac Senacki 6PM", Isbn = "9788381439961", Author = authors[1], State = (int)Models.BookState.Borrowed },
        new Book(){Id = 6, Title = "Dystopia", Isbn = "9788382526370", Author = authors[1], State = (int)Models.BookState.Borrowed },
        new Book(){Id = 7, Title = "Zamęt", Isbn = "9788380159990", Author = authors[0], State = (int)Models.BookState.Borrowed },
        new Book(){Id = 8, Title = "Odwet", Isbn = "9788381430043", Author = authors[0], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 9, Title = "Nabór", Isbn = "9788381433839", Author = authors[0], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 10, Title = "Holub", Isbn = "9788395755286", Author = authors[1], State = (int)Models.BookState.Borrowed },
        new Book(){Id = 11, Title = "Reset", Isbn = "9788379653089", Author = authors[1], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 12, Title = "Spisek założycielski. Historia jednego morderstwa", Isbn = "9788366498013", Author = authors[1], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 13, Title = "Casino Royale", Isbn = "9780713481822", Author = authors[2], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 14, Title = "Live and Let Die", Isbn = "9780713481823", Author = authors[2], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 15, Title = "Dr. No", Isbn = "9780713481824", Author = authors[2], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 16, Title = "From Russia, with Love", Isbn = "9780713481825", Author = authors[2], State = (int)Models.BookState.Demaged },
        new Book(){Id = 17, Title = "Goldfinger", Isbn = "9780713481826", Author = authors[2], State = (int)Models.BookState.Demaged },
        new Book(){Id = 18, Title = "The Spy Who Leved Me", Isbn = "9780713481827", Author = authors[2], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 19, Title = "On Her Majesty's Secret Service", Isbn = "9780713481828", Author = authors[2], State = (int)Models.BookState.OnTheShelf },
        new Book(){Id = 20, Title = "You Only Live Twice", Isbn = "9780713481829", Author = authors[2], State = (int)Models.BookState.OnTheShelf },
        ];
}

