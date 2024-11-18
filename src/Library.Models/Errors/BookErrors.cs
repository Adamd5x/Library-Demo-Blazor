using ErrorOr;

namespace Library.Models.Errors;

public static class BookErrors
{
    public static Error StateNotPermitted => Error.Failure (
        code: nameof(ErrorsCodes.StateNotPermitted),
        description: "State is not permitted"
        );

    public static Error BorrorwIsNotPermitted => Error.Failure (
        code: nameof(ErrorsCodes.BorrowNotPermitted),
        description: "Borrow is not permitted"
        );

    public static Error AlreadyExists => Error.Failure (
    code: nameof (ErrorsCodes.AlreadyExists),
    description: "Book already exists"
    );
}
