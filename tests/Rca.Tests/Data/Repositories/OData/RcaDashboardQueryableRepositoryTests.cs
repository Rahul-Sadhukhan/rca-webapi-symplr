// <copyright file="RcaDashboardQueryableRepositoryTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using Orion.Tests.EntityFrameworkHelpers;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Data.Repositories.Queryable;
using Xunit;

namespace Rca.Tests.Data.Repositories.OData
{
    public class RcaDashboardQueryableRepositoryTests : BaseTest
    {
        private readonly Mock<IRcaDbContext> _rcaDbContextMock;

        public RcaDashboardQueryableRepositoryTests()
        {
            _rcaDbContextMock = new Mock<IRcaDbContext>();
        }

        [Fact]
        public void GetRcaDashboardInfoOdataQueryable_Always_ReturnsDataFromDbContextOnce()
        {
            // Arrange
            var dashboardOdataQueryableDbSetMock = MockDbSet.CreateMockDbSet(new List<RCADashboardInfoView>().AsQueryable());
            _rcaDbContextMock.Setup(x => x.RCADashboardInfoViews).Returns(dashboardOdataQueryableDbSetMock.Object);
            var sut = new RcaDashboardQueryableRepository(_rcaDbContextMock.Object);

            // Act
            var result = sut.GetRcaDashboardInfoQueryableByUserId(It.IsAny<long>());

            // Assert
            result.Should().NotBeNull();
            _rcaDbContextMock.Verify(x => x.RCADashboardInfoViews, Times.Once);
        }
    }
}
