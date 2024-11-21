using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Common.Data;
using Orion.Common.Data.Entities;

namespace Rca.Extension
{
    public static class BaseUnitOfWorkExtension
    {
        public static async Task<int> SaveChangesAsync(this BaseUnitOfWork unitOfWork, DbContext db, long userId)
        {
            foreach (var entity in db.ChangeTracker.Entries<ITrackableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        entity.Entity.CreatedByUserId = userId;
                        entity.Entity.LastModifiedDate = entity.Entity.CreatedDate;
                        entity.Entity.LastModifiedByUserId = userId;
                        break;
                    case EntityState.Modified:
                        entity.Entity.LastModifiedDate = DateTimeOffset.UtcNow;
                        entity.Entity.LastModifiedByUserId = userId;
                        break;
                }
            }

            return await db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
