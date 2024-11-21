// <copyright file="RootCauseAnalysisDetails.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using Rca.Enums;

namespace Rca.Data.Entities
{
    [ExcludeFromCodeCoverage]
    internal class RootCauseAnalysisDetails
    {
        public int RcaId { get; set; }

        public string RcaName { get; set; }

        public string Theme { get; set; }

        public string Description { get; set; }

        public RootCauseAnalysisStatus? Status { get; set; }

        public DateTimeOffset? IncidentDate { get; set; }

        public long? ItemId { get; set; }

        public string ItemName { get; set; }

        public int? ParticipantId { get; set; }

        public string ParticipantName { get; set; }

        public bool? IsReadPermissionEnabled { get; set; }
    }
}
