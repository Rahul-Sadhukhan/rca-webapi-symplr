// <copyright file="ItemODataServiceTests.cs" company="symplr">
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
using Rca.Services.OData;
using Xunit;

namespace Rca.Tests.Services.OData
{
    public class ItemODataServiceTests : BaseTest
    {
        [Theory]
        [AutoMoqData]
        internal void GetItemQueryable_Always_ReturnsQueryable(
            [Frozen] Mock<IItemQueryableRepository> repositoryMock,
            [Frozen] Mock<IRcaUnitOfWork> unitOfWorkMock,
            List<ItemODataModel> itemODataModels,
            ItemODataService sut)
        {
            // Arrange
            var expectedList = itemODataModels.AsQueryable();
            unitOfWorkMock.Setup(x => x.ItemQueryableRepository).Returns(repositoryMock.Object);
            repositoryMock.Setup(x => x.GetItemQueryableByUserId(1)).Returns(expectedList);

            // Act
            var results = sut.GetItemQueryable();

            // Assert
            results.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetItemQueryable_Always_CallsItemODataRepositoryOnce(
            [Frozen] Mock<IRcaUnitOfWork> unitOfWorkMock,
            [Frozen] Mock<IItemQueryableRepository> repositoryMock,
            [Frozen] Mock<IUsersComponent> userComponentMock)
        {
            // Arrange
            repositoryMock.Setup(x => x.GetItemQueryableByUserId(It.IsAny<long>())).Returns(new List<ItemODataModel>().AsQueryable());
            var sut = new ItemODataService(unitOfWorkMock.Object, userComponentMock.Object);

            // Act
            sut.GetItemQueryable();

            // Assert
            repositoryMock.Verify(x => x.GetItemQueryableByUserId(It.IsAny<long>()), Times.Once);
        }
    }
}
