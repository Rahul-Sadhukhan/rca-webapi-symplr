// <copyright file="RcaDashboardODataServiceTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Orion.Users;
using Rca.Data;
using Rca.Data.Repositories.Queryable;
using Rca.Models.OData;
using Rca.Services;
using Rca.Services.OData;
using Xunit;

namespace Rca.Tests.Services.OData
{
    public class RcaDashboardODataServiceTests : BaseTest
    {
        [Theory]
        [AutoMoqData]
        internal void GetDashboardInfoQueryable_Always_ReturnsQueryable(
            [Frozen] Mock<IRcaDashboardQueryableRepository> repositoryMock,
            [Frozen] Mock<IRcaUnitOfWork> unitOfWorkMock,
            List<RcaDashboardODataModel> rcaDashboardODataModel,
            RcaDashboardODataService sut)
        {
            // Arrange
            var expectedList = rcaDashboardODataModel.AsQueryable();
            unitOfWorkMock.Setup(x => x.RcaDashboardQueryableRepository).Returns(repositoryMock.Object);
            repositoryMock.Setup(x => x.GetRcaDashboardInfoQueryableByUserId(1)).Returns(expectedList);

            // Act
            var results = sut.GetRcaDashboardInfoQueryable();

            // Assert
            results.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetDashboardInfoQueryable_Always_ReturnsRcaDashboardODataQueryableOnce(
            [Frozen] Mock<IRcaUnitOfWork> unitOfWorkMock,
            [Frozen] Mock<IRcaDashboardQueryableRepository> repositoryMock,
            [Frozen] Mock<ISoftLabelResourceService> softLabelResourceServiceMock,
            [Frozen] Mock<IUsersComponent> userComponentMock)
        {
            // Arrange
            repositoryMock.Setup(x => x.GetRcaDashboardInfoQueryableByUserId(It.IsAny<long>())).Returns(new List<RcaDashboardODataModel>().AsQueryable());
            var sut = new RcaDashboardODataService(unitOfWorkMock.Object, userComponentMock.Object, softLabelResourceServiceMock.Object);

            // Act
            sut.GetRcaDashboardInfoQueryable();

            // Assert
            repositoryMock.Verify(x => x.GetRcaDashboardInfoQueryableByUserId(It.IsAny<long>()), Times.Once);
        }
    }
}