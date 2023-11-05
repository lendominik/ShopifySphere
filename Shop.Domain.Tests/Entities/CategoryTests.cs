using Xunit;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Shop.Domain.Entities.Tests
{
    public class CategoryTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            var category = new Category();
            category.Name = "Test Category";

            category.EncodeName();

            category.EncodedName.Should().Be("categorytestcategory");
        }
    }
}