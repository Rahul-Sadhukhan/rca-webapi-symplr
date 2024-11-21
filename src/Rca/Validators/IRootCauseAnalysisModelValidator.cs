// <copyright file="IRootCauseAnalysisModelValidator.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using FluentValidation;
using Rca.Models;

namespace Rca.Validators
{
    internal interface IRootCauseAnalysisModelValidator : IValidator<CreateRCAModel>
    {
    }
}
