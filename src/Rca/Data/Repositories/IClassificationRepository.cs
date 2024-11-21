// <copyright file="IClassificationRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal interface IClassificationRepository
    {
        /// <summary>
        /// Gets a list of classifications.
        /// </summary>
        /// <returns>List of classifications.</returns>
        IEnumerable<Classification> GetClassifications();

        /// <summary>
        /// Check classification exist based on classification id.
        /// </summary>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>Boolean flag represents rca exist or not.</returns>
        bool DoesClassificationExist(int classificationId);

        /// <summary>
        /// Gets a list of classifications for a provided RCA id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>List of classification entities.</returns>
        IEnumerable<Classification> GetClassificationsByRCAId(int rcaId);
    }
}
