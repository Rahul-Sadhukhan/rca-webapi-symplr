using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Common.Data.Entities;

namespace Rca.Data.Entities
{
    internal class Participant : ITrackableEntity
    {
        public int ParticipantId { get; set; }

        public string ParticipantName { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public long CreatedByUserId { get; set; }

        public DateTimeOffset LastModifiedDate { get; set; }

        public long LastModifiedByUserId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<RootCauseParticipant> RootCauseParticipants { get; set; }
    }
}
