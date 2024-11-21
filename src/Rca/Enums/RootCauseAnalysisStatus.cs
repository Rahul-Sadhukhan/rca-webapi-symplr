// <copyright file="RootCauseAnalysisStatus.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Enums
{
    public enum RootCauseAnalysisStatus : byte
    {
        /// <summary>
        /// Indicates open state.
        /// </summary>
        Open = 1,

        /// <summary>
        /// Indicates InProgress state.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Indicates Closed state.
        /// </summary>
        Closed = 3,
    }
}
