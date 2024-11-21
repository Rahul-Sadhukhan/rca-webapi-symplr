// <copyright file="CauseControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Web.Http.Results;
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
    public class CauseControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetClassifications_CallsCauseServiceWithCorrectArguments(
           [Frozen] Mock<ICauseService> causeServiceMock)
        {
            // arrange
            using (var controller = new CauseController(causeServiceMock.Object))
            {
                // act
                var result = controller.GetClassifications();

                // assert
                causeServiceMock.Verify(x => x.GetClassifications(), Times.Once);
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkNegotiatedContentResult<IEnumerable<ClassificationModel>>));
            }
        }

        [Theory]
        [AutoMoqData]
        public void GetCauseById_CallsCauseServiceWithCorrectArguments(
            [Frozen] Mock<ICauseService> causeServiceMock)
        {
            // arrange
            using (var controller = new CauseController(causeServiceMock.Object))
            {
                // act
                var result = controller.GetCauseById(10);

                // assert
                causeServiceMock.Verify(x => x.GetCauseById(It.IsAny<int>()), Times.Once);
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkNegotiatedContentResult<CauseModel>));
            }
        }

        [Theory]
        [AutoMoqData]
        public void PostCreateCause_CallsCauseServiceWithCorrectArguments(
           [Frozen] Mock<ICauseService> causeServiceMock,
           CauseModel causeModel,
           int rcaId)
        {
            // arrange
            using (var controller = new CauseController(causeServiceMock.Object))
            {
                // act
                var result = controller.Post(rcaId, causeModel);

                // assert
                causeServiceMock.Verify(x => x.CreateCause(rcaId, causeModel), Times.Once);
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkResult));
            }
        }

        [Theory]
        [AutoMoqData]
        public void GetRCAClassificationCauses_CallsCauseServiceWithCorrectArguments(
            [Frozen] Mock<ICauseService> causeServiceMock)
        {
            // arrange
            using (var controller = new CauseController(causeServiceMock.Object))
            {
                // act
                var result = controller.GetRCAClassificationCauses(10, 10);

                // assert
                causeServiceMock.Verify(x => x.GetRCAClassificationCauses(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkNegotiatedContentResult<List<CauseSelectListModel>>));
            }
        }

        [Theory]
        [AutoMoqData]
        public void GetClassificationsByRCAId_CallsCauseServiceWithCorrectArguments(
            [Frozen] Mock<ICauseService> causeServiceMock)
        {
            // arrange
            using (var controller = new CauseController(causeServiceMock.Object))
            {
                // act
                var result = controller.GetClassificationsByRCAId(10);

                // assert
                causeServiceMock.Verify(x => x.GetClassificationsByRCAId(It.IsAny<int>()), Times.Once);
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkNegotiatedContentResult<IEnumerable<ClassificationModel>>));
            }
        }
    }
}
