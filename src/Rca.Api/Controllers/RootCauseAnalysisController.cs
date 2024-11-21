// <copyright file="RootCauseAnalysisController.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>
using System.Web.Http;
using Orion.Api.Filters.FunctionalPermissionAttributeFilter;
using Orion.Api.Filters.UserItemSecurityApiActionFilter;
using Orion.Permissions.Enums;
using Rca.Models;
using Rca.Services;

namespace Rca.Api.Controllers
{
    /// <summary>
    /// RootCause Analysis Controller.
    /// </summary>
    public class RootCauseAnalysisController : SecureApiController
    {
        private readonly IRootCauseAnalysisService _rootCauseAnalysisService;

        public RootCauseAnalysisController(IRootCauseAnalysisService rootCauseAnalysisService)
        {
            _rootCauseAnalysisService = rootCauseAnalysisService;
        }

        /// <summary>
        /// Gets rca by id.
        /// </summary>
        /// <param name="rcaId">rca id.</param>
        /// <returns>Question list.</returns>
        [HttpGet]
        [Route("rootcauseanalysis/{rcaId:int}")]
        [SecureApiPermission(IdentityPermission.Read)]
        public IHttpActionResult GetRootCauseAnalysisById(int rcaId)
        {
            return Ok(_rootCauseAnalysisService.GetRootCauseAnalysisById(rcaId));
        }

        /// <summary>
        /// Create root cause analysis.
        /// </summary>
        /// <param name="rootCauseAnalysisModel">Create rca model.</param>
        /// <returns>Response model containing created rca id.</returns>
        [HttpPost]
        [Route("create")]
        [SecureApiPermission(IdentityPermission.Modify)]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysisCreate })]
        public IHttpActionResult Create([FromBody] CreateRCAModel rootCauseAnalysisModel)
        {
            return Ok(_rootCauseAnalysisService.CreateRootCauseAnalysis(rootCauseAnalysisModel));
        }

        /// <summary>
        /// Gets the rca details by Id.
        /// </summary>
        /// <param name="rcaId">The rca identifier.</param>
        /// <returns>Rca details model.</returns>
        [HttpGet]
        [Route("rootcauseanalysisdetails/{rcaId:int}")]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        [SecureApiPermission(IdentityPermission.Read)]
        public IHttpActionResult GetRootCauseAnalysisDetails(int rcaId)
        {
            return Ok(_rootCauseAnalysisService.GetRootCauseAnalysisDetails(rcaId));
        }
    }
}
