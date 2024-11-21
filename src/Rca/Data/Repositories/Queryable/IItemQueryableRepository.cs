// <copyright file="IItemQueryableRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Models.OData;

namespace Rca.Data.Repositories.Queryable
{
    /// <summary>
    /// Repository interface to retrieve item data in OData format.
    /// </summary>
    internal interface IItemQueryableRepository
    {
        IQueryable<ItemODataModel> GetItemQueryableByUserId(long userId);
    }
}
