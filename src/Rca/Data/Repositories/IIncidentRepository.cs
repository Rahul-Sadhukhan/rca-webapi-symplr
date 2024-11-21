// <copyright file="IIncidentRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using Rca.Data.Entities;

namespace Rca.Data.Repositories
{
    internal interface IIncidentRepository
    {
        IEnumerable<Incident> AddIncidents(IEnumerable<Incident> rcaItemsList);

        string ValidateItemListforRCA(string rcaItemsList, long userId);
    }
}
