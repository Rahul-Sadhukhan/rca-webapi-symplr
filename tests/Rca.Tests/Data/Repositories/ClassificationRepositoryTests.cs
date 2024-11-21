// <copyright file="ClassificationRepositoryTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Moq;
using Orion.Tests.EntityFrameworkHelpers;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Data.Repositories;
using Xunit;

namespace Rca.Tests.Data.Repositories
{
    public class ClassificationRepositoryTests : BaseTest
    {
        private static Mock<IRcaDbContext> _rcaDbContext;

        public ClassificationRepositoryTests()
        {
            _rcaDbContext = new Mock<IRcaDbContext>();
        }

        [Fact]
        public void GetClassifications_Always_CallsDbContext_ReturnData()
        {
            // Arrange
            var testClassifications = GetClassifications();
            var classificationDbSet = MockDbSet.CreateMockDbSet(testClassifications.AsQueryable());
            classificationDbSet.Setup(x => x.Include(It.IsAny<string>())).Returns(classificationDbSet.Object);
            _rcaDbContext.Setup(x => x.Classifications).Returns(classificationDbSet.Object);
            var repository = new ClassificationRepository(_rcaDbContext.Object);

            // Act
            repository.GetClassifications();

            // Assert
            _rcaDbContext.Verify(x => x.Classifications, Times.Once);
        }

        [Fact]
        public void GetClassificationsByRCAId_Always_CallsDbContext_ReturnData()
        {
            // Arrange
            var rcaId = 5;
            var testClassifications = GetClassificationWithCauses();
            var classificationDbSet = MockDbSet.CreateMockDbSet(testClassifications.AsQueryable());
            classificationDbSet.Setup(x => x.Include(It.IsAny<string>())).Returns(classificationDbSet.Object);
            _rcaDbContext.Setup(x => x.Classifications).Returns(classificationDbSet.Object);
            var repository = new ClassificationRepository(_rcaDbContext.Object);

            // Act
            repository.GetClassificationsByRCAId(rcaId);

            // Assert
            _rcaDbContext.Verify(x => x.Classifications, Times.Once);
        }

        #region Private Methods

        private List<Classification> GetClassifications()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize<Classification>(o => o
            .Without(foo => foo.Causes));
            var classificationConfigurations = fixture.CreateMany<Classification>(2);
            return classificationConfigurations.ToList();
        }

        private List<Classification> GetClassificationWithCauses()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize<Classification>(o => o
            .With(foo => foo.Causes));
            var classificationConfigurations = fixture.CreateMany<Classification>(1);
            return classificationConfigurations.ToList();
        }

        #endregion
    }
}
