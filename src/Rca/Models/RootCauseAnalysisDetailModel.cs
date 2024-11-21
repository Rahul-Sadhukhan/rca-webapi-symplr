// <copyright file="RootCauseAnalysisDetailModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Collections.Generic;
using Rca.Enums;

namespace Rca.Models
{
    public class RootCauseAnalysisDetailModel
    {
        public int RcaId { get; set; }

        public string RcaName { get; set; }

        public string Theme { get; set; }

        public string Description { get; set; }

        public RootCauseAnalysisStatus? Status { get; set; }

        public DateTimeOffset? IncidentDate { get; set; }

        public ICollection<ParticipantModel> Participants { get; set; }

        public ICollection<LinkedItemModel> LinkedItems { get; set; }
    }
}
