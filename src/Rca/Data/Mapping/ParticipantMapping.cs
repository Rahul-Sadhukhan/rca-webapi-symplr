using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rca.Data.Entities;

namespace Rca.Data.Mapping
{
    internal class ParticipantMapping : EntityTypeConfiguration<Participant>
    {
        public ParticipantMapping()
        {
            // Map to the 'RCA.Participants' table
             ToTable("Participants", "RCA");

            // Primary Key
             HasKey(p => p.ParticipantId);

            // Properties
             Property(p => p.ParticipantId)
                .HasColumnName("ParticipantId")
                .IsRequired();

             Property(p => p.ParticipantName)
                .HasColumnName("ParticipantName")
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode(true); // nvarchar is Unicode by default

             Property(p => p.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasPrecision(2)
                .IsRequired();

             Property(p => p.CreatedByUserId)
                .HasColumnName("CreatedByUserId")
                .IsRequired();

             Property(p => p.LastModifiedDate)
                .HasColumnName("LastModifiedDate")
                .HasPrecision(2)
                .IsRequired();

             Property(p => p.LastModifiedByUserId)
                .HasColumnName("LastModifiedByUserId")
                .IsRequired();

             Property(p => p.IsDeleted)
                .HasColumnName("IsDeleted")
                .IsRequired()
                .HasColumnType("bit");

             HasIndex(e => e.CreatedByUserId).HasName("FK_RCA_Participants_CreatedUser");

             HasIndex(e => e.LastModifiedByUserId).HasName("FK_RCA_Participants_LastModifiedUser");

             HasMany(entity => entity.RootCauseParticipants)
                .WithRequired(x => x.Participants)
                .HasForeignKey(x => x.RCAId);
        }
    }
}
