// <copyright file="RootCauseAnalysisMapping.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Rca.Data.Entities;

    internal class RootCauseAnalysisMapping : EntityTypeConfiguration<RootCauseAnalysis>
    {
        public RootCauseAnalysisMapping()
        {
            ToTable("RootCauseAnalysis", "RCA");

            HasKey(entity => entity.RcaId);
            Property(entity => entity.RcaId).HasColumnName("RCAId").IsRequired();
            Property(entity => entity.RcaName).HasColumnName("RCAName").HasMaxLength(250).IsRequired();
            Property(entity => entity.Theme).HasColumnName("Theme").HasMaxLength(100).IsOptional();
            Property(entity => entity.Description).HasColumnName("Description").IsRequired();
            Property(entity => entity.CreatedByUserId).HasColumnName("CreatedByUserId").IsRequired();
            Property(entity => entity.LastModifiedByUserId).HasColumnName("LastModifiedByUserId").IsRequired();
            Property(entity => entity.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            Property(entity => entity.Status).HasColumnName("Status").IsOptional();
            Property(entity => entity.IncidentDate).HasColumnName("IncidentDate").HasPrecision(2).IsOptional();
            Property(entity => entity.CreatedDate).HasColumnName("CreatedDate").HasPrecision(2).IsRequired();
            Property(entity => entity.LastModifiedDate).HasColumnName("LastModifiedDate").HasPrecision(2).IsRequired();
            HasIndex(e => e.CreatedByUserId).HasName("FK_RCA_RootCauseAnalysis_CreatedUser");
            HasIndex(e => e.LastModifiedByUserId).HasName("FK_RCA_RootCauseAnalysis_LastModifiedUser");

            HasMany(entity => entity.RootCauseParticipants)
                .WithRequired(x => x.RootCauseAnalyses)
                .HasForeignKey(x => x.RCAId);

            HasMany(entity => entity.Incidents)
                .WithRequired(x => x.RootCauseAnalysis)
                .HasForeignKey(x => x.RCAId);
        }
    }
}
