// <copyright file="IRcaDbContext.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Rca.Data.Entities;

    internal interface IRcaDbContext : IDisposable
    {
         DbSet<RootCauseAnalysis> RootCauseAnalysis { get; }

         DbSet<Incident> Incidents { get; set; }

         DbSet<Participant> Participants { get; }

         DbSet<RootCauseParticipant> RootCauseParticipants { get; }

         DbSet<Classification> Classifications { get; }

         DbSet<Cause> Causes { get; }

         DbSet<RCADashboardInfoView> RCADashboardInfoViews { get; }

         DbSet<Item> Items { get; }

         int SaveChanges();

         Task<int> SaveChangesAsync();

         List<RootCauseAnalysisDetails> GetRootCauseAnalysisDetails(int rcaId, long userId);

         string ValidateItemListforRCA(string rcaItemsList, long userId);
    }
}
