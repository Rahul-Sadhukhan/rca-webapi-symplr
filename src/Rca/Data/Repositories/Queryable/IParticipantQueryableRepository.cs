// <copyright file="IParticipantQueryableRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Models.OData;

namespace Rca.Data.Repositories.Queryable
{
    /// <summary>
    /// Repository interface to retrieve data in OData format.
    /// </summary>
    internal interface IParticipantQueryableRepository
    {
        IQueryable<ParticipantODataModel> GetParticipantQueryable();
    }
}