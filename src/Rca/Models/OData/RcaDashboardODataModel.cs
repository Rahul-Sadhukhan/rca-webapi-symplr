// <copyright file="RcaDashboardODataModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Runtime.Serialization;

namespace Rca.Models.OData
{
    /// <summary>
    /// OData model for Rca Dashboard.
    /// </summary>
    [DataContract]
    public class RcaDashboardODataModel
    {
        [DataMember(Name = "rcaId")]
        public int RcaId { get; set; }

        [DataMember(Name = "incidentDate")]
        public DateTimeOffset? IncidentDate { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "problemStatement")]
        public string ProblemStatement { get; set; }

        [DataMember(Name = "theme")]
        public string Theme { get; set; }

        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        [DataMember(Name = "linkedItems")]
        public int? LinkedItems { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "createdByUserId")]
        public long CreatedByUserId { get; set; }
    }
}
