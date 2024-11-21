// <copyright file="ParticipantODataService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Data;
using Rca.Models.OData;

namespace Rca.Services.OData
{
    internal class ParticipantODataService : IParticipantODataService
    {
        private readonly IRcaUnitOfWork _rcaUnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantODataService" /> class.
        /// </summary>
        /// <param name="rcaUnitOfWork">The unit of work.</param>
        public ParticipantODataService(IRcaUnitOfWork rcaUnitOfWork)
        {
            _rcaUnitOfWork = rcaUnitOfWork;
        }

        /// <summary>
        /// Get List of Participants in OData format.
        /// </summary>
        /// <returns>A list of ParticipantODataModel.</returns>
        public IQueryable<ParticipantODataModel> GetParticipantQueryable()
        {
            return _rcaUnitOfWork.ParticipantQueryableRepository.GetParticipantQueryable();
        }
    }
}
