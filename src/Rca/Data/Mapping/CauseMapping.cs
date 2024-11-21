// <copyright file="CauseMapping.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Rca.Data.Entities;

    internal class CauseMapping : EntityTypeConfiguration<Cause>
    {
        public CauseMapping()
        {
            ToTable("Causes", "RCA");

            HasKey(entity => entity.CauseId);

            HasIndex(entity => entity.RCAId).HasName("FK_RCA_Causes_RCAId");
            HasIndex(entity => entity.ParentCauseId).HasName("FK_RCA_Causes_ParentCauseId");
            HasIndex(entity => entity.ClassificationId).HasName("FK_RCA_Causes_ClassificationId");

            Property(entity => entity.CauseId).HasColumnName("CauseId").IsRequired();
            Property(entity => entity.RCAId).HasColumnName("RCAId").IsRequired();
            Property(entity => entity.Description).HasColumnName("Description").IsOptional();
            Property(entity => entity.ClassificationId).HasColumnName("ClassificationId").IsOptional();
            Property(entity => entity.ParentCauseId).HasColumnName("ParentCauseId").IsOptional();
            Property(entity => entity.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            Property(entity => entity.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            Property(entity => entity.CreatedByUserId).HasColumnName("CreatedByUserId").IsRequired();
            Property(entity => entity.LastModifiedDate).HasColumnName("LastModifiedDate").IsRequired();
            Property(entity => entity.LastModifiedByUserId).HasColumnName("LastModifiedByUserId").IsRequired();
        }
    }
}
