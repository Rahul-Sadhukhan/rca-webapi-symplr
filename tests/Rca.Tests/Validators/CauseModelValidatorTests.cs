// <copyright file="CauseModelValidatorTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using FluentValidation.TestHelper;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Rca.Models;
using Rca.Validators;
using Srm.Tests.Validators;
using Xunit;

namespace Rca.Tests.Validators
{
    public class CauseModelValidatorTests : BaseValidatorTests
    {
        [Theory]
        [AutoMoqData]
        internal void Validate_RCAId_NotEqualToZero_ValidationError(
            CauseModelValidator<CauseModel> sut,
            CauseModel model)
        {
            // Arrange
            model.RCAId = 0;

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.RCAId, model);
        }

        [Theory]
        [AutoMoqData]
        internal void Validate_RCAId_MinusOne_ValidationError(
            CauseModelValidator<CauseModel> sut,
            CauseModel model)
        {
            // Arrange
            model.RCAId = -1;

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.RCAId, model);
        }

        [Theory]
        [AutoMoqData]
        internal void Validate_ClassificationId_MinusOne_ValidationError(
            CauseModelValidator<CauseModel> sut,
            CauseModel model)
        {
            // Arrange
            model.ClassificationId = -1;

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.ClassificationId, model);
        }

        [Theory]
        [AutoMoqData]
        internal void Validate_ParentCauseId_MinusOne_ValidationError(
            CauseModelValidator<CauseModel> sut,
            CauseModel model)
        {
            // Arrange
            model.ParentCauseId = -1;

            // Act and Assert
            sut.ShouldHaveValidationErrorFor(e => e.ParentCauseId, model);
        }
    }
}
