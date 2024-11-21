// <copyright file="ItemMapping.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Rca.Data.Entities;

    internal class ItemMapping : EntityTypeConfiguration<Item>
    {
        public ItemMapping()
        {
            ToTable("vRCAItems", "RCA");

            HasKey(entity => entity.ItemId);
            Property(entity => entity.ItemId).HasColumnName("ItemId").IsRequired();
            Property(entity => entity.Name).HasColumnName("Name").HasMaxLength(250);
            Property(entity => entity.Description).HasColumnName("Description");
            Property(entity => entity.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            Property(entity => entity.CreatedByUserId).HasColumnName("CreatedByUserId");
            Property(entity => entity.LastModifiedDate).HasColumnName("LastModifiedDate").IsRequired();
            Property(entity => entity.LastModifiedByUserId).HasColumnName("LastModifiedByUserId").IsRequired();
            Property(entity => entity.IsDeleted).HasColumnName("IsDeleted").IsRequired();

            HasMany(entity => entity.Incidents)
                .WithRequired(x => x.Item)
                .HasForeignKey(x => x.ItemId);
            Property(entity => entity.UserId).HasColumnName("UserId");
        }
    }
}
