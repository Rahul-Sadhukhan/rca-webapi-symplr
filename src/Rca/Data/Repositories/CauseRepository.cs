// <copyright file="CauseRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Rca.Data.Entities;

    internal class CauseRepository : ICauseRepository
    {
        private readonly IRcaDbContext _rcaDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CauseRepository"/> class.
        /// </summary>
        /// <param name="rcaDbContext">Rca db context.</param>
        public CauseRepository(IRcaDbContext rcaDbContext)
        {
            _rcaDbContext = rcaDbContext;
        }

        /// <summary>
        /// Check cause exist based on cause id.
        /// </summary>
        /// <param name="causeId">Cause identifier.</param>
        /// <returns>Boolean flag represents cause exist or not.</returns>
        public bool DoesCauseExist(int causeId)
        {
            return _rcaDbContext
                .Causes
                .AsNoTracking()
                .Any(x => x.CauseId == causeId && !x.IsDeleted);
        }

        /// <summary>
        /// Gets the cause details for a provider cause id.
        /// </summary>
        /// <param name="causeId">The cause identifier.</param>
        /// <returns>Cause model.</returns>
        public Cause GetCauseById(int causeId)
        {
            return _rcaDbContext
                .Causes
                .AsNoTracking().Where(x => x.CauseId == causeId && !x.IsDeleted)
                .FirstOrDefault();
        }

        /// <summary>
        /// Creates a cause.
        /// </summary>
        /// <param name="cause">Cause entity.</param>
        /// <returns>Create cause entity.</returns>
        public Cause CreateCause(Cause cause)
        {
            return _rcaDbContext.Causes.Add(cause);
        }

        /// <summary>
        /// Gets a list of causes for a provided rcaId and classificationId.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>List of causes entities.</returns>
        public IEnumerable<Cause> GetClassificationCauses(int rcaId, int classificationId)
        {
            return _rcaDbContext
                .Causes
                .AsNoTracking()
                .Where(x => x.RCAId == rcaId && x.ClassificationId == classificationId && !x.IsDeleted)
                .OrderByDescending(x => x.CauseId)
                .AsEnumerable();
        }
    }
}
