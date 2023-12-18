using Xunit;
using Shop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Shop.Application.ApplicationUser.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User"});

            // Act
            var isInRole = currentUser.IsInRole("Admin");

            // Assert
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User" });

            // Act
            var isInRole = currentUser.IsInRole("Owner");

            // Assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingCaseRole_ShouldReturnFalse()
        {
            // Arange
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User" });

            // Act
            var isInRole = currentUser.IsInRole("admin");

            // Assert
            isInRole.Should().BeFalse();
        }
    }
}