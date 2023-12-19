﻿using Xunit;
using Shop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using FluentAssertions;

namespace Shop.Application.Services.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void Calculate_WhenCartItemsExist_ReturnsCorrectTotal()
        {
            // Arrange
            var httpContextAccessor = new Mock<IHttpContextAccessor>();  
            var cartItems = new List<CartItem>
            {
                new CartItem { UnitPrice = 10 },
                new CartItem { UnitPrice = 20 }
            };
            var orderService = new OrderService(httpContextAccessor.Object);

            // Act
            var result = orderService.Calculate(cartItems);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void Calculate_WhenNoCartItemsExist_ReturnsZero()
        {
            // Arrange
            var cartItems = new List<CartItem>();
            var orderService = new OrderService(Mock.Of<IHttpContextAccessor>());

            // Act
            var result = orderService.Calculate(cartItems);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CheckStockQuantity_WhenStockIsSufficient_NoExceptionThrown()
        {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem { Quantity = 2, Item = new Domain.Entities.Item { StockQuantity = 3 } }
            };
            var orderService = new OrderService(Mock.Of<IHttpContextAccessor>());

            // Act & Assert
           
        }

        [Fact]
        public void CheckStockQuantity_WhenStockIsInsufficient_ThrowsOutOfStockException()
        {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem { Quantity = 5, Item = new Domain.Entities.Item { StockQuantity = 3 } }
            };
            var orderService = new OrderService(Mock.Of<IHttpContextAccessor>());

            // Act & Assert
            Assert.Throws<OutOfStockException>(() => orderService.CheckStockQuantity(cartItems));
        }
    }
}