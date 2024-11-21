// <copyright file="Item.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Orion.Common.Data.Entities;

    internal class Item : ITrackableEntity
    {
        public long ItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public long CreatedByUserId { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public long LastModifiedByUserId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

        public long UserId { get; set; }
    }
}
