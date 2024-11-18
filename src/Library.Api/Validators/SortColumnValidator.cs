using Library.Models.Helpers;

namespace Library.Api.Validators
{
    public static class SortColumnValidator
    {
        public static bool Validate(string columnName)
        {
            return EntityHelper.GetSortableColumns().Contains(columnName, StringComparer.OrdinalIgnoreCase);
        }
    }
}
