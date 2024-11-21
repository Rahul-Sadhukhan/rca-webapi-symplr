// <copyright file="ItemODataController.cs" company="symplr">
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
    /// <summary>
    /// Item OData controller.
    /// </summary>
    /// <seealso cref="ApiController" />
    public class ItemODataController : SecureApiODataController
    {
        private readonly IItemODataService _itemODataService;

        public ItemODataController(IItemODataService itemODataService)
        {
            _itemODataService = itemODataService;
        }

        /// <summary>
        /// Gets the list of Item Info.
        /// </summary>
        /// <returns>ItemODataModel list.</returns>
        [HttpGet]
        [ODataRoute("items")]
        [EnableQuery]
        [SecureApiPermission(IdentityPermission.Read)]
        [FunctionalPermission(new[] { FunctionalPermissionType.ManageRootCauseAnalysis })]
        public IQueryable<ItemODataModel> GetItem()
        {
            return _itemODataService.GetItemQueryable();
        }
    }
}