// <copyright file="RootCauseAnalysisRepositoryTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Moq;
using Orion.Tests.EntityFrameworkHelpers;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Data.Repositories;
using Xunit;

namespace Rca.Tests.Data.Repositories
{
    public class RootCauseAnalysisRepositoryTests
    {
        private static Mock<IRcaDbContext> _mockRcaDbContext;

        public RootCauseAnalysisRepositoryTests()
        {
            _mockRcaDbContext = new Mock<IRcaDbContext>();
        }

        [Fact]
        public void GetRootCauseAnalysisById_Calls_DbContext()
        {
            // Arrange
            var rootCauseAnalysisDbSet = MockDbSet.CreateMockDbSet(new List<RootCauseAnalysis>().AsQueryable());
            _mockRcaDbContext.Setup(x => x.RootCauseAnalysis).Returns(rootCauseAnalysisDbSet.Object);
            var sut = new RootCauseAnalysisRepository(_mockRcaDbContext.Object);

            // Act
            var result = sut.GetRootCauseAnalysisById(1);

            // Assert
            _mockRcaDbContext.Verify(x => x.RootCauseAnalysis, Times.Once);
        }

        [Fact]
        public void GetRootCauseAnalysisDetails_Calls_DbContext()
        {
            // Arrange
            _mockRcaDbContext.Setup(x => x.GetRootCauseAnalysisDetails(It.IsAny<int>(), It.IsAny<long>()));
            var sut = new RootCauseAnalysisRepository(_mockRcaDbContext.Object);

            // Act
            var result = sut.GetRootCauseAnalysisDetails(1, 1);

            // Assert
            _mockRcaDbContext.Verify(x => x.GetRootCauseAnalysisDetails(1, 1), Times.Once);
        }
    }
}
