using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Common.Data.Entities;

namespace Rca.Data.Entities
{
    internal class RootCauseParticipant : ITrackableEntity
    {
        public int RootCauseParticipantId { get; set; }

        public int RCAId { get; set; }

        public int ParticipantId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public long CreatedByUserId { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public long LastModifiedByUserId { get; set; }

        public virtual Participant Participants { get; set; }

        public virtual RootCauseAnalysis RootCauseAnalyses { get; set; }
    }
}
