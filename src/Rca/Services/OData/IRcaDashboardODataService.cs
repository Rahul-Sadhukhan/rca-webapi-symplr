// <copyright file="RCADashboardInfoView.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Rca.Models.OData;

namespace Rca.Services.OData
{
    public interface IRcaDashboardODataService
    {
        IQueryable<RcaDashboardODataModel> GetRcaDashboardInfoQueryable();
    }
}
