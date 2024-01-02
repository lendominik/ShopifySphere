using Xunit;
using FluentAssertions;

namespace Shop.Domain.Entities.Tests
{
    public class CategoryTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            // Arrange
            var category = new Category();
            category.Name = "Test Category";

            // Act
            category.EncodeName();

            // Assert
            category.EncodedName.Should().Be("categorytestcategory");
        }
    }
}