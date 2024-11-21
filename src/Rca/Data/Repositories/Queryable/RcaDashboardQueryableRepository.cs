// <copyright file="RcaDashboardQueryableRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using AutoMapper.QueryableExtensions;
using Rca.Models.OData;

namespace Rca.Data.Repositories.Queryable
{
    internal class RcaDashboardQueryableRepository : IRcaDashboardQueryableRepository
    {
        private readonly IRcaDbContext _rcaDbContext;

        public RcaDashboardQueryableRepository(IRcaDbContext rcaDbContext)
        {
            _rcaDbContext = rcaDbContext;
        }

        public IQueryable<RcaDashboardODataModel> GetRcaDashboardInfoQueryableByUserId(long userId)
        {
            return _rcaDbContext
                .RCADashboardInfoViews
                .AsNoTracking().Where(rca => rca.CreatedByUserId == userId)
                .OrderByDescending(rca => rca.RcaId)
                .ProjectTo<RcaDashboardODataModel>();
        }
    }
}
