// <copyright file="ParticipantODataServiceTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Rca.Data;
using Rca.Data.Repositories.Queryable;
using Rca.Models.OData;
using Rca.Services.OData;
using Xunit;

namespace Rca.Tests.Services.OData
{
    public class ParticipantODataServiceTests : BaseTest
    {
        [Theory]
        [AutoMoqData]
        internal void GetParticipantQueryable_Always_ReturnsQueryable(
            [Frozen] Mock<IParticipantQueryableRepository> repositoryMock,
            [Frozen] Mock<IRcaUnitOfWork> unitOfWorkMock,
            List<ParticipantODataModel> participantODataModels,
            ParticipantODataService sut)
        {
            // Arrange
            var expectedList = participantODataModels.AsQueryable();
            unitOfWorkMock.Setup(x => x.ParticipantQueryableRepository).Returns(repositoryMock.Object);
            repositoryMock.Setup(x => x.GetParticipantQueryable()).Returns(expectedList);

            // Act
            var results = sut.GetParticipantQueryable();

            // Assert
            results.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetParticipantQueryable_Always_CallsParticipantODataRepositoryOnce(
            [Frozen] Mock<IRcaUnitOfWork> unitOfWorkMock,
            [Frozen] Mock<IParticipantQueryableRepository> repositoryMock)
        {
            // Arrange
            repositoryMock.Setup(x => x.GetParticipantQueryable()).Returns(new List<ParticipantODataModel>().AsQueryable());
            var sut = new ParticipantODataService(unitOfWorkMock.Object);

            // Act
            sut.GetParticipantQueryable();

            // Assert
            repositoryMock.Verify(x => x.GetParticipantQueryable(), Times.Once);
        }
    }
}