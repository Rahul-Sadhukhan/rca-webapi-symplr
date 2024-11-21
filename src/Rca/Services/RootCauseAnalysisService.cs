// <copyright file="RootCauseAnalysisService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using Orion.Common.Data;
using Orion.Common.Enums;
using Orion.Common.Exceptions;
using Orion.Multilingual;
using Orion.Multilingual.Enums;
using Orion.Users;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Enums;
using Rca.Models;
using Rca.Validators;

namespace Rca.Services
{
    internal class RootCauseAnalysisService : IRootCauseAnalysisService
    {
        private readonly IRcaUnitOfWork _rcaUnitOfWork;
        private readonly IRootCauseAnalysisModelValidator _validator;
        private readonly IUsersComponent _userComponent;
        private readonly ISoftLabelResourceService _softLabelResourceService;
        private readonly IMultilingualComponent _multilingualComponent;

        public RootCauseAnalysisService(IRcaUnitOfWork rcaUnitOfWork, IRootCauseAnalysisModelValidator validator, IUsersComponent userComponent, ISoftLabelResourceService softLabelResourceService, IMultilingualComponent multilingualComponent)
        {
            _rcaUnitOfWork = rcaUnitOfWork;
            _validator = validator;
            _userComponent = userComponent;
            _softLabelResourceService = softLabelResourceService;
            _multilingualComponent = multilingualComponent;
        }

        /// <summary>
        /// Get RootCauseAnalysis by id.
        /// </summary>
        /// <param name="rcaId">RCA id.</param>
        /// <returns>RootCauseAnalysis model.</returns>
        public RootCauseAnalysisModel GetRootCauseAnalysisById(int rcaId)
        {
            var rootCauseAnalysis = _rcaUnitOfWork.RootCauseAnalysisRepository.GetRootCauseAnalysisById(rcaId);

            return Mapper.Map<RootCauseAnalysisModel>(rootCauseAnalysis);
        }

        /// <summary>
        /// Create Root Cause Analysis.
        /// </summary>
        /// <param name="rootCauseAnalysisModel">Create RCA Model.</param>
        /// <returns>CreateRCAResponse model.</returns>
        public CreateRCAResponse CreateRootCauseAnalysis(CreateRCAModel rootCauseAnalysisModel)
        {
            ValidateCreateRCAModel(rootCauseAnalysisModel);
            try
            {
                using (var scope = new OrionTransactionScope())
                {
                    var rootCauseAnalysis = new RootCauseAnalysis()
                    {
                        RcaName = rootCauseAnalysisModel.RcaName,
                        Theme = rootCauseAnalysisModel.Theme,
                        Description = rootCauseAnalysisModel.Description,
                        IncidentDate = rootCauseAnalysisModel.IncidentDate,
                        Status = (byte)RootCauseAnalysisStatus.Open,
                        CreatedByUserId = _userComponent.CurrentUser.Id,
                        LastModifiedByUserId = _userComponent.CurrentUser.Id,
                    };
                    _rcaUnitOfWork.RootCauseAnalysisRepository.AddRootCauseAnalysis(rootCauseAnalysis);
                    _rcaUnitOfWork.SaveChanges();
                    if (rootCauseAnalysis.RcaId > 0)
                    {
                        if (rootCauseAnalysisModel.Participants != null && rootCauseAnalysisModel.Participants.Any())
                        {
                            rootCauseAnalysisModel.Participants = rootCauseAnalysisModel.Participants.Where(x => IsValidParticipant(x));
                            var newParticipantList = ValidateAndAddParticipant(rootCauseAnalysisModel);
                            AddRCAParticipant(rootCauseAnalysisModel, rootCauseAnalysis.RcaId);
                        }

                        if (rootCauseAnalysisModel.ItemIds != null && rootCauseAnalysisModel.ItemIds.Any())
                        {
                            AddItemswithRCA(rootCauseAnalysisModel.ItemIds, rootCauseAnalysis.RcaId);
                        }

                        scope.Complete();
                    }

                    return new CreateRCAResponse()
                    {
                        Id = rootCauseAnalysis.RcaId,
                        Message = _multilingualComponent.GetSoftLabel(ValidationMessage.CREATE_RCA_SUCCESSFUL_MESSAGE.ToString(), ResourceType.ValidationMessage, _userComponent.CurrentUser.Language),
                    };
                }
            }
            catch (ValidationPlatformException validationPlatformException)
            {
                throw new ValidationPresentationException(validationPlatformException.ValidationResult?.Errors);
            }
            catch (ValidationPresentationException exception)
            {
                throw new ValidationPresentationException(exception.ErrorModel.Errors);
            }
            catch (Exception exception)
            {
                throw new ValidationPresentationException(exception.Message);
            }
        }

