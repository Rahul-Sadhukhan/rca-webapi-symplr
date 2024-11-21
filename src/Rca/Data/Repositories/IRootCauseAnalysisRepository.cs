// <copyright file="IRootCauseAnalysisRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal interface IRootCauseAnalysisRepository
    {
        RootCauseAnalysis GetRootCauseAnalysisById(long rcaId);

        RootCauseAnalysis AddRootCauseAnalysis(RootCauseAnalysis rootCauseAnalysis);

        /// <summary>
        /// Check rca exist based on rca id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>Boolean flag represents rca exist or not.</returns>
        bool DoesRCAExist(int rcaId);

        /// <summary>
        /// Get RootCauseAnalysis details by id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <returns>RootCauseAnalysisDetails model.</returns>
        List<RootCauseAnalysisDetails> GetRootCauseAnalysisDetails(int rcaId, long userId);
    }
}
