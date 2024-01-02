using Xunit;
using FluentAssertions;

namespace Shop.Domain.Entities.Tests
{
    public class ItemTests
    {
        [Fact]
        public void EncodeName_ShouldSetEncodedName()
        {
            // Arrange
            var item = new Item();
            item.Name = "Test Item";

            // Act
            item.EncodeName();

            // Assert
            item.EncodedName.Should().Be("itemtestitem");
        }

        [Fact]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // Arramge
            var item = new Item();

            // Act
            var action = () => item.EncodeName();

            // Assert
            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}