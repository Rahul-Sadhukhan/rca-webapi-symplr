// <copyright file="RootCauseAnalysisControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Rca.Api.Controllers;
using Rca.Models;
using Rca.Services;
using Xunit;

namespace Rca.Api.Tests.Controllers
{
    public class RootCauseAnalysisControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetRootCauseAnalysisById_Calls_RootCauseAnalysisServiceWithCorrectArguments(
        [Frozen] Mock<IRootCauseAnalysisService> rootCauseAnalysisService)
        {
            // arrange
            using (var controller = new RootCauseAnalysisController(rootCauseAnalysisService.Object))
            {
                rootCauseAnalysisService.Setup(x => x.GetRootCauseAnalysisById(It.IsAny<int>())).Returns(new RootCauseAnalysisModel());

                // act
                var result = controller.GetRootCauseAnalysisById(It.IsAny<int>());

                // assert
                rootCauseAnalysisService.Verify(x => x.GetRootCauseAnalysisById(It.IsAny<int>()), Times.Once);
                result.Should().NotBeNull();
            }
        }

        [Theory]
        [AutoMoqData]
        public void GetRootCauseAnalysisDetails_Calls_RootCauseAnalysisServiceWithCorrectArguments(
        [Frozen] Mock<IRootCauseAnalysisService> rootCauseAnalysisService)
        {
            // arrange
            using (var controller = new RootCauseAnalysisController(rootCauseAnalysisService.Object))
            {
                rootCauseAnalysisService.Setup(x => x.GetRootCauseAnalysisDetails(It.IsAny<int>())).Returns(new RootCauseAnalysisDetailModel());

                // act
                var result = controller.GetRootCauseAnalysisDetails(It.IsAny<int>());

                // assert
                rootCauseAnalysisService.Verify(x => x.GetRootCauseAnalysisDetails(It.IsAny<int>()), Times.Once);
                result.Should().NotBeNull();
            }
        }

        [Theory]
        [AutoMoqData]
        public void CreateRCA_Calls_RootCauseAnalysisServiceWithCorrectArguments(
        [Frozen] Mock<IRootCauseAnalysisService> rootCauseAnalysisService)
        {
            // arrange
            using (var controller = new RootCauseAnalysisController(rootCauseAnalysisService.Object))
            {
                rootCauseAnalysisService.Setup(x => x.CreateRootCauseAnalysis(It.IsAny<CreateRCAModel>())).Returns(new CreateRCAResponse());

                // act
                var result = controller.Create(It.IsAny<CreateRCAModel>());

                // assert
                rootCauseAnalysisService.Verify(x => x.CreateRootCauseAnalysis(It.IsAny<CreateRCAModel>()), Times.Once);
                result.Should().NotBeNull();
            }
        }
    }
}
