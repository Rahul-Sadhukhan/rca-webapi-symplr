// <copyright file="RCADashboardInfoView.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using Orion.Common.Data.Entities;

namespace Rca.Data.Entities
{
    [ExcludeFromCodeCoverage]
    internal class RCADashboardInfoView
    {
        public int RcaId { get; set; }

        public DateTimeOffset? IncidentDate { get; set; }

        public string Name { get; set; }

        public string ProblemStatement { get; set; }

        public string Theme { get; set; }

        public string Owner { get; set; }

        public int? LinkedItems { get; set; }

        public string Status { get; set; }

        public long CreatedByUserId { get; set; }
    }
}