        /// <summary>
        /// Get RootCauseAnalysis details by id.
        /// </summary>
        /// <param name="rcaId">RCA id.</param>
        /// <returns>RootCauseAnalysisDetailModel model.</returns>
        public RootCauseAnalysisDetailModel GetRootCauseAnalysisDetails(int rcaId)
        {
            if (rcaId <= 0)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_INVALID));
            }

            try
            {
                var model = _rcaUnitOfWork.RootCauseAnalysisRepository.GetRootCauseAnalysisDetails(rcaId, _userComponent.CurrentUser.Id);
                return Mapper.Map<RootCauseAnalysisDetailModel>(model);
            }
            catch (SqlException ex)
            {
                if (ex.Number == (int)SqlCustomErrorCode.CustomError)
                {
                    throw new NotFoundException(ex.Message);
                }

                throw new ValidationPresentationException(ex.Message);
            }
        }

        /// <summary>
        /// Validate existing participant list and Add all the Participants who are not present in participant table.
        /// </summary>
        /// <param name="rootCauseAnalysisModel">Create RCA Model.</param>
        /// <returns>List of Participants.</returns>
        private List<Participant> ValidateAndAddParticipant(CreateRCAModel rootCauseAnalysisModel)
        {
            var existingParticipantFromDb = _rcaUnitOfWork.ParticipantRepository.GetAllParticipants();
            var existingParticipantFromModel = rootCauseAnalysisModel.Participants.Where(x => x.ParticipantId > 0);
            StringBuilder invalidExistingParticipantList = null;
            StringBuilder invalidParticipantList = null;
            List<Orion.Common.Models.FieldErrorModel> errorModel = null;
            if (existingParticipantFromModel.Any())
            {
                var invalidParticipants = existingParticipantFromModel
                                            .Where(p => !existingParticipantFromDb.Any(c => c.ParticipantId == p.ParticipantId))
                                            .Select(p => new { p.ParticipantId, p.ParticipantName });
                if (invalidParticipants.Any())
                {
                    invalidParticipantList = new StringBuilder();
                    errorModel = new List<Orion.Common.Models.FieldErrorModel>();
                    foreach (var participant in invalidParticipants)
                    {
                        invalidParticipantList.AppendFormat("{{{0}-{1}}}", participant.ParticipantId, participant.ParticipantName);
                    }

                    errorModel.Add(new Orion.Common.Models.FieldErrorModel(nameof(rootCauseAnalysisModel.Participants), string.Format($"" + _multilingualComponent.GetSoftLabel(ValidationMessage.INVALID_PARTICIPANTS.ToString(), ResourceType.ValidationMessage, _userComponent.CurrentUser.Language), invalidParticipantList)));
                }

                var invalidExistingParticipant = IsValidExistingParticipant(existingParticipantFromModel, existingParticipantFromDb);
                if (invalidExistingParticipant.Any())
                {
                    invalidExistingParticipantList = new StringBuilder();
                    errorModel = errorModel ?? new List<Orion.Common.Models.FieldErrorModel>();
                    foreach (var participant in invalidExistingParticipant)
                    {
                        invalidExistingParticipantList.AppendFormat("{{{0}-{1}}}", participant.ParticipantId, participant.ParticipantName);
                    }

                    errorModel.Add(new Orion.Common.Models.FieldErrorModel(nameof(rootCauseAnalysisModel.Participants), string.Format($"" + _multilingualComponent.GetSoftLabel(ValidationMessage.INVALID_EXISTING_PARTICIPANTS.ToString(), ResourceType.ValidationMessage, _userComponent.CurrentUser.Language), invalidExistingParticipantList)));
                }

                if (errorModel != null && errorModel.Any())
                {
                    throw new ValidationPresentationException(errorModel.ToArray());
                }
            }

            var tobeAddedParticipant = rootCauseAnalysisModel.Participants?.Where(x => x.ParticipantId == 0 && !existingParticipantFromDb.Select(y => y?.ParticipantName?.ToLower(CultureInfo.InvariantCulture))
                                                                                   .Contains(x?.ParticipantName?.ToLower(CultureInfo.InvariantCulture))).ToList();
            List<Participant> participants = null;
            if (tobeAddedParticipant != null && tobeAddedParticipant.Any())
            {
                participants = new List<Participant>();
                foreach (var participant in tobeAddedParticipant)
                {
                    participants.Add(new Participant()
                    {
                        ParticipantName = participant.ParticipantName,
                        CreatedByUserId = _userComponent.CurrentUser.Id,
                        LastModifiedByUserId = _userComponent.CurrentUser.Id,
                    });
                }

                var addedParticipant = _rcaUnitOfWork.ParticipantRepository.AddParticipants(participants);
                _rcaUnitOfWork.SaveChanges();
            }

            return participants;
        }

        /// <summary>
        /// Add all the Participants associated with the RCA Id.
        /// </summary>
        /// <param name="rootCauseAnalysisModel">Create RCA Model.</param>
        /// <param name="createdRCAId">Created RCA Id.</param>
        private void AddRCAParticipant(CreateRCAModel rootCauseAnalysisModel, int createdRCAId)
        {
            var existingParticipant = _rcaUnitOfWork.ParticipantRepository.GetAllParticipants();
            if (rootCauseAnalysisModel.Participants != null && rootCauseAnalysisModel.Participants.Any())
            {
                var tobeAddedParticipant = rootCauseAnalysisModel.Participants.Where(x => x.ParticipantId > 0).Distinct().ToList();
                var participantToBeUpdated = rootCauseAnalysisModel.Participants.Where(x => x.ParticipantId == 0);
                if (participantToBeUpdated != null && participantToBeUpdated.Any())
                {
                    var updatedParticipantList = participantToBeUpdated
                                                    .GroupJoin(
                                                        existingParticipant,
                                                        rcaParticipants => rcaParticipants?.ParticipantName?.ToLower(CultureInfo.InvariantCulture),
                                                        existingParticipants => existingParticipants?.ParticipantName?.ToLower(CultureInfo.InvariantCulture),
                                                        (rcaParticipants, gj) => new { rcaParticipants, gj })
                                                    .SelectMany(
                                                        x => x.gj.DefaultIfEmpty(),
                                                        (x, foundParticipants) => new CreateParticipantModel
                                                        {
                                                            ParticipantName = x.rcaParticipants?.ParticipantName != null ? x.rcaParticipants.ParticipantName.ToLower(CultureInfo.InvariantCulture) : null,
                                                            ParticipantId = foundParticipants != null ? foundParticipants.ParticipantId : x.rcaParticipants.ParticipantId,
                                                        });
                    tobeAddedParticipant.AddRange(updatedParticipantList);
                }

                if (tobeAddedParticipant != null && tobeAddedParticipant.Any())
                {
                    List<RootCauseParticipant> rootCauseParticipants = new List<RootCauseParticipant>();
                    foreach (var participant in tobeAddedParticipant)
                    {
                        rootCauseParticipants.Add(new RootCauseParticipant()
                        {
                            ParticipantId = participant.ParticipantId,
                            RCAId = createdRCAId,
                            CreatedByUserId = _userComponent.CurrentUser.Id,
                            LastModifiedByUserId = _userComponent.CurrentUser.Id,
                        });
                    }

                    var addedParticipant = _rcaUnitOfWork.RootCauseParticipantRepository.AddRootCauseParticipants(rootCauseParticipants);
                    _rcaUnitOfWork.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Validate the participant model to be added.
        /// </summary>
        /// <param name="participant">Participant to be added.</param>
        /// <returns>If a participant is valid.</returns>
        private bool IsValidParticipant(CreateParticipantModel participant)
        {
            return participant != null && participant.ParticipantName != null; // Add more validation conditions as needed
        }

        /// <summary>
        /// Add all the Items associated with the RCA Id.
        /// </summary>
        /// <param name="items">List of itemid associated with create RCA model.</param>
        /// <param name="createdRCAId">Created RCA Id.</param>
        private void AddItemswithRCA(IEnumerable<long> items, int createdRCAId)
        {
            List<Incident> incidents = new List<Incident>();
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    incidents.Add(new Incident()
                    {
                        ItemId = item,
                        RCAId = createdRCAId,
                        CreatedByUserId = _userComponent.CurrentUser.Id,
                        LastModifiedByUserId = _userComponent.CurrentUser.Id,
                    });
                }

                var addedParticipant = _rcaUnitOfWork.IncidentRepository.AddIncidents(incidents);
                _rcaUnitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Validate the Create RCA Model.
        /// </summary>
        /// <param name="rootCauseAnalysisModel">Participant to be added.</param>
        private void ValidateCreateRCAModel(CreateRCAModel rootCauseAnalysisModel)
        {
            if (rootCauseAnalysisModel == null)
            {
                throw new ValidationPresentationException(new Orion.Common.Models.FieldErrorModel(nameof(rootCauseAnalysisModel), _multilingualComponent.GetSoftLabel(ValidationMessage.CREATE_RCA_MODEL_NULL.ToString(), ResourceType.ValidationMessage, _userComponent.CurrentUser.Language)));
            }

            var validationResult = _validator.Validate(rootCauseAnalysisModel);

            if (!validationResult.IsValid)
            {
                throw new ValidationPresentationException(validationResult.Errors);
            }

            if (rootCauseAnalysisModel.ItemIds != null && rootCauseAnalysisModel.ItemIds.Any())
            {
                var isValidItems = IsValidItems(rootCauseAnalysisModel.ItemIds);
                if (!isValidItems.Item1)
                {
                    throw new ValidationPresentationException(new Orion.Common.Models.FieldErrorModel(nameof(rootCauseAnalysisModel.ItemIds), string.Format($"" + isValidItems.Item2)));
                }
            }
        }

        /// <summary>
        /// Validate list of items to be added.
        /// </summary>
        /// <param name="itemList">List of Items.</param>
        private Tuple<bool, string> IsValidItems(IEnumerable<long> itemList)
        {
            try
            {
                var itemListString = string.Join(",", itemList);
                var validationMessage = _rcaUnitOfWork.IncidentRepository.ValidateItemListforRCA(itemListString, _userComponent.CurrentUser.Id);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return new Tuple<bool, string>(false, validationMessage);
                }

                return new Tuple<bool, string>(true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new ValidationPresentationException(ex.Message);
            }
        }

        /// <summary>
        /// Validate list of participants to be added.
        /// </summary>
        /// <param name="participantList">List of Participants.</param>
        /// <param name="existingParticipant">List of existing Participants from db.</param>
        private IEnumerable<CreateParticipantModel> IsValidExistingParticipant(IEnumerable<CreateParticipantModel> participantList, IEnumerable<Participant> existingParticipant)
        {
            var listOfParticipantsWithName = participantList
                                                    .Where(x => !string.IsNullOrWhiteSpace(x.ParticipantName))
                                                    .Select(x => new CreateParticipantModel
                                                    {
                                                        ParticipantId = x.ParticipantId,
                                                        ParticipantName = x.ParticipantName.Trim().ToLower(CultureInfo.InvariantCulture),
                                                    }).ToList();
            var participantIds = new HashSet<int>(listOfParticipantsWithName.Select(x => x.ParticipantId));
            var dbParticipants = (existingParticipant ?? Enumerable.Empty<Participant>())
                                    .Where(dbItem => participantIds.Contains(dbItem.ParticipantId))
                                    .Select(dbItem => new
                                    {
                                        dbItem.ParticipantId,
                                        ParticipantName = dbItem.ParticipantName.Trim().ToLower(CultureInfo.InvariantCulture),
                                    }).ToList();
            var dbParticipantsInRequest = listOfParticipantsWithName.Where(listItem => dbParticipants.Select(dbItem => dbItem.ParticipantId).Contains(listItem.ParticipantId)).ToList();
            var unMatchedParticipants = dbParticipantsInRequest.Where(listItem => dbParticipants.Any() && !dbParticipants.Any(dbItem => dbItem.ParticipantId == listItem.ParticipantId
                                         && dbItem.ParticipantName.Trim().ToLower(CultureInfo.InvariantCulture) == listItem.ParticipantName.Trim().ToLower(CultureInfo.InvariantCulture))).ToList();
            return unMatchedParticipants;
        }
    }
}
