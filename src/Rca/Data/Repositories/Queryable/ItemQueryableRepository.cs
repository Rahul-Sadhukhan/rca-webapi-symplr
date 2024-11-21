// <copyright file="ItemQueryableRepository.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using AutoMapper.QueryableExtensions;
using Rca.Models.OData;

namespace Rca.Data.Repositories.Queryable
{
    internal class ItemQueryableRepository : IItemQueryableRepository
    {
        private readonly IRcaDbContext _rcaDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemQueryableRepository"/> class.
        /// </summary>
        /// <param name="rcaDbContext">The database context.</param>
        public ItemQueryableRepository(IRcaDbContext rcaDbContext)
        {
            _rcaDbContext = rcaDbContext;
        }

        /// <summary>
        /// Gets a list of item models in OData format.
        /// </summary>
        /// <returns>A list of item models.</returns>
        /// <param name="userId">User Id. </param>
        public IQueryable<ItemODataModel> GetItemQueryableByUserId(long userId)
        {
            return _rcaDbContext
            .Items
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .ProjectTo<ItemODataModel>();
        }
    }
}
