using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rca.Data.Entities;

namespace Rca.Data.Mapping
{
    internal class RootCauseParticipantMapping : EntityTypeConfiguration<RootCauseParticipant>
    {
        public RootCauseParticipantMapping()
        {
            // Table and schema mapping
             ToTable("RootCauseParticipants", "RCA");

            // Primary Key
             HasKey(rcp => rcp.RootCauseParticipantId);

            // Properties
             Property(rcp => rcp.RootCauseParticipantId)
                .HasColumnName("RootCauseParticipantId")
                .IsRequired();

             Property(rcp => rcp.RCAId)
                .HasColumnName("RCAId")
                .IsRequired();

             Property(rcp => rcp.ParticipantId)
                .HasColumnName("ParticipantId")
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
             HasIndex(e => e.CreatedByUserId).HasName("FK_RCA_RootCauseParticipants_CreatedUser");

             HasIndex(e => e.LastModifiedByUserId).HasName("FK_RCA_RootCauseParticipants_LastModifiedUser");
             HasIndex(e => e.RCAId).HasName("FK_RCA_RootCauseParticipants_RCAId");
        }
    }
}
