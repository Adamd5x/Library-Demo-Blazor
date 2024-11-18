using FluentAssertions;
using Library.Models.Helpers;

namespace Library.Models.Test
{
    public class EntityHelperTest
    {
        [Fact]
        public void EntityHelper_StringFields_RetunsListFields ()
        {
            // Arrange

            // Act
            var result = EntityHelper.GetSortableColumns();

            // Assert
            result.Should ().NotBeNullOrEmpty ();
            result.Should ().HaveCount (4);
        }
    }
}