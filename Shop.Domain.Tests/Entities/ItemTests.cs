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
    }
}