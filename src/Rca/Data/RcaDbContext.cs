// <copyright file="RcaDbContext.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using Rca.Data.Entities;
    using Rca.Data.Mapping;

    internal class RcaDbContext : DbContext, IRcaDbContext
    {
        static RcaDbContext()
        {
            Database.SetInitializer<RcaDbContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RcaDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RcaDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<RootCauseAnalysis> RootCauseAnalysis { get; set; }

        public DbSet<Incident> Incidents { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<RootCauseParticipant> RootCauseParticipants { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<Cause> Causes { get; set; }

        public DbSet<RCADashboardInfoView> RCADashboardInfoViews { get; set; }

        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Retrieves the list of rca details.
        /// </summary>
        /// <param name="rcaId">rca identifier.</param>
        /// <param name="userId">user identifier.</param>
        /// <returns>List of rca details.</returns>
        public List<RootCauseAnalysisDetails> GetRootCauseAnalysisDetails(int rcaId, long userId)
        {
            var paramRcaId = new SqlParameter
            {
                Value = rcaId,
                DbType = DbType.Int32,
                ParameterName = "@RcaId",
            };

            var paramUserId = new SqlParameter
            {
                Value = userId,
                DbType = DbType.Int64,
                ParameterName = "@UserId",
            };

            return Database.SqlQuery<RootCauseAnalysisDetails>(
                "EXECUTE [RCA].[GetRCADetails] @RcaId, @UserId",
                paramRcaId,
                paramUserId).ToList();
        }

        /// <summary>
        /// Validates list of items.
        /// </summary>
        /// <param name="rcaItemsList">List of Items.</param>
        /// <param name="userId">Current User Id.</param>
        /// <returns>Validation message.</returns>
        public string ValidateItemListforRCA(string rcaItemsList, long userId)
        {
            var paramItemList = new SqlParameter
            {
                Value = rcaItemsList,
                DbType = DbType.String,
                Size = -1,
                ParameterName = "@ItemList",
            };

            var paramUserId = new SqlParameter
            {
                Value = userId,
                DbType = DbType.Int64,
                ParameterName = "@UserId",
            };

            return Database.SqlQuery<string>(
                "EXECUTE [RCA].[ValidateItemListForRCA] @ItemList, @UserId",
                paramItemList,
                paramUserId).FirstOrDefault();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RootCauseAnalysisMapping());
            modelBuilder.Configurations.Add(new ParticipantMapping());
            modelBuilder.Configurations.Add(new RootCauseParticipantMapping());
            modelBuilder.Configurations.Add(new ClassificationMapping());
            modelBuilder.Configurations.Add(new CauseMapping());
            modelBuilder.Configurations.Add(new RCADashboardInfoViewMapping());
            modelBuilder.Configurations.Add(new ItemMapping());
            modelBuilder.Configurations.Add(new IncidentMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
