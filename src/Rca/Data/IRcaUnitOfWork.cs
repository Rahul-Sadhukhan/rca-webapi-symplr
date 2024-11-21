// <copyright file="IRcaUnitOfWork.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>
using System.Threading.Tasks;
using Rca.Data.Repositories;
using Rca.Data.Repositories.Queryable;

namespace Rca.Data
{
    internal interface IRcaUnitOfWork
    {
        IRootCauseAnalysisRepository RootCauseAnalysisRepository { get; }

        IParticipantRepository ParticipantRepository { get; }

        IRootCauseParticipantRepository RootCauseParticipantRepository { get; }

        ICauseRepository CauseRepository { get; }

        IClassificationRepository ClassificationRepository { get; }

        IRcaDashboardQueryableRepository RcaDashboardQueryableRepository { get; }

        IParticipantQueryableRepository ParticipantQueryableRepository { get; }

        IItemQueryableRepository ItemQueryableRepository { get; }

        IIncidentRepository IncidentRepository { get; }

        int SaveChanges();
    }
}