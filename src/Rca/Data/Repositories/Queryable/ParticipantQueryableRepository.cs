// <copyright file="ParticipantQueryableRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using AutoMapper.QueryableExtensions;
using Rca.Models.OData;

namespace Rca.Data.Repositories.Queryable
{
    internal class ParticipantQueryableRepository : IParticipantQueryableRepository
    {
        private readonly IRcaDbContext _rcaDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantQueryableRepository"/> class.
        /// </summary>
        /// <param name="rcaDbContext">The database context.</param>
        public ParticipantQueryableRepository(IRcaDbContext rcaDbContext)
        {
            _rcaDbContext = rcaDbContext;
        }

        /// <summary>
        /// Gets a list of participant models in OData format.
        /// </summary>
        /// <returns>A list of participant models.</returns>
        public IQueryable<ParticipantODataModel> GetParticipantQueryable()
        {
            return _rcaDbContext
            .Participants
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ProjectTo<ParticipantODataModel>();
        }
    }
}
