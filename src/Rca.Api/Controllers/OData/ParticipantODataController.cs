// <copyright file="ParticipantODataController.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Orion.Api.Filters.FunctionalPermissionAttributeFilter;
using Orion.Api.Filters.UserItemSecurityApiActionFilter;
using Orion.Permissions.Enums;
using Rca.Models.OData;
using Rca.Services.OData;

namespace Rca.Api.Controllers.OData
{
    public class ParticipantODataController : SecureApiODataController
    {
        private readonly IParticipantODataService _participantODataService;

        public ParticipantODataController(IParticipantODataService participantODataService)
        {
            _participantODataService = participantODataService;
        }

        /// <summary>
        /// Gets the list of participants.
        /// </summary>
        /// <returns>Queryable list of ParticipantODataController.</returns>
        [ODataRoute("participants")]
        [HttpGet]
        [EnableQuery]
        [SecureApiPermission(IdentityPermission.Read)]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        public IQueryable<ParticipantODataModel> GetParticipant()
        {
            return _participantODataService.GetParticipantQueryable();
        }
    }
}