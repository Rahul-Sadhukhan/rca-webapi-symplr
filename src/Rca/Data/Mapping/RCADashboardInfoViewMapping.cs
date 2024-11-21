// <copyright file="RCADashboardInfoViewMapping.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Rca.Data.Entities;

    internal class RCADashboardInfoViewMapping : EntityTypeConfiguration<RCADashboardInfoView>
    {
        public RCADashboardInfoViewMapping()
        {
            ToTable("vRCADashBoardInfo", "RCA");

            HasKey(entity => entity.RcaId);

            Property(entity => entity.RcaId).HasColumnName("RCAId");
            Property(entity => entity.IncidentDate).HasColumnName("IncidentDate");
            Property(entity => entity.Name).HasColumnName("Name");
            Property(entity => entity.ProblemStatement).HasColumnName("ProblemStatement");
            Property(entity => entity.Theme).HasColumnName("Theme");
            Property(entity => entity.Owner).HasColumnName("Owner");
            Property(entity => entity.LinkedItems).HasColumnName("LinkedItems");
            Property(entity => entity.Status).HasColumnName("Status");
            Property(entity => entity.CreatedByUserId).HasColumnName("CreatedByUserId");
        }
    }
}
