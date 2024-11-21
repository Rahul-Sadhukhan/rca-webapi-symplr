// <copyright file="IncidentRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal class IncidentRepository : IIncidentRepository
    {
        private readonly IRcaDbContext _dbContext;

        public IncidentRepository(IRcaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add all the items associated with rcaid.
        /// </summary>
        /// <param name="rcaItemsList">List of items to be added associated with rcaid.</param>
        /// <returns>List of added items associated with rcaid.</returns>
        public IEnumerable<Incident> AddIncidents(IEnumerable<Incident> rcaItemsList)
        {
            return _dbContext.Incidents.AddRange(rcaItemsList);
        }

        /// <summary>
        /// validate for any invalid item in the list.
        /// </summary>
        /// <param name="rcaItemsList">List of items to be added associated with rcaid.</param>
        /// <param name="userId">User Id.</param>
        /// <returns>validations message for any invalid item in the list.</returns>
        public string ValidateItemListforRCA(string rcaItemsList, long userId)
        {
            return _dbContext.ValidateItemListforRCA(rcaItemsList, userId);
        }
    }
}
