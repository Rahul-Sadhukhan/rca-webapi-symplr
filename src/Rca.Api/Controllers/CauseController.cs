// <copyright file="CauseController.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Api.Controllers
{
    using System.Web.Http;
    using Orion.Api.Filters.FunctionalPermissionAttributeFilter;
    using Orion.Api.Filters.UserItemSecurityApiActionFilter;
    using Orion.Permissions.Enums;
    using Rca.Models;
    using Rca.Services;

    /// <summary>
    /// Cause controller.
    /// </summary>
    /// <seealso cref="ApiController" />
    [RoutePrefix("causes")]
    public class CauseController : SecureApiController
    {
        private readonly ICauseService _causeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CauseController"/> class.
        /// </summary>
        /// <param name="causeService">The cause service.</param>
        public CauseController(ICauseService causeService)
        {
            _causeService = causeService;
        }

        /// <summary>
        /// Gets a list of classifications.
        /// </summary>
        /// <returns>List of classifications.</returns>
        [HttpGet]
        [Route("classifications")]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        [SecureApiPermission(IdentityPermission.Read)]
        public IHttpActionResult GetClassifications()
        {
            var classifications = _causeService.GetClassifications();
            return Ok(classifications);
        }

        /// <summary>
        /// Gets the cause details for a provider cause id.
        /// </summary>
        /// <param name="causeId">The cause identifier.</param>
        /// <returns>Cause model.</returns>
        [HttpGet]
        [Route("{causeId:int}")]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        [SecureApiPermission(IdentityPermission.Read)]
        public IHttpActionResult GetCauseById(int causeId)
        {
            return Ok(_causeService.GetCauseById(causeId));
        }

        /// <summary>
        /// Creates a cause.
        /// </summary>
        /// <param name="rcaId">The rca identifier.</param>
        /// <param name="causeModel">Cause model.</param>
        /// <returns>Success.</returns>
        [Route("{rcaId:int}")]
        [HttpPost]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        [SecureApiPermission(IdentityPermission.Modify)]
        public IHttpActionResult Post(int rcaId, [FromBody] CauseModel causeModel)
        {
            _causeService.CreateCause(rcaId, causeModel);
            return Ok();
        }

        /// <summary>
        /// Gets a list of RCA classification causes and it's sub-causes.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <param name="classificationId">Classification identifier.</param>
        /// <returns>Cause select list model.</returns>
        [HttpGet]
        [Route("rootcauseanalysis/{rcaId:int}/classifications/{classificationId:int}")]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        [SecureApiPermission(IdentityPermission.Read)]
        public IHttpActionResult GetRCAClassificationCauses(int rcaId, int classificationId)
        {
            return Ok(_causeService.GetRCAClassificationCauses(rcaId, classificationId));
        }

        /// <summary>
        /// Gets a list of classifications for a provided RCA id.
        /// </summary>
        /// <param name="rcaId">RCA identifier.</param>
        /// <returns>List of classifications.</returns>
        [HttpGet]
        [Route("rootcauseanalysis/{rcaId:int}")]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        [SecureApiPermission(IdentityPermission.Read)]
        public IHttpActionResult GetClassificationsByRCAId(int rcaId)
        {
            return Ok(_causeService.GetClassificationsByRCAId(rcaId));
        }
    }
}
