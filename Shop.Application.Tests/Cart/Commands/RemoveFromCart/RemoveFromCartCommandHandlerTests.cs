using Xunit;
using MediatR;
using Moq;
using Shop.Application.Services.CartServices;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using Shop.Application.Exceptions;

namespace Shop.Application.Cart.Commands.RemoveFromCart.Tests
{
    public class RemoveFromCartCommandHandlerTests
    {
        [Fact]
        public async Task Handle_RemoveFromCart_WhenCartIsNull()
        {
            // Arrange
            var command = new RemoveFromCartCommand()
            {
                Id = Guid.NewGuid()
            };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var cartRepositoryServiceMock = new Mock<ICartRepositoryService>();
            cartRepositoryServiceMock.Setup(m => m.GetCartItems(httpContextAccessorMock.Object))
                .Returns(new List<Domain.Entities.CartItem>
                {

                });

            var handler = new RemoveFromCartCommandHandler(cartRepositoryServiceMock.Object, httpContextAccessorMock.Object);

            // Act
            var subject = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await subject.Should().ThrowAsync<NotFoundException>();
        }
        [Fact]
        public async Task Handle_RemoveFromCart_WhenCartIsNotNull()
        {
            // Arrange
            var command = new RemoveFromCartCommand()
            {
                Id = Guid.NewGuid()
            };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var cartRepositoryServiceMock = new Mock<ICartRepositoryService>();
            cartRepositoryServiceMock.Setup(m => m.GetCartItems(httpContextAccessorMock.Object))
                .Returns(new List<Domain.Entities.CartItem>()
                {
                    new Domain.Entities.CartItem()
                    {
                        Id = command.Id
                    }
                });

            var handler = new RemoveFromCartCommandHandler(cartRepositoryServiceMock.Object, httpContextAccessorMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            cartRepositoryServiceMock.Verify(m => m.SaveCartItemsToSession(It.IsAny<List<Domain.Entities.CartItem>>(), httpContextAccessorMock.Object), Times.Once);
        }
    }
}