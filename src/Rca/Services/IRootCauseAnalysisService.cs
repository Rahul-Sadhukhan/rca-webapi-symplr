// <copyright file="IRootCauseAnalysisService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Rca.Models;

namespace Rca.Services
{
    public interface IRootCauseAnalysisService
    {
        RootCauseAnalysisModel GetRootCauseAnalysisById(int rcaId);

        CreateRCAResponse CreateRootCauseAnalysis(CreateRCAModel rootCauseAnalysisModel);

        /// <summary>
        /// Get RootCauseAnalysis details by id.
        /// </summary>
        /// <param name="rcaId">RCA id.</param>
        /// <returns>RootCauseAnalysisDetailModel model.</returns>
        RootCauseAnalysisDetailModel GetRootCauseAnalysisDetails(int rcaId);
    }
}
