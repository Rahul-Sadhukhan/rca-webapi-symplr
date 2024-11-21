// <copyright file="ParticipantValidator.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using FluentValidation;
using Orion.Multilingual;
using Orion.Multilingual.Enums;
using Orion.Users;
using Rca.Data;
using Rca.Enums;
using Rca.Models;

namespace Rca.Validators
{
    internal class ParticipantValidator : AbstractValidator<CreateParticipantModel>
    {
        public ParticipantValidator(
           IMultilingualComponent multilingualComponent,
           IUsersComponent usersComponent)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ParticipantName)
                .NotNull().NotEmpty().WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.PARTICIPANT_NAME_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language));
            RuleFor(x => x.ParticipantId)
                .GreaterThanOrEqualTo(0).WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.PARTICIPANT_ID_GREATER_THAN_EQUAL_ZERO.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language));
        }
    }
}
