// <copyright file="ICauseRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal interface ICauseRepository
    {
        /// <summary>
        /// Check cause exist based on cause id.
        /// </summary>
        /// <param name="causeId">Cause identifier.</param>
        /// <returns>Boolean flag represents cause exist or not.</returns>
        bool DoesCauseExist(int causeId);

        /// <summary>
        /// Gets the cause details for a provider cause id.
        /// </summary>
        /// <param name="causeId">The cause identifier.</param>
        /// <returns>Cause model.</returns>
        Cause GetCauseById(int causeId);

        /// <summary>
        /// Creates a cause.
        /// </summary>
        /// <param name="cause">Cause entity.</param>
        /// <returns>Create cause entity.</returns>
        Cause CreateCause(Cause cause);

        /// <summary>
        /// Gets a list of causes for a provided rcaId and classificationId.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>List of causes entities.</returns>
        IEnumerable<Cause> GetClassificationCauses(int rcaId, int classificationId);
    }
}
