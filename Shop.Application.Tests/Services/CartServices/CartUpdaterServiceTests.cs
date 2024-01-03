using Xunit;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Moq;
using FluentAssertions;

namespace Shop.Application.Services.CartServices.Tests
{
    public class CartUpdaterServiceTests
    {
        [Fact]
        public void UpdateOrCreateCartItem_WhenItemIsInCart_ShouldUpdateQuantity()
        {
            // Arrange
            var cartRepositoryServiceMock = new Mock<ICartRepositoryService>();

            var cartId = "cartId";

            var httpContextAccesor = new Mock<IHttpContextAccessor>();

            var cartUpdaterService = new CartUpdaterService(cartRepositoryServiceMock.Object);

            var item = new Domain.Entities.Item()
            {
                Id = 1,
                Price = 10,
            };

            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    ItemId = 1,
                    Quantity = 10,
                },
                new CartItem
                {
                    ItemId = 2,
                    Quantity = 15,

                }
            };

            // Act
            cartUpdaterService.UpdateOrCreateCartItem(item, cartId, cartItems, httpContextAccesor.Object);

            // Assert
            cartItems[0].UnitPrice.Should().Be(110);
            cartItems[0].Quantity.Should().Be(11);
        }
        [Fact]
        public void UpdateOrCreateCartItem_WhenItemIsNotInCart_ShouldCreateNewCartItem()
        {
            // Arrange
            var cartRepositoryServiceMock = new Mock<ICartRepositoryService>();

            var cartId = "cartId";

            var httpContextAccesor = new Mock<IHttpContextAccessor>();

            var cartUpdaterService = new CartUpdaterService(cartRepositoryServiceMock.Object);

            var item = new Domain.Entities.Item()
            {
                Id = 1,
                Price = 15
            };

            var cartItems = new List<CartItem>
            {

            };

            // Act
            cartUpdaterService.UpdateOrCreateCartItem(item, cartId, cartItems, httpContextAccesor.Object);

            // Assert
            cartItems[0].Quantity.Should().Be(1);
            cartItems[0].UnitPrice.Should().Be(15);
        }
    }
}