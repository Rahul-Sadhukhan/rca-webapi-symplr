// <copyright file="RootCauseAnalysisModelValidator.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using FluentValidation;
using FluentValidation.Validators;
using Orion.Common.Exceptions;
using Orion.Multilingual;
using Orion.Multilingual.Enums;
using Orion.Users;
using Rca.Data;
using Rca.Enums;
using Rca.Models;

namespace Rca.Validators
{
    internal class RootCauseAnalysisModelValidator : AbstractValidator<CreateRCAModel>, IRootCauseAnalysisModelValidator
    {
        private readonly IRcaUnitOfWork _unitOfWork;
        private readonly IMultilingualComponent _multilingualComponent;
        private readonly IUsersComponent _usersComponent;

        public RootCauseAnalysisModelValidator(
            IMultilingualComponent multilingualComponent,
            IUsersComponent usersComponent,
            IRcaUnitOfWork unitOfWork)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            _unitOfWork = unitOfWork;
            _multilingualComponent = multilingualComponent;
            _usersComponent = usersComponent;
            RuleFor(x => x.RcaName)
                .NotNull().WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.RCA_NAME_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .NotEmpty().WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.RCA_NAME_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .Must(rcaname => !string.IsNullOrWhiteSpace(rcaname)).WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.RCA_NAME_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .MaximumLength(100).WithMessage(x => string.Format($"" + multilingualComponent.GetSoftLabel(FieldLabel.NAME.ToString(), ResourceType.FieldLabel, usersComponent.CurrentUser.Language) + ": " + multilingualComponent.GetSoftLabel(ValidationMessage.VALIDATION_MAXLENGTH.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language), 100));
            RuleFor(x => x.Theme)
                .MaximumLength(100).WithMessage(x => string.Format($"" + multilingualComponent.GetSoftLabel(FieldLabel.THEME.ToString(), ResourceType.FieldLabel, usersComponent.CurrentUser.Language) + ": " + multilingualComponent.GetSoftLabel(ValidationMessage.VALIDATION_MAXLENGTH.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language), 100));
            RuleFor(x => x.Description)
                .NotNull().WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.PROBLEM_STATEMENT_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .NotEmpty().WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.PROBLEM_STATEMENT_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .Must(description => !string.IsNullOrWhiteSpace(description)).WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.RCA_NAME_IS_REQUIRED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .MaximumLength(1000).WithMessage(x => string.Format($"" + multilingualComponent.GetSoftLabel(FieldLabel.PROBLEM_STATEMENT.ToString(), ResourceType.FieldLabel, usersComponent.CurrentUser.Language) + ": " + multilingualComponent.GetSoftLabel(ValidationMessage.VALIDATION_MAXLENGTH.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language), 1000));
            RuleFor(x => x.IncidentDate)
                .Must(IsValidUTCFormat).WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.INCIDENT_DATE_INVALID_UTC_FORMAT.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language))
                .Must(IsValidIncidentDate).WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.INCIDENT_DATE_MUST_NOT_BE_FUTURE_DATE.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language));
            RuleForEach(x => x.Participants)
                .SetValidator(new ParticipantValidator(_multilingualComponent, _usersComponent));
            RuleFor(x => x.Participants)
                .Must(participants =>
                {
                    var result = IsDuplicateParticipantsFound(participants);
                    return result.Item1;
                }).WithMessage(x =>
                {
                    var result = IsDuplicateParticipantsFound(x.Participants);
                    return string.Format($"" + multilingualComponent.GetSoftLabel(ValidationMessage.DUPLICATE_PARTICIPANT_NOT_ALLOWED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language), result.Item2);
                });
            RuleFor(x => x.ItemIds)
                .Must(IsDuplicateItemFound).WithMessage(x => multilingualComponent.GetSoftLabel(ValidationMessage.DUPLICATE_ITEMS_NOT_ALLOWED.ToString(), ResourceType.ValidationMessage, usersComponent.CurrentUser.Language));
        }

        private bool IsValidIncidentDate(DateTimeOffset? incidentDate)
        {
            if (incidentDate == null)
            {
                return true;
            }

            if (incidentDate.HasValue)
            {
                return incidentDate.Value.Date <= DateTimeOffset.UtcNow.Date;
            }

            return false;
        }

        private bool IsValidUTCFormat(DateTimeOffset? incidentDate)
        {
            if (incidentDate == null)
            {
                return true;
            }

            if (incidentDate.HasValue)
            {
                return incidentDate.Value.Offset == TimeSpan.Zero;
            }

            return false;
        }

        private bool IsDuplicateItemFound(IEnumerable<long> itemList)
        {
            if (itemList != null && itemList.Any())
            {
                var seenItems = new HashSet<long>();
                foreach (var item in itemList)
                {
                    if (!seenItems.Add(item))
                    {
                        return false; // Duplicate found
                    }
                }
            }

            return true;
        }

        private Tuple<bool, string> IsDuplicateParticipantsFound(IEnumerable<CreateParticipantModel> participantList)
        {
            if (participantList != null && participantList.Any())
            {
                // Check for duplicate participant IDs (greater than zero)
                var duplicateParticipantIds = participantList
                    .Where(p => p.ParticipantId > 0)
                    .GroupBy(p => p.ParticipantId)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
                if (duplicateParticipantIds.Any())
                {
                    return new Tuple<bool, string>(false, "ParticipantId");
                }

                // Check for duplicate participant names
                var duplicateParticipantNames = participantList
                    .Where(p => !string.IsNullOrEmpty(p.ParticipantName))
                    .GroupBy(p => p.ParticipantName.ToLower(CultureInfo.InvariantCulture))
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateParticipantNames.Any())
                {
                    return new Tuple<bool, string>(false, "ParticipantName");
                }

                var duplicateParticipants = participantList
                .GroupBy(p => new { p.ParticipantId, p.ParticipantName })
                .Where(g => g.Count() > 1)
                .Select(g => new { g.Key.ParticipantId, g.Key.ParticipantName })
                .ToList();

                if (duplicateParticipants.Any())
                {
                    return new Tuple<bool, string>(false, "Participants");
                }
            }

            return new Tuple<bool, string>(true, string.Empty);
        }
    }
}
