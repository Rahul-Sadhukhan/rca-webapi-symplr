// <copyright file="RootCauseAnalysisODataController.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Orion.Api.Filters.FunctionalPermissionAttributeFilter;
using Orion.Api.Filters.UserItemSecurityApiActionFilter;
using Orion.Permissions.Enums;
using Rca.Services.OData;

namespace Rca.Api.Controllers.OData
{
    /// <summary>
    /// Rca Dashboard controller.
    /// </summary>
    /// <seealso cref="ApiController" />
    public class RcaDashboardODataController : SecureApiODataController
    {
        private readonly IRcaDashboardODataService _rcaDashboardODataService;

        public RcaDashboardODataController(IRcaDashboardODataService rcaDashboardODataService)
        {
            _rcaDashboardODataService = rcaDashboardODataService;
        }

        /// <summary>
        /// Gets the list of Rca Dashboard Info.
        /// </summary>
        /// <returns>RcaDashboardODataModel list.</returns>
        [HttpGet]
        [ODataRoute("dashboard")]
        [EnableQuery(EnsureStableOrdering = false)]
        [SecureApiPermission(IdentityPermission.Read)]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        public IHttpActionResult GetRcaDashboardInfo()
        {
            return Ok(_rcaDashboardODataService.GetRcaDashboardInfoQueryable());
        }
    }
}