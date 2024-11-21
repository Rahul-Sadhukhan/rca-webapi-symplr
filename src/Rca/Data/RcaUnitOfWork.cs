// <copyright file="RcaUnitOfWork.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Data
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Orion.Clients;
    using Orion.Common.Data;
    using Orion.Users;
    using Rca.Data.Entities;
    using Rca.Data.Repositories;
    using Rca.Data.Repositories.Queryable;
    using Rca.Extension;

    internal class RcaUnitOfWork : BaseUnitOfWork, IRcaUnitOfWork
    {
        private readonly Lazy<IRcaDbContext> _dbContext;
        private readonly IUsersComponent _usersComponent;
        private readonly Lazy<IRootCauseAnalysisRepository> _rootCauseAnalysisRepository;
        private readonly Lazy<IParticipantRepository> _participantRepository;
        private readonly Lazy<IRootCauseParticipantRepository> _rootCauseParticipantRepository;
        private readonly Lazy<ICauseRepository> _causeRepository;
        private readonly Lazy<IClassificationRepository> _classificationRepository;
        private readonly Lazy<IRcaDashboardQueryableRepository> _rcaDashboardQueryableRepository;
        private readonly Lazy<IParticipantQueryableRepository> _participantQueryableRepository;
        private readonly Lazy<IItemQueryableRepository> _itemQueryableRepository;
        private readonly Lazy<IIncidentRepository> _incidentRepository;

        public RcaUnitOfWork(
            IClientComponent clientComponent,
            IUsersComponent usersComponent,
            Func<IRcaDbContext, IRootCauseAnalysisRepository> rootCauseAnalysisRepository,
            Func<IRcaDbContext, IParticipantRepository> participantRepository,
            Func<IRcaDbContext, IRootCauseParticipantRepository> rootCauseParticipantRepository,
            Func<IRcaDbContext, ICauseRepository> causeRepository,
            Func<IRcaDbContext, IClassificationRepository> classificationRepository,
            Func<IRcaDbContext, IRcaDashboardQueryableRepository> rcaDashboardQueryableRepository,
            Func<IRcaDbContext, IParticipantQueryableRepository> participantQueryableRepository,
            Func<IRcaDbContext, IItemQueryableRepository> itemQueryableRepository,
            Func<IRcaDbContext, IIncidentRepository> incidentRepository,
            Func<string, IRcaDbContext> dbFactory)
        {
            _dbContext = new Lazy<IRcaDbContext>(() => dbFactory(clientComponent.CurrentClient.ConnectionString));
            _rootCauseAnalysisRepository = new Lazy<IRootCauseAnalysisRepository>(() => rootCauseAnalysisRepository(_dbContext.Value));
            _participantRepository = new Lazy<IParticipantRepository>(() => participantRepository(_dbContext.Value));
            _rootCauseParticipantRepository = new Lazy<IRootCauseParticipantRepository>(() => rootCauseParticipantRepository(_dbContext.Value));
            _usersComponent = usersComponent;
            _causeRepository = new Lazy<ICauseRepository>(() => causeRepository(_dbContext.Value));
            _classificationRepository = new Lazy<IClassificationRepository>(() => classificationRepository(_dbContext.Value));
            _rcaDashboardQueryableRepository = new Lazy<IRcaDashboardQueryableRepository>(() => rcaDashboardQueryableRepository(_dbContext.Value));
            _participantQueryableRepository = new Lazy<IParticipantQueryableRepository>(() => participantQueryableRepository(_dbContext.Value));
            _itemQueryableRepository = new Lazy<IItemQueryableRepository>(() => itemQueryableRepository(_dbContext.Value));
            _incidentRepository = new Lazy<IIncidentRepository>(() => incidentRepository(_dbContext.Value));
        }

        public IRootCauseAnalysisRepository RootCauseAnalysisRepository => _rootCauseAnalysisRepository.Value;

        public IParticipantRepository ParticipantRepository => _participantRepository.Value;

        public IRootCauseParticipantRepository RootCauseParticipantRepository => _rootCauseParticipantRepository.Value;

        public ICauseRepository CauseRepository => _causeRepository.Value;

        public IClassificationRepository ClassificationRepository => _classificationRepository.Value;

        public IRcaDashboardQueryableRepository RcaDashboardQueryableRepository => _rcaDashboardQueryableRepository.Value;

        public IParticipantQueryableRepository ParticipantQueryableRepository => _participantQueryableRepository.Value;

        public IItemQueryableRepository ItemQueryableRepository => _itemQueryableRepository.Value;

        public IIncidentRepository IncidentRepository => _incidentRepository.Value;

        public int SaveChanges()
        {
            try
            {
                return SaveChanges((DbContext)_dbContext.Value, _usersComponent.CurrentUser.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
