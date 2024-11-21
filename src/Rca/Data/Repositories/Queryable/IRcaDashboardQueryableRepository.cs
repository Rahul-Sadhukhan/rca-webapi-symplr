// <copyright file="IRcaDashboardQueryableRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Models.OData;

namespace Rca.Data.Repositories.Queryable
{
    internal interface IRcaDashboardQueryableRepository
    {
        IQueryable<RcaDashboardODataModel> GetRcaDashboardInfoQueryableByUserId(long userId);
    }
}
