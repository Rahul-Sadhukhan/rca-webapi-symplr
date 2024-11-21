// <copyright file="CauseModelValidator.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using FluentValidation;
using Rca.Enums;
using Rca.Models;
using Rca.Services;

namespace Rca.Validators
{
    internal class CauseModelValidator<T> : AbstractValidator<T>
        where T : CauseModel
    {
        public CauseModelValidator(
            ISoftLabelResourceService softLabelResourceService)
        {
            RuleFor(x => x.RCAId)
                .NotEqual(0).WithMessage(x => softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_REQUIRED))
                .GreaterThanOrEqualTo(1).WithMessage(x => softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_INVALID));

            When(x => x.ClassificationId != null, () =>
            {
                RuleFor(x => x.ClassificationId)
                .GreaterThanOrEqualTo(1)
                .WithMessage(x => softLabelResourceService.GetSoftLabel(ValidationMessage.CLASSIFICATION_ID_INVALID));
            });

            When(x => x.ParentCauseId != null, () =>
            {
                RuleFor(x => x.ParentCauseId)
                .GreaterThanOrEqualTo(1)
                .WithMessage(x => softLabelResourceService.GetSoftLabel(ValidationMessage.PARENT_CAUSE_ID_INVALID));
            });
        }
    }
}
