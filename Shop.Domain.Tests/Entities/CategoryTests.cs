using Xunit;
using FluentAssertions;

namespace Shop.Domain.Entities.Tests
{
    public class CategoryTests
    {
        [Fact]
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

        [Fact]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // Arramge
            var category = new Category();

            // Act
            var action = () => category.EncodeName();

            // Assert
            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}