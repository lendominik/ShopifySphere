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
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User"});

            var isInRole = currentUser.IsInRole("Admin");

            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User" });

            var isInRole = currentUser.IsInRole("Owner");

            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingCaseRole_ShouldReturnFalse()
        {
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin", "User" });

            var isInRole = currentUser.IsInRole("admin");

            isInRole.Should().BeFalse();
        }
    }
}