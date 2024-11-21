// <copyright file="ItemODataService.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using Orion.Users;
using Rca.Data;
using Rca.Models.OData;

namespace Rca.Services.OData
{
    internal class ItemODataService : IItemODataService
    {
        private readonly IRcaUnitOfWork _rcaUnitOfWork;
        private readonly IUsersComponent _usersComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemODataService" /> class.
        /// </summary>
        /// <param name="rcaUnitOfWork">The unit of work.</param>
        /// <param name="usersComponent">The user component.</param>
        public ItemODataService(IRcaUnitOfWork rcaUnitOfWork, IUsersComponent usersComponent)
        {
            _rcaUnitOfWork = rcaUnitOfWork;
            _usersComponent = usersComponent;
        }

        /// <summary>
        /// Get List of Items in OData format.
        /// </summary>
        /// <returns>A list of ItemODataModel.</returns>
        public IQueryable<ItemODataModel> GetItemQueryable()
        {
            return _rcaUnitOfWork.ItemQueryableRepository.GetItemQueryableByUserId(_usersComponent.CurrentUser.Id);
        }
    }
}
