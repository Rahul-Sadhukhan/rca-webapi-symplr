// <copyright file="ParticipantRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal class ParticipantRepository : IParticipantRepository
    {
        private readonly IRcaDbContext _dbContext;

        public ParticipantRepository(IRcaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns all existing Participants .
        /// </summary>
        /// <returns>List of existing Participants.</returns>
        public List<Participant> GetAllParticipants()
        {
            return _dbContext.Participants.ToList();
        }

        /// <summary>
        /// Add all the Participants who are not present in participant table.
        /// </summary>
        /// <param name="participantList">List of new participants to be added.</param>
        /// <returns>List of added Participants.</returns>
        public IEnumerable<Participant> AddParticipants(IEnumerable<Participant> participantList)
        {
            return _dbContext.Participants.AddRange(participantList);
        }
    }
}
