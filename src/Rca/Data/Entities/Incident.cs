using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Common.Data.Entities;

namespace Rca.Data.Entities
{
    internal class Incident : ITrackableEntity
    {
       [Key]
       public int IncidentId { get; set; }

       public int RCAId { get; set; }

       public long ItemId { get; set; }

       public DateTimeOffset CreatedDate { get; set; }

       public long CreatedByUserId { get; set; }

       public DateTimeOffset LastModifiedDate { get; set; }

       public long LastModifiedByUserId { get; set; }

       public bool IsDeleted { get; set; }

       public virtual Item Item { get; set; }

       public virtual RootCauseAnalysis RootCauseAnalysis { get; set; }
    }
}
