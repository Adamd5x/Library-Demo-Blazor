namespace Library.Core.Extensions;

public static class StringExtensions
{
    public static TEnum ToEnum<TEnum>(this string input) where TEnum : struct
    {
        ArgumentNullException.ThrowIfNullOrEmpty(nameof(input));

        var result = Enum.TryParse(typeof(TEnum), input, out var enumValue);
        if (!result) 
        {
            return default;
        }

        return (TEnum)enumValue!;
    }
}
