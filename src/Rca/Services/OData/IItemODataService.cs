// <copyright file="IItemODataService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Models.OData;

namespace Rca.Services.OData
{
    public interface IItemODataService
    {
        /// <summary>
        /// Gets a list of item models in OData format.
        /// </summary>
        /// <returns>A list of item models.</returns>
        IQueryable<ItemODataModel> GetItemQueryable();
    }
}
