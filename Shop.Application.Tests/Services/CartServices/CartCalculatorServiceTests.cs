using Xunit;
using Shop.Application.Services.CartServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using FluentAssertions;

namespace Shop.Application.Services.CartServices.Tests
{
    public class CartCalculatorServiceTests
    {

        [Fact]
        public void UpdateCartItemPriceAndQuantity_WhenNewQuantityIsCorrect()
        {
            // Arrange
            var cartItem = new CartItem
            {
                Quantity = 10,
                UnitPrice = 10,
            };

            var newQuantity = 15;

            var cartCalculatorService = new CartCalculatorService();

            // Act
            cartCalculatorService.UpdateCartItemPriceAndQuantity(cartItem, newQuantity);

            // Assert
            cartItem.Quantity.Should().Be(15);
            cartItem.UnitPrice.Should().Be(15);
        }
        [Fact]
        public void UpdateCartItemPriceAndQuantity_WhenNewQuantityIsZero()
        {
            // Arrange
            var cartItem = new CartItem
            {
                Quantity = 10,
                UnitPrice = 10,
            };

            var newQuantity = 0;

            var cartCalculatorService = new CartCalculatorService();

            // Act
            cartCalculatorService.UpdateCartItemPriceAndQuantity(cartItem, newQuantity);

            // Assert
            cartItem.Quantity.Should().Be(0);
            cartItem.UnitPrice.Should().Be(0);
        }
        [Fact]
        public void CalculateCartTotal_WhenCartItemsIsNull_ReturnsZero()
        {
            // Arrange 
            var cartItems = new List<CartItem>();
            var cartCalculatorService = new CartCalculatorService();

            // Act
            var result = cartCalculatorService.CalculateCartTotal(cartItems);

            // Assert
            result.Should().Be(0);
        }
        [Fact]
        public void CalculateCartTotal_WhenCartItemsIsNotNull_ReturnsCorrectTotal()
        {
            // Arrange 
            var cartItems = new List<CartItem>()
            {
                new CartItem()
                {
                    UnitPrice = 10,
                },
                new CartItem()
                {
                    UnitPrice = 20,
                },
                new CartItem()
                {
                    UnitPrice = 30,
                }
            };
            var cartCalculatorService = new CartCalculatorService();

            // Act
            var result = cartCalculatorService.CalculateCartTotal(cartItems);

            // Assert
            result.Should().Be(60);
        }
    }
}