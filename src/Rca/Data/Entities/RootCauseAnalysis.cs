// <copyright file="RootCauseAnalysis.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Orion.Common.Data.Entities;

    internal class RootCauseAnalysis : ITrackableEntity
    {
        public int RcaId { get; set; }

        public string RcaName { get; set; }

        public string Theme { get; set; }

        public string Description { get; set; }

        public byte? Status { get; set; }

        public DateTimeOffset? IncidentDate { get; set; }

        public bool IsDeleted { get; set; }

        public long CreatedByUserId { get; set; }

        public long LastModifiedByUserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public virtual ICollection<RootCauseParticipant> RootCauseParticipants { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
