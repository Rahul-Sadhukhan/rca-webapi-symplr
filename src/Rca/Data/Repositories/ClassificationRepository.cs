// <copyright file="ClassificationRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Rca.Constants;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal class ClassificationRepository : IClassificationRepository
    {
        private readonly IRcaDbContext _rcaDbContext;

        public ClassificationRepository(IRcaDbContext rcaDbContext)
        {
            _rcaDbContext = rcaDbContext;
        }

        /// <summary>
        /// Gets a list of classifications.
        /// </summary>
        /// <returns>List of classifications.</returns>
        public IEnumerable<Classification> GetClassifications()
        {
            return _rcaDbContext
                .Classifications
                .AsNoTracking()
                .OrderBy(x => x.ClassificationName != ClassificationConstant.NoClassification)
                .ThenBy(x => x.ClassificationName)
                .AsEnumerable();
        }

        /// <summary>
        /// Check classification exist based on classification id.
        /// </summary>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>Boolean flag represents rca exist or not.</returns>
        public bool DoesClassificationExist(int classificationId)
        {
            return _rcaDbContext
                .Classifications
                .AsNoTracking()
                .Any(x => x.ClassificationId == classificationId);
        }

        /// <summary>
        /// Gets a list of classifications for a provided RCA id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>List of classification entities.</returns>
        public IEnumerable<Classification> GetClassificationsByRCAId(int rcaId)
        {
            return _rcaDbContext
                .Classifications
                .Include(x => x.Causes)
                .AsNoTracking()
                .Where(x => x.Causes.Any(y => y.RCAId == rcaId && !y.IsDeleted))
                .OrderBy(x => x.ClassificationName != ClassificationConstant.NoClassification)
                .ThenBy(x => x.ClassificationName)
                .AsEnumerable();
        }
    }
}
