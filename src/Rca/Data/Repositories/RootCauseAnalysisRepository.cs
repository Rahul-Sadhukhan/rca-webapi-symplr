// <copyright file="RootCauseAnalysisRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>
using System.Collections.Generic;
using System.Linq;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal class RootCauseAnalysisRepository : IRootCauseAnalysisRepository
    {
        private readonly IRcaDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootCauseAnalysisRepository"/> class.
        /// </summary>
        /// <param name="dbContext">db context.</param>
        public RootCauseAnalysisRepository(IRcaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RootCauseAnalysis GetRootCauseAnalysisById(long rcaId)
        {
            return _dbContext
                .RootCauseAnalysis
                .AsNoTracking()
                .Where(x => x.RcaId == rcaId && !x.IsDeleted)
                .FirstOrDefault();
        }

        /// <summary>
        /// Create Root Cause Analysis.
        /// </summary>
        /// <param name="rootCauseAnalysis">Root Cause Analysis Model.</param>
        /// <returns>created RootCauseAnalysis model.</returns>
        public RootCauseAnalysis AddRootCauseAnalysis(RootCauseAnalysis rootCauseAnalysis)
        {
            return _dbContext.RootCauseAnalysis.Add(rootCauseAnalysis);
        }

        /// <summary>
        /// Check rca exist based on rca id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>Boolean flag represents rca exist or not.</returns>
        public bool DoesRCAExist(int rcaId)
        {
            return _dbContext
                .RootCauseAnalysis
                .AsNoTracking()
                .Any(x => x.RcaId == rcaId && !x.IsDeleted);
        }

        /// <summary>
        /// Get RootCauseAnalysis details by id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="userId">user identifier.</param>
        /// <returns>RootCauseAnalysisDetails model.</returns>
        public List<RootCauseAnalysisDetails> GetRootCauseAnalysisDetails(int rcaId, long userId)
        {
            return _dbContext.GetRootCauseAnalysisDetails(rcaId, userId);
        }
    }
}
