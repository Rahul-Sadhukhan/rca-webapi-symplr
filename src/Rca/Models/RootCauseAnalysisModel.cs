// <copyright file="RootCauseAnalysisModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>
using System;
using System.Collections.Generic;

namespace Rca.Models
{
    public class RootCauseAnalysisModel
    {
        public int RcaId { get; set; }

        public string RcaName { get; set; }

        public string Theme { get; set; }

        public string Description { get; set; }

        public int? Status { get; set; }

        public DateTimeOffset? IncidentDate { get; set; }

        public bool IsDeleted { get; set; }

        public long CreatedByUserId { get; set; }

        public long LastModifiedByUserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public IEnumerable<ParticipantModel> Participants { get; set; }
    }
}
