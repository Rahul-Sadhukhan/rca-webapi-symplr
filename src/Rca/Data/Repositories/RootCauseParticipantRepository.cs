// <copyright file="RootCauseParticipantRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal class RootCauseParticipantRepository : IRootCauseParticipantRepository
    {
        private readonly IRcaDbContext _dbContext;

        public RootCauseParticipantRepository(IRcaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add all the Participants associated with rcaid.
        /// </summary>
        /// <param name="rcaParticipantList">List of participants to be added associated with rcaid.</param>
        /// <returns>List of added Participants associated with rcaid.</returns>
        public IEnumerable<RootCauseParticipant> AddRootCauseParticipants(IEnumerable<RootCauseParticipant> rcaParticipantList)
        {
            return _dbContext.RootCauseParticipants.AddRange(rcaParticipantList);
        }
    }
}
