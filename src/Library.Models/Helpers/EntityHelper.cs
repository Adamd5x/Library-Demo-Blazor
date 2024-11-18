using System.Reflection;
using Library.Models.Entity;

namespace Library.Models.Helpers;

public static class EntityHelper
{
    public static IEnumerable<string> GetSortableColumns()
    {
        IEnumerable<string> members = new Book().GetType()
                   .GetMembers()
                   .Where(x => x.MemberType == MemberTypes.Property)
                   .Select(x => x.Name)
                   .ToList();
        return members.Where(x => !x.Equals("Id"));
    }
}
