// <copyright file="ClassificationsMapping.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Rca.Data.Entities;

    internal class ClassificationMapping : EntityTypeConfiguration<Classification>
    {
        public ClassificationMapping()
        {
            ToTable("Classifications", "RCA");

            HasKey(entity => entity.ClassificationId);

            HasIndex(e => e.ClassificationName).HasName("UQ_RCA_Classification");

            Property(entity => entity.ClassificationId).HasColumnName("ClassificationId").IsRequired();
            Property(entity => entity.ClassificationName).HasColumnName("Classification").HasMaxLength(100).IsRequired();

            HasMany(entity => entity.Causes)
                .WithOptional(x => x.Classification)
                .HasForeignKey(x => x.ClassificationId);
        }
    }
}
