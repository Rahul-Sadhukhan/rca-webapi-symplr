// <copyright file="RootCauseAnalysisServiceTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Orion.Common.Enums;
using Orion.Common.Exceptions;
using Orion.Multilingual;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Orion.Tests.Helpers;
using Orion.Users;
using Orion.Users.Models;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Data.Repositories;
using Rca.Models;
using Rca.Models.OData;
using Rca.Services;
using Rca.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rca.Tests.Services
{
    public class RootCauseAnalysisServiceTests : BaseTest
    {
        private readonly Mock<IRcaUnitOfWork> _mockRcaUnitOfWork;
        private readonly Mock<IRootCauseAnalysisModelValidator> _mockValidator;
        private readonly Mock<IUsersComponent> _mockUserComponent;
        private readonly Mock<ISoftLabelResourceService> _mockSoftLabelService;
        private readonly RootCauseAnalysisService _service;
        private readonly Mock<IMultilingualComponent> _mockMultilingualComponent;

        public RootCauseAnalysisServiceTests()
        {
            _mockRcaUnitOfWork = new Mock<IRcaUnitOfWork>();
            _mockValidator = new Mock<IRootCauseAnalysisModelValidator>();
            _mockUserComponent = new Mock<IUsersComponent>();
            _mockSoftLabelService = new Mock<ISoftLabelResourceService>();
            _mockMultilingualComponent = new Mock<IMultilingualComponent>();

            // Set up default user for _mockUserComponent
            _mockUserComponent.Setup(uc => uc.CurrentUser).Returns(new OrionUserModel { Id = 5 });

            // Create the service instance with mocks
            _service = new RootCauseAnalysisService(_mockRcaUnitOfWork.Object, _mockValidator.Object, _mockUserComponent.Object, _mockSoftLabelService.Object, _mockMultilingualComponent.Object);
        }
        #region Get RootCauseAnalysis By Id

        [Theory]
        [AutoMoqData]
        internal void GetRootCauseAnalysisById_ReturnsSuccess(
          [Frozen] Mock<IRootCauseAnalysisRepository> mockRootCauseAnalysisRepository,
          RootCauseAnalysisService sut)
        {
            // Arrange
            mockRootCauseAnalysisRepository.Setup(x => x.GetRootCauseAnalysisById(It.IsAny<int>())).Returns(new RootCauseAnalysis { RcaId = 1 });

            // Act
            var result = sut.GetRootCauseAnalysisById(1);

            // assert
            result.Should().NotBeNull();
        }

        #endregion

        [Theory]
        [AutoMoqData]
        internal void CreateRootCauseAnalysis_ShouldThrowValidationException_WhenRCANameIsNotGiven(
         RootCauseAnalysisService sut,
         [Frozen] Mock<IValidator<CreateRCAModel>> createModelValidatorMock,
         [Frozen] Mock<ValidationResult> validationResultMock,
         CreateRCAModel model)
        {
            model.RcaName = string.Empty;
            model.IncidentDate = DateTimeOffset.UtcNow;

            var validationResult = new ValidationResult(new List<ValidationFailure>
                 {
                     new ValidationFailure("RcaName", "RcaName is required."),
                 });

            validationResultMock.Setup(x => x.IsValid).Returns(false);

            createModelValidatorMock.Setup(v => v.Validate(model)).Returns(validationResult);

            // Act & Assert
            Assert.Throws<ValidationPresentationException>(() => sut.CreateRootCauseAnalysis(model));
        }

        [Theory]
        [AutoMoqData]
        internal void CreateRootCauseAnalysis_ShouldThrowValidationException_WhenDescriptionIsNotGiven(
         RootCauseAnalysisService sut,
         [Frozen] Mock<IValidator<CreateRCAModel>> createModelValidatorMock,
         [Frozen] Mock<ValidationResult> validationResultMock,
         CreateRCAModel model)
        {
            model.Description = string.Empty;
            model.IncidentDate = DateTimeOffset.UtcNow;

            var validationResult = new ValidationResult(new List<ValidationFailure>
                 {
                     new ValidationFailure("Description", "ProblemStatement is required."),
                 });

            validationResultMock.Setup(x => x.IsValid).Returns(false);

            createModelValidatorMock.Setup(v => v.Validate(model)).Returns(validationResult);

            // Act & Assert
            Assert.Throws<ValidationPresentationException>(() => sut.CreateRootCauseAnalysis(model));
        }

        [Theory]
        [AutoMoqData]
        internal void CreateRootCauseAnalysis_ShouldThrowValidationException_WhenIncidentDateIsNotUTCFormat(
        RootCauseAnalysisService sut,
        [Frozen] Mock<IValidator<CreateRCAModel>> createModelValidatorMock,
        [Frozen] Mock<ValidationResult> validationResultMock,
        CreateRCAModel model)
        {
            model.IncidentDate = DateTimeOffset.Now;

            var validationResult = new ValidationResult(new List<ValidationFailure>
                 {
                     new ValidationFailure("IncidentDate", "IncidentDate is invalid."),
                 });

            validationResultMock.Setup(x => x.IsValid).Returns(false);

            createModelValidatorMock.Setup(v => v.Validate(model)).Returns(validationResult);

            // Act & Assert
            Assert.Throws<ValidationPresentationException>(() => sut.CreateRootCauseAnalysis(model));
        }

        #region Get RootCauseAnalysisDetails
        [Theory]
        [AutoMoqData]
        internal void CreateRootCauseAnalysis_ShouldThrowError_WhenModelIsValid_ButDuplicateParticipants(
        RootCauseAnalysisService sut,
        [Frozen] Mock<IValidator<CreateRCAModel>> createModelValidatorMock,
        [Frozen] Mock<ValidationResult> validationResultMock,
        CreateRCAModel model)
        {
            // Arrange
            model = new CreateRCAModel
            {
                RcaName = "Test RCA",
                Theme = "Test Theme",
                Description = "Test Description",
                IncidentDate = DateTimeOffset.UtcNow,
                Participants = new List<CreateParticipantModel>
        {
            new CreateParticipantModel { ParticipantName = "Participant 1", ParticipantId = 0 },
            new CreateParticipantModel { ParticipantName = "Participant 1", ParticipantId = 0 }, // Duplicate
        },
                ItemIds = new List<long> { 1 },
            };
            var validationResult = new ValidationResult(new List<ValidationFailure>
                 {
                     new ValidationFailure("Participants", "Duplicate participants are not allowed."),
                 });

            // Setup for adding participants
            validationResultMock.Setup(x => x.IsValid).Returns(false);

            createModelValidatorMock.Setup(v => v.Validate(model)).Returns(validationResult);

            // Act & Assert
            Assert.Throws<ValidationPresentationException>(() => sut.CreateRootCauseAnalysis(model));
        }

        [Theory]
        [AutoMoqData]
        internal void GetRootCauseAnalysisDetails_ReturnsSuccess(
          [Frozen] Mock<IRootCauseAnalysisRepository> mockRootCauseAnalysisRepository,
          [Frozen] Mock<IUsersComponent> mockUserComponent,
          RootCauseAnalysisService sut)
        {
            // Arrange
            mockUserComponent.Setup(x => x.CurrentUser).Returns(new Orion.Users.Models.OrionUserModel { Id = 1 });
            mockRootCauseAnalysisRepository.Setup(x => x.GetRootCauseAnalysisDetails(It.IsAny<int>(), It.IsAny<long>()))
                .Returns(new List<RootCauseAnalysisDetails>());

            // Act
            var result = sut.GetRootCauseAnalysisDetails(1);

            // assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoMoqData]
        internal void GetRootCauseAnalysisDetails_InvalidRCAId_ThrowsError(
         [Frozen] Mock<IRootCauseAnalysisRepository> mockRootCauseAnalysisRepository,
         [Frozen] Mock<IUsersComponent> mockUserComponent,
         RootCauseAnalysisService sut)
        {
            // Arrange
            mockUserComponent.Setup(x => x.CurrentUser).Returns(new Orion.Users.Models.OrionUserModel { Id = 1 });
            mockRootCauseAnalysisRepository.Setup(x => x.GetRootCauseAnalysisDetails(It.IsAny<int>(), It.IsAny<long>()))
                .Returns(new List<RootCauseAnalysisDetails>());

            // Act & Assert
            Assert.Throws<ValidationPresentationException>(() => sut.GetRootCauseAnalysisDetails(-1));
        }
        #endregion

        [Fact]
        internal void CreateRootCauseAnalysis_ShouldReturnSuccess_WhenModelIsValid()
        {
            // Arrange
            var model = new CreateRCAModel
            {
                RcaName = "Test RCA",
                Theme = "Test Theme",
                Description = "Test Description",
                IncidentDate = DateTimeOffset.UtcNow,
                Participants = new List<CreateParticipantModel>
         {
             new CreateParticipantModel { ParticipantName = "Participant 1", ParticipantId = 0 },
             new CreateParticipantModel { ParticipantName = "Participant 1", ParticipantId = 0 },
         },
                ItemIds = new List<long> { 1 },
            };

            List<ItemODataModel> mockItemODataModel = new List<ItemODataModel> { new ItemODataModel { ItemId = 1 } };

            var rootCauseAnalysis = new RootCauseAnalysis { RcaId = 1 };

            _mockValidator.Setup(v => v.Validate(It.IsAny<CreateRCAModel>())).Returns(new ValidationResult());
            _mockRcaUnitOfWork.Setup(u => u.RootCauseAnalysisRepository.AddRootCauseAnalysis(It.IsAny<RootCauseAnalysis>()))
                                                                    .Callback<RootCauseAnalysis>(rca => rca.RcaId = 1)
                                                                    .Returns((RootCauseAnalysis rca) => rca);
            _mockRcaUnitOfWork.Setup(u => u.ParticipantRepository.GetAllParticipants()).Returns(new List<Participant>
                                                                                         {
                                                                                             new Participant
                                                                                             { ParticipantId = 1, ParticipantName = "test" },
                                                                                         });
            _mockRcaUnitOfWork.Setup(u => u.ParticipantRepository.AddParticipants(It.IsAny<IEnumerable<Participant>>()))
                                                                    .Callback<IEnumerable<Participant>>(participants =>
                                                                    {
                                                                        foreach (var participant in participants)
                                                                        {
                                                                            participant.ParticipantId = 2;
                                                                        }
                                                                    })
                                                                    .Returns((List<Participant> participant) => participant);
            _mockRcaUnitOfWork.Setup(u => u.RootCauseParticipantRepository.AddRootCauseParticipants(It.IsAny<IEnumerable<RootCauseParticipant>>()))
                                                                    .Callback<IEnumerable<RootCauseParticipant>>(participants =>
                                                                    {
                                                                        foreach (var participant in participants)
                                                                        {
                                                                            participant.RootCauseParticipantId = 1;
                                                                            participant.ParticipantId = 2;
                                                                            participant.RCAId = 1;
                                                                        }
                                                                    })
                                                                    .Returns((List<RootCauseParticipant> participant) => participant);
            _mockRcaUnitOfWork.Setup(u => u.ItemQueryableRepository.GetItemQueryableByUserId(_mockUserComponent.Object.CurrentUser.Id)).Returns(mockItemODataModel.AsQueryable()).Verifiable();
            _mockRcaUnitOfWork.Setup(u => u.IncidentRepository.AddIncidents(It.IsAny<IEnumerable<Incident>>()))
                                                                    .Returns(It.IsAny<IEnumerable<Incident>>());
            _mockRcaUnitOfWork.Setup(u => u.SaveChanges());

            // Act
            var result = _service.CreateRootCauseAnalysis(model);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal(1, result.Id);

            _mockRcaUnitOfWork.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        internal void CreateRootCauseAnalysis_ShouldReturnSuccess_WhenItemsAndParticipantsAreNull()
        {
            // Arrange
            var model = new CreateRCAModel
            {
                RcaName = "Test RCA",
                Theme = "Test Theme",
                Description = "Test Description",
                IncidentDate = DateTimeOffset.UtcNow,
                Participants = null,
                ItemIds = null,
            };

            List<ItemODataModel> mockItemODataModel = new List<ItemODataModel> { new ItemODataModel { ItemId = 1 } };

            var rootCauseAnalysis = new RootCauseAnalysis { RcaId = 1 };

            _mockValidator.Setup(v => v.Validate(It.IsAny<CreateRCAModel>())).Returns(new ValidationResult());
            _mockRcaUnitOfWork.Setup(u => u.RootCauseAnalysisRepository.AddRootCauseAnalysis(It.IsAny<RootCauseAnalysis>()))
                                                                    .Callback<RootCauseAnalysis>(rca => rca.RcaId = 1)
                                                                    .Returns((RootCauseAnalysis rca) => rca);
            _mockRcaUnitOfWork.Setup(u => u.ParticipantRepository.GetAllParticipants()).Returns(new List<Participant>
                                                                                         { new Participant
                                                                                             { ParticipantId = 1, ParticipantName = "test" },
                                                                                         });
            _mockRcaUnitOfWork.Setup(u => u.ItemQueryableRepository.GetItemQueryableByUserId(_mockUserComponent.Object.CurrentUser.Id)).Returns(mockItemODataModel.AsQueryable()).Verifiable();
            _mockRcaUnitOfWork.Setup(u => u.SaveChanges());

            // Act
            var result = _service.CreateRootCauseAnalysis(model);

            // Assert
            _mockRcaUnitOfWork.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }

        [Fact]
        internal void CreateRootCauseAnalysis_ShouldReturnSuccess_WhenItemsAndParticipantsAreNull_AndIncidentDateNotGiven()
        {
            // Arrange
            var model = new CreateRCAModel
            {
                RcaName = "Test RCA",
                Theme = "Test Theme",
                Description = "Test Description",
                IncidentDate = null,
                Participants = null,
                ItemIds = null,
            };

            List<ItemODataModel> mockItemODataModel = new List<ItemODataModel> { new ItemODataModel { ItemId = 1 } };

            var rootCauseAnalysis = new RootCauseAnalysis { RcaId = 1 };

            _mockValidator.Setup(v => v.Validate(It.IsAny<CreateRCAModel>())).Returns(new ValidationResult());
            _mockRcaUnitOfWork.Setup(u => u.RootCauseAnalysisRepository.AddRootCauseAnalysis(It.IsAny<RootCauseAnalysis>()))
                                                                    .Callback<RootCauseAnalysis>(rca => rca.RcaId = 1)
                                                                    .Returns((RootCauseAnalysis rca) => rca);
            _mockRcaUnitOfWork.Setup(u => u.ParticipantRepository.GetAllParticipants()).Returns(new List<Participant>
                                                                                         { new Participant
                                                                                             { ParticipantId = 1, ParticipantName = "test" },
                                                                                         });
            _mockRcaUnitOfWork.Setup(u => u.ItemQueryableRepository.GetItemQueryableByUserId(_mockUserComponent.Object.CurrentUser.Id)).Returns(mockItemODataModel.AsQueryable()).Verifiable();
            _mockRcaUnitOfWork.Setup(u => u.SaveChanges());

            // Act
            var result = _service.CreateRootCauseAnalysis(model);

            // Assert
            _mockRcaUnitOfWork.Verify(u => u.SaveChanges(), Times.AtLeastOnce);
        }
    }
}
