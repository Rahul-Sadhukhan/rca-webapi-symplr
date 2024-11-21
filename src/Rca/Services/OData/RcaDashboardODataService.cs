// <copyright file="RcaDashboardODataService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Linq;
using Orion.Common.Exceptions;
using Orion.Users;
using Rca.Data;
using Rca.Enums;
using Rca.Models.OData;

namespace Rca.Services.OData
{
    /// <summary>
    /// RcaDashboard Service class to return data in OData format.
    /// </summary>
    internal class RcaDashboardODataService : IRcaDashboardODataService
    {
        private readonly IRcaUnitOfWork _rcaUnitOfWork;
        private readonly IUsersComponent _usersComponent;
        private readonly ISoftLabelResourceService _softLabelResourceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RcaDashboardODataService" /> class.
        /// </summary>
        /// <param name="rcaUnitOfWork">The unit of work.</param>
        /// <param name="usersComponent">The user component.</param>
        /// <param name="softLabelResourceService">Validaion Message service.</param>
        public RcaDashboardODataService(IRcaUnitOfWork rcaUnitOfWork, IUsersComponent usersComponent, ISoftLabelResourceService softLabelResourceService)
        {
            _rcaUnitOfWork = rcaUnitOfWork;
            _usersComponent = usersComponent;
            _softLabelResourceService = softLabelResourceService;
        }

        /// <summary>
        /// Get List of RCA Dashboard Info in OData format.
        /// </summary>
        /// <returns>A list of RcaDashboardODataModel.</returns>
        public IQueryable<RcaDashboardODataModel> GetRcaDashboardInfoQueryable()
        {
            try
            {
                return _rcaUnitOfWork.RcaDashboardQueryableRepository.GetRcaDashboardInfoQueryableByUserId(_usersComponent.CurrentUser.Id);
            }
            catch (Exception ex)
            {
                throw new ValidationPresentationException(ex.Message);
            }
        }
    }
}
