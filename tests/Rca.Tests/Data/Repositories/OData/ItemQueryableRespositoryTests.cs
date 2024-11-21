// <copyright file="ItemQueryableRespositotyTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Orion.Tests.EntityFrameworkHelpers;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Data.Repositories.Queryable;
using Xunit;

namespace Rca.Tests.Data.Repositories.OData
{
    public class ItemQueryableRespositoryTests : BaseTest
    {
        private readonly Mock<IRcaDbContext> _rcaDbContextMock;

        public ItemQueryableRespositoryTests()
        {
            _rcaDbContextMock = new Mock<IRcaDbContext>();
        }

        [Fact]
        public void GetItemOdataQueryable_Always_ReturnsDataFromDbContextOnce()
        {
            // Arrange
            var itemOdataQueryableDbSetMock = MockDbSet.CreateMockDbSet(new List<Item>().AsQueryable());
            _rcaDbContextMock.Setup(x => x.Items).Returns(itemOdataQueryableDbSetMock.Object);
            var sut = new ItemQueryableRepository(_rcaDbContextMock.Object);

            // Act
            var result = sut.GetItemQueryableByUserId(It.IsAny<long>());

            // Assert
            result.Should().NotBeNull();
            _rcaDbContextMock.Verify(x => x.Items, Times.Once);
        }
    }
}
