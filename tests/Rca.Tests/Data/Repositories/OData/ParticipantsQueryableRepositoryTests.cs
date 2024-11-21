// <copyright file="ParticipantsQueryableRepositoryTests.cs" company="symplr">
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
    public class ParticipantsQueryableRepositoryTests : BaseTest
    {
        private readonly Mock<IRcaDbContext> _rcaDbContextMock;

        public ParticipantsQueryableRepositoryTests()
        {
            _rcaDbContextMock = new Mock<IRcaDbContext>();
        }

        [Fact]
        public void GetParticipantOdataQueryable_Always_ReturnsDataFromDbContextOnce()
        {
            // Arrange
            var participantOdataQueryableDbSetMock = MockDbSet.CreateMockDbSet(new List<Participant>().AsQueryable());
            _rcaDbContextMock.Setup(x => x.Participants).Returns(participantOdataQueryableDbSetMock.Object);
            var sut = new ParticipantQueryableRepository(_rcaDbContextMock.Object);

            // Act
            var result = sut.GetParticipantQueryable();

            // Assert
            result.Should().NotBeNull();
            _rcaDbContextMock.Verify(x => x.Participants, Times.Once);
        }
    }
}
