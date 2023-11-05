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
    public class ItemTests
    {
        [Fact()]
        public void EncodeNameTest_ShouldSetEncodedName()
        {
            var item = new Item();
            item.Name = "Test Name";

            item.EncodeName();

            item.EncodedName.Should().Be("itemtestname");
        }
    }
}