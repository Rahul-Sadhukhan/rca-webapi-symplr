using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Rca.Models;
using Rca.Validators;
using Xunit;

namespace Rca.Tests.Validators
{
    public class RootCauseAnalysisModelValidatorTests
    {
        [Theory]
        [AutoMoqData]
        internal void Validate_RcaName_Empty_ValidationError(
            RootCauseAnalysisModelValidator sut,
            CreateRCAModel model)
        {
            // Arrange
            model.RcaName = string.Empty;

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.RcaName, model);
        }

        [Theory]
        [AutoMoqData]
        internal void Validate_RcaName_MaximumLength_ValidationError(
            RootCauseAnalysisModelValidator sut,
            CreateRCAModel model)
        {
            // Arrange
            model.RcaName = new string('x', 101);

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.RcaName, model);
        }

        [Theory]
        [AutoMoqData]
        internal void Validate_Description_Empty_ValidationError(
            RootCauseAnalysisModelValidator sut,
            CreateRCAModel model)
        {
            // Arrange
            model.Description = string.Empty;

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.Description, model);
        }

        [Theory]
        [AutoMoqData]
        internal void Validate_IncidentDate_NoFuturedate_ValidationError(
            RootCauseAnalysisModelValidator sut,
            CreateRCAModel model)
        {
            // Arrange
            model.IncidentDate = DateTimeOffset.UtcNow.AddDays(1);

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.IncidentDate, model);
        }
    }
}
