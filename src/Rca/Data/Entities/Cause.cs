// <copyright file="Causes.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Entities
{
    using System;
    using Orion.Common.Data.Entities;

    internal class Cause : ITrackableEntity
    {
        public int CauseId { get; set; }

        public int RCAId { get; set; }

        public string Description { get; set; }

        public int? ClassificationId { get; set; }

        public int? ParentCauseId { get; set; }

        public bool IsDeleted { get; set; }

        public long CreatedByUserId { get; set; }

        public long LastModifiedByUserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public virtual Classification Classification { get; set; }
    }
}
