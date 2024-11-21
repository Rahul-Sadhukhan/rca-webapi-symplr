﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rca.Data.Entities;

namespace Rca.Data.Mapping
{
    internal class IncidentMapping : EntityTypeConfiguration<Incident>
    {
        public IncidentMapping()
        {
            // Table and schema mapping
            ToTable("Incident", "RCA");

            // Primary Key
            HasKey(rcp => rcp.IncidentId);

            // Properties
            Property(rcp => rcp.IncidentId)
               .HasColumnName("IncidentId")
               .IsRequired();

            Property(rcp => rcp.RCAId)
               .HasColumnName("RCAId")
               .IsRequired();

            Property(rcp => rcp.ItemId)
               .HasColumnName("ItemId")
               .IsRequired();

            Property(rcp => rcp.CreatedDate)
              .HasColumnName("CreatedDate").HasPrecision(2)
              .IsRequired();

            Property(rcp => rcp.CreatedByUserId)
               .HasColumnName("CreatedByUserId")
               .IsRequired();

            Property(rcp => rcp.LastModifiedDate)
              .HasColumnName("LastModifiedDate").HasPrecision(2)
              .IsRequired();

            Property(rcp => rcp.LastModifiedByUserId)
               .HasColumnName("LastModifiedByUserId")
               .IsRequired();
            HasIndex(e => e.CreatedByUserId).HasName("FK_RCA_Incident_CreatedUser");

            HasIndex(e => e.LastModifiedByUserId).HasName("FK_RCA_Incident_LastModifiedUser");
            HasIndex(e => e.RCAId).HasName("FK_RCA_Incident_RCAId");
            HasIndex(e => e.RCAId).HasName("FK_RCA_Incident_ItemId");
        }
    }
}