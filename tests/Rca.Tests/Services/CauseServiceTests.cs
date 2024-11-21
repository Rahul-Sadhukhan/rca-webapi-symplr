// <copyright file="CauseServiceTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Orion.Common.Exceptions;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Data.Repositories;
using Rca.Enums;
using Rca.Models;
using Rca.Services;
using Xunit;

namespace Rca.Tests.Services
{
    public class CauseServiceTests : BaseTest
    {
        #region Get Classifications

        [Theory]
        [AutoMoqData]
        internal void GetClassifications_ReturnsClassificationsList(
         [Frozen] Mock<IClassificationRepository> classificationRepositoryMock,
         CauseService sut,
         IEnumerable<Classification> classificationsList)
        {
            // Arrange
            classificationRepositoryMock.Setup(x => x.GetClassifications()).Returns(classificationsList);

            // Act
            var result = sut.GetClassifications();

            // Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region Get Cause By Id

        [Theory]
        [AutoMoqData]
        internal void GetCause_ValidateParamters_Returns_CauseModel(
           [Frozen] Mock<ICauseRepository> causeRepositoryMock,
           Cause causeEntity,
           CauseService sut)
        {
            // Arrange
            var causeId = 1;
            causeRepositoryMock.Setup(x => x.DoesCauseExist(causeId)).Returns(true);
            causeRepositoryMock.Setup(x => x.GetCauseById(causeId)).Returns(causeEntity);

            // Act
            var result = sut.GetCauseById(causeId);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetCause_InvalidCauseId_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           CauseService sut)
        {
            // Arrange
            var causeId = -1;
            var msg = "Cause id is invalid.";
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.GetCauseById(causeId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void GetCause_CauseIdNotExist_Throws_NotFoundException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<ICauseRepository> causeRepositoryMock,
           CauseService sut)
        {
            // Arrange
            var causeId = 1;
            var msg = "Cause id does not exist.";
            causeRepositoryMock.Setup(x => x.DoesCauseExist(causeId)).Returns(false);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<NotFoundException>(() => sut.GetCauseById(causeId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        #endregion

        #region Create Cause

        [Theory]
        [AutoMoqData]
        internal void CreatCause_ValidParameter_ValidCauseModel_ReturnSuccess(
           [Frozen] Mock<IValidator<CauseModel>> causeModelValidatorMock,
           [Frozen] Mock<ValidationResult> validationResultMock,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           [Frozen] Mock<IRcaUnitOfWork> rcaUnitOfWorkMock,
           CauseModel causeModel,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            causeModel.RCAId = 1;
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            validationResultMock.Setup(x => x.IsValid).Returns(true);
            causeModelValidatorMock.Setup(x => x.Validate(causeModel)).Returns(validationResultMock.Object);

            // Act
            sut.CreateCause(rcaId, causeModel);

            // Assert
            rcaUnitOfWorkMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        internal void CreatCause_ValidRCAId__Model_NULL_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           CauseModel causeModel,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            causeModel = null;
            var msg = "RCA id and description are required for creating a cause.";
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.CreateCause(rcaId, causeModel));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void CreatCause_InvalidRCAId_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           CauseModel causeModel,
           CauseService sut)
        {
            // Arrange
            var rcaId = -1;
            var msg = "RCA id is invalid.";
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.CreateCause(rcaId, causeModel));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void CreatCause_RCAIdNotExist_Throws_NotFoundxception(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           CauseModel causeModel,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var msg = "RCA id does not exist.";
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(false);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<NotFoundException>(() => sut.CreateCause(rcaId, causeModel));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void CreatCause_ValidRCAIdNot_CauseModel_InvalidRCAId_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<IValidator<CauseModel>> causeModelValidatorMock,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           CauseModel causeModel,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            causeModel.RCAId = -1;
            var msg = "RCA id is invalid.";
            ValidationFailure validationFailure = new ValidationFailure("RCAId", msg);
            var validationErrors = new ValidationResult() { Errors = { validationFailure } };
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            causeModelValidatorMock.Setup(x => x.Validate(causeModel)).Returns(validationErrors);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.CreateCause(rcaId, causeModel));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void CreatCause_ParameterRCAId_CauseModelRCAId_Mismatch_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           CauseModel causeModel,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            causeModel.RCAId = 5;
            var msg = "RCA id is not matching with parameter rcaId.";
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.CreateCause(rcaId, causeModel));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        #endregion

        #region Get RCA Classification Causes

        [Theory]
        [AutoMoqData]
        internal void GetRCAClassificationCauses_ValidParameters_Returns_CauseSelectListModel_WithCauses(
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           [Frozen] Mock<IClassificationRepository> classificationRepositoryMock,
           [Frozen] Mock<ICauseRepository> causeRepositoryMock,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var classificationId = 5;
            var causes = new List<Cause>()
            {
                new Cause() { CauseId = 1, RCAId = rcaId, Description = "Cause 1", ClassificationId = classificationId, ParentCauseId = null },
            };
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            classificationRepositoryMock.Setup(x => x.DoesClassificationExist(classificationId)).Returns(true);
            causeRepositoryMock.Setup(x => x.GetClassificationCauses(rcaId, classificationId)).Returns(causes);

            // Act
            var result = sut.GetRCAClassificationCauses(rcaId, classificationId);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetRCAClassificationCauses_ValidParameters_Returns_CauseSelectListModel_WithSubCauses(
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           [Frozen] Mock<IClassificationRepository> classificationRepositoryMock,
           [Frozen] Mock<ICauseRepository> causeRepositoryMock,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var classificationId = 5;
            var causes = new List<Cause>()
            {
                new Cause() { CauseId = 1, RCAId = rcaId, Description = "Cause 1", ClassificationId = classificationId, ParentCauseId = null },
                new Cause() { CauseId = 2, RCAId = rcaId, Description = "Sub-cause 1", ClassificationId = classificationId, ParentCauseId = 1 },
            };
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            classificationRepositoryMock.Setup(x => x.DoesClassificationExist(classificationId)).Returns(true);
            causeRepositoryMock.Setup(x => x.GetClassificationCauses(rcaId, classificationId)).Returns(causes);

            // Act
            var result = sut.GetRCAClassificationCauses(rcaId, classificationId);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetRCAClassificationCauses_InvalidRCAId_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           CauseService sut)
        {
            // Arrange
            var rcaId = -1;
            var classificationId = 5;
            var msg = "RCA id is invalid.";
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.GetRCAClassificationCauses(rcaId, classificationId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void GetRCAClassificationCauses_InvalidClassificationId_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var classificationId = -5;
            var msg = "Classification id is invalid.";
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.GetRCAClassificationCauses(rcaId, classificationId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void GetRCAClassificationCauses_RCAIdNotExist_Throws_NotFoundxception(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var classificationId = 5;
            var msg = "RCA id does not exist.";
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(false);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<NotFoundException>(() => sut.GetRCAClassificationCauses(rcaId, classificationId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void GetRCAClassificationCauses_ClassificationIdNotExist_Throws_NotFoundxception(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           [Frozen] Mock<IClassificationRepository> classificationRepositoryMock,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var classificationId = 5;
            var msg = "Classification id does not exist.";
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            classificationRepositoryMock.Setup(x => x.DoesClassificationExist(classificationId)).Returns(false);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<NotFoundException>(() => sut.GetRCAClassificationCauses(rcaId, classificationId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        #endregion

        #region Get Classifications By RCAId

        [Theory]
        [AutoMoqData]
        internal void GetClassificationsByRCAId_ValidRCAId_Returns_CLassificationModel(
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           [Frozen] Mock<IClassificationRepository> classificationRepositoryMock,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var classifications = new List<Classification>()
            {
                new Classification() { ClassificationId = 1, ClassificationName = "classification 1" },
                new Classification() { ClassificationId = 2, ClassificationName = "classification 2" },
            };
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(true);
            classificationRepositoryMock.Setup(x => x.GetClassificationsByRCAId(rcaId)).Returns(classifications);

            // Act
            var result = sut.GetClassificationsByRCAId(rcaId);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetClassificationsByRCAId_InvalidRCAId_Throws_ValidationException(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           CauseService sut)
        {
            // Arrange
            var rcaId = -1;
            var msg = "RCA id is invalid.";
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<ValidationPresentationException>(() => sut.GetClassificationsByRCAId(rcaId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        [Theory]
        [AutoMoqData]
        internal void GetClassificationsByRCAId_RCAIdNotExist_Throws_NotFoundxception(
           [Frozen] Mock<ISoftLabelResourceService> mockSoftLabelResourceService,
           [Frozen] Mock<IRootCauseAnalysisRepository> rootCauseAnalysisMock,
           CauseService sut)
        {
            // Arrange
            var rcaId = 1;
            var msg = "RCA id does not exist.";
            rootCauseAnalysisMock.Setup(x => x.DoesRCAExist(rcaId)).Returns(false);
            mockSoftLabelResourceService.Setup(x => x.GetSoftLabel(It.IsAny<ValidationMessage>())).Returns(msg);

            // Act & Assert
            var result = Assert.Throws<NotFoundException>(() => sut.GetClassificationsByRCAId(rcaId));
            AssertExceptionMessage(result.ErrorModel, msg);
        }

        #endregion
    }
}
