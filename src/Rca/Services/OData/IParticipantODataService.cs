// <copyright file="IParticipantODataService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Models.OData;

namespace Rca.Services.OData
{
    public interface IParticipantODataService
    {
        /// <summary>
        /// Gets a list of participant models in OData format.
        /// </summary>
        /// <returns>A list of participant models.</returns>
        IQueryable<ParticipantODataModel> GetParticipantQueryable();
    }
}
