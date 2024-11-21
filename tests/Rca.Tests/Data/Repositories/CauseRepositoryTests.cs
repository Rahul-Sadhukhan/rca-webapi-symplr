// <copyright file="CauseRepositoryTests.cs" company="symplr">
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
using Rca.Data.Repositories;
using Xunit;

namespace Rca.Tests.Data.Repositories
{
    public class CauseRepositoryTests : BaseTest
    {
        private static Mock<IRcaDbContext> _rcaDbContext;

        public CauseRepositoryTests()
        {
            _rcaDbContext = new Mock<IRcaDbContext>();
        }

        [Fact]
        public void DoesCauseExist_Always_CallsDbContext_ReturnBooleanValue()
        {
            // Arrange
            var testCauses = GetCauses();
            var causeId = testCauses.First().CauseId;
            testCauses.First().IsDeleted = false;
            var testCausesDbSet = MockDbSet.CreateMockDbSet(testCauses.AsQueryable());
            testCausesDbSet.Setup(x => x.Include(It.IsAny<string>())).Returns(testCausesDbSet.Object);
            _rcaDbContext.Setup(x => x.Causes).Returns(testCausesDbSet.Object);
            var repository = new CauseRepository(_rcaDbContext.Object);

            // Act
            var res = repository.DoesCauseExist(causeId);

            // Assert
            res.Should().BeTrue();
            _rcaDbContext.Verify(x => x.Causes, Times.Once);
        }

        [Fact]
        public void GetCauseById_Always_CallsDbContext_ReturnData()
        {
            // Arrange
            var testCauses = GetCauses();
            var causeId = testCauses.First().CauseId;
            testCauses.First().IsDeleted = false;
            var testCausesDbSet = MockDbSet.CreateMockDbSet(testCauses.AsQueryable());
            testCausesDbSet.Setup(x => x.Include(It.IsAny<string>())).Returns(testCausesDbSet.Object);
            _rcaDbContext.Setup(x => x.Causes).Returns(testCausesDbSet.Object);
            var repository = new CauseRepository(_rcaDbContext.Object);

            // Act
            var res = repository.GetCauseById(causeId);

            // Assert
            res.Should().NotBeNull();
            _rcaDbContext.Verify(x => x.Causes, Times.Once);
        }

        [Fact]
        public void CreateCause_Always_ReturnsData_From_DbContextOnce()
        {
            // Arrange
            var testCauses = GetCauses();
            var testCausesDbSet = MockDbSet.CreateMockDbSet(testCauses.AsQueryable());
            _rcaDbContext.Setup(x => x.Causes).Returns(testCausesDbSet.Object);
            var sut = new CauseRepository(_rcaDbContext.Object);

            // Act
            sut.CreateCause(It.IsAny<Cause>());

            // Assert
            _rcaDbContext.Verify(x => x.Causes, Times.Once);
        }

        [Fact]
        public void GetClassificationCauses_Always_ReturnsData_From_DbContextOnce()
        {
            // Arrange
            var testCauses = GetCauses();
            var rcaId = testCauses.First().RCAId;
            var classificationId = testCauses.First().ClassificationId.Value;
            testCauses.First().IsDeleted = false;
            var testCausesDbSet = MockDbSet.CreateMockDbSet(testCauses.AsQueryable());
            testCausesDbSet.Setup(x => x.Include(It.IsAny<string>())).Returns(testCausesDbSet.Object);
            _rcaDbContext.Setup(x => x.Causes).Returns(testCausesDbSet.Object);
            var sut = new CauseRepository(_rcaDbContext.Object);

            // Act
            var res = sut.GetClassificationCauses(rcaId, classificationId);

            // Assert
            res.Should().NotBeNull();
            _rcaDbContext.Verify(x => x.Causes, Times.Once);
        }

        #region Private Methods

        private List<Cause> GetCauses()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize<Cause>(o => o
            .Without(foo => foo.Classification));
            var causeConfigurations = fixture.CreateMany<Cause>(2);
            causeConfigurations.ToList().ForEach(x => x.IsDeleted = false);
            return causeConfigurations.ToList();
        }

        #endregion
    }
}
