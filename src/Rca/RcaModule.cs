// <copyright file="RcaModule.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca
{
    using System.Linq;
    using Autofac;
    using FluentValidation;
    using Orion.Api.Filters.UserItemSecurityApiActionFilter;
    using Rca.Data;
    using Rca.Data.Repositories;
    using Rca.Data.Repositories.Queryable;
    using Rca.Models;
    using Rca.Resolvers;
    using Rca.Services;
    using Rca.Services.OData;
    using Rca.Validators;

    public class RcaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // services registration
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.Contains("Rca.Services"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<RootCauseAnalysisService>().As<IRootCauseAnalysisService>().InstancePerLifetimeScope();
            builder.RegisterType<SoftLabelResourceService>().As<ISoftLabelResourceService>().InstancePerLifetimeScope();
            builder.RegisterType<CauseService>().As<ICauseService>().InstancePerLifetimeScope();
            builder.RegisterType<RcaDashboardODataService>().As<IRcaDashboardODataService>().InstancePerLifetimeScope();
            builder.RegisterType<ParticipantODataService>().As<IParticipantODataService>().InstancePerLifetimeScope();
            builder.RegisterType<ItemODataService>().As<IItemODataService>().InstancePerLifetimeScope();

            // Security resolver
            builder.RegisterType<SecurityItemResolver>().As<ISecurityItemResolver>().InstancePerLifetimeScope();

            // Db Context
            builder.RegisterType<RcaDbContext>().As<IRcaDbContext>().InstancePerLifetimeScope();

            // Unit of Work
            builder.RegisterType<RcaUnitOfWork>().As<IRcaUnitOfWork>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<RootCauseAnalysisRepository>().As<IRootCauseAnalysisRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ParticipantRepository>().As<IParticipantRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RootCauseParticipantRepository>().As<IRootCauseParticipantRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IncidentRepository>().As<IIncidentRepository>().InstancePerLifetimeScope();

            // Validator
            builder.RegisterType<RootCauseAnalysisModelValidator>().As<IRootCauseAnalysisModelValidator>().InstancePerLifetimeScope();
            builder.RegisterType<ClassificationRepository>().As<IClassificationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CauseRepository>().As<ICauseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RcaDashboardQueryableRepository>().As<IRcaDashboardQueryableRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ParticipantQueryableRepository>().As<IParticipantQueryableRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ItemQueryableRepository>().As<IItemQueryableRepository>().InstancePerLifetimeScope();

            // Validators
            builder.RegisterType<CauseModelValidator<CauseModel>>().As<IValidator<CauseModel>>().InstancePerLifetimeScope();
        }
    }
}
