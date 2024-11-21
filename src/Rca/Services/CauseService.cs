// <copyright file="CauseService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Orion.Common.Exceptions;
using Orion.Users;
using Rca.Data;
using Rca.Data.Entities;
using Rca.Enums;
using Rca.Models;

namespace Rca.Services
{
    internal class CauseService : ICauseService
    {
        private readonly IRcaUnitOfWork _rcaUnitOfWork;
        private readonly IUsersComponent _userComponent;
        private readonly ISoftLabelResourceService _softLabelResourceService;
        private readonly IValidator<CauseModel> _causeModelValidator;

        public CauseService(
            IRcaUnitOfWork rcaUnitOfWork,
            ISoftLabelResourceService softLabelResourceService,
            IValidator<CauseModel> causeModelValidator,
            IUsersComponent userComponent)
        {
            _rcaUnitOfWork = rcaUnitOfWork;
            _softLabelResourceService = softLabelResourceService;
            _causeModelValidator = causeModelValidator;
            _userComponent = userComponent;
        }

        /// <summary>
        /// Gets a list of classifications.
        /// </summary>
        /// <returns>List of classifications.</returns>
        public IEnumerable<ClassificationModel> GetClassifications()
        {
            var classifications = _rcaUnitOfWork.ClassificationRepository.GetClassifications();
            return Mapper.Map<List<ClassificationModel>>(classifications);
        }

        /// <summary>
        /// Gets the cause details for a provider cause id.
        /// </summary>
        /// <param name="causeId">The cause identifier.</param>
        /// <returns>Cause model.</returns>
        public CauseModel GetCauseById(int causeId)
        {
            if (causeId <= 0)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.CAUSE_ID_INVALID));
            }

            if (!_rcaUnitOfWork.CauseRepository.DoesCauseExist(causeId))
            {
                throw new NotFoundException(_softLabelResourceService.GetSoftLabel(ValidationMessage.CAUSE_ID_DOES_NOT_EXIST));
            }

            var cause = _rcaUnitOfWork.CauseRepository.GetCauseById(causeId);
            return Mapper.Map<CauseModel>(cause);
        }

        /// <summary>
        /// Creates a cause.
        /// </summary>
        /// <param name="rcaId">The rca identifier.</param>
        /// <param name="causeModel">Cause model.</param>
        public void CreateCause(int rcaId, CauseModel causeModel)
        {
            if (causeModel == null)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.CANNOT_CREATE_CAUSE_WITHOUT_REQUIRED_DATA));
            }

            if (rcaId <= 0)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_INVALID));
            }

            if (!_rcaUnitOfWork.RootCauseAnalysisRepository.DoesRCAExist(rcaId))
            {
                throw new NotFoundException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_DOES_NOT_EXIST));
            }

            var validationResult = _causeModelValidator.Validate(causeModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationPresentationException(validationResult.Errors);
            }

            if (rcaId != causeModel.RCAId)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_UNIQUE_WITHIN_PARAMETERS));
            }

            var causeEntity = Mapper.Map<Cause>(causeModel);
            causeEntity.RCAId = rcaId;
            _rcaUnitOfWork.CauseRepository.CreateCause(causeEntity);
            _rcaUnitOfWork.SaveChanges();
        }

        /// <summary>
        /// Gets a list of RCA classification causes and it's sub-causes.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>Cause select list model.</returns>
        public List<CauseSelectListModel> GetRCAClassificationCauses(int rcaId, int classificationId)
        {
            if (rcaId <= 0)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_INVALID));
            }

            if (classificationId <= 0)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.CLASSIFICATION_ID_INVALID));
            }

            if (!_rcaUnitOfWork.RootCauseAnalysisRepository.DoesRCAExist(rcaId))
            {
                throw new NotFoundException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_DOES_NOT_EXIST));
            }

            if (!_rcaUnitOfWork.ClassificationRepository.DoesClassificationExist(classificationId))
            {
                throw new NotFoundException(_softLabelResourceService.GetSoftLabel(ValidationMessage.CLASSIFICATION_ID_DOES_NOT_EXIST));
            }

            var classificationCauses = _rcaUnitOfWork.CauseRepository.GetClassificationCauses(rcaId, classificationId);
            var causeSelectList = Mapper.Map<List<CauseSelectListModel>>(classificationCauses);

            var itemsToRemove = new List<CauseSelectListModel>();
            foreach (var causeSelectModel in causeSelectList)
            {
                var subCauses = causeSelectList.Where(x => x.ParentCauseId == causeSelectModel.CauseId).ToList();
                causeSelectModel.Subcauses = subCauses;
                itemsToRemove.AddRange(subCauses);
            }

            causeSelectList.RemoveAll(data => itemsToRemove.Any(x => x.CauseId == data.CauseId));
            return causeSelectList;
        }

        /// <summary>
        /// Gets a list of classifications for a provided RCA id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>List of classifications.</returns>
        public IEnumerable<ClassificationModel> GetClassificationsByRCAId(int rcaId)
        {
            if (rcaId <= 0)
            {
                throw new ValidationPresentationException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_INVALID));
            }

            if (!_rcaUnitOfWork.RootCauseAnalysisRepository.DoesRCAExist(rcaId))
            {
                throw new NotFoundException(_softLabelResourceService.GetSoftLabel(ValidationMessage.RCA_ID_DOES_NOT_EXIST));
            }

            var classifications = _rcaUnitOfWork.ClassificationRepository.GetClassificationsByRCAId(rcaId);
            return Mapper.Map<List<ClassificationModel>>(classifications);
        }
    }
}
