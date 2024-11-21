// <copyright file="AutofacConfig.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Api.App_Start
{
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Orion.Api.Filters;
    using Orion.Api.Filters.FeaturePermissionAttributeFilter;
    using Orion.Api.Filters.FunctionalPermissionAttributeFilter;
    using Orion.Api.Filters.UserItemSecurityApiActionFilter;
    using Orion.Clients;
    using Orion.Cognos;
    using Orion.Comments;
    using Orion.Configuration;
    using Orion.Configuration.ConfigurationResolvers;
    using Orion.Configuration.Models.Configs.WebApi;
    using Orion.Encryption;
    using Orion.Entities;
    using Orion.FileExport;
    using Orion.History;
    using Orion.Items;
    using Orion.Lists;
    using Orion.Logging;
    using Orion.Multilingual;
    using Orion.Permissions;
    using Orion.Search;
    using Orion.Templates;
    using Orion.Users;

    /// <summary>
    /// AutofacConfig class.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            RegisterModules(builder);
            RegisterFilters(builder);

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<RcaModule>();
            builder.RegisterModule<ClientComponentModule>();
            builder.RegisterModule<CommentsComponentModule>();
            builder.RegisterModule<CognosComponentModule>();
            builder.RegisterModule<ConfigurationComponentModule<WebApiConfigModel, WebApiConfigurationResolver>>();
            builder.RegisterModule<EncryptionComponentModule>();
            builder.RegisterModule<EntitiesModule>();
            builder.RegisterModule<FileExportModule>();
            builder.RegisterModule<HistoryComponentModule>();
            builder.RegisterModule<ItemsComponentModule>();
            builder.RegisterModule<ListComponentModule>();
            builder.RegisterModule<LoggingComponentModule>();
            builder.RegisterModule<MultilingualComponentModule>();
            builder.RegisterModule<PermissionsComponentModule>();
            builder.RegisterModule<SearchComponentModule>();
            builder.RegisterModule<TemplatesComponentModule>();
            builder.RegisterModule<UsersComponentModule>();
        }

        private static void RegisterFilters(ContainerBuilder builder)
        {
            builder.Register(c => new LoggingActionFilter(
                     c.Resolve<ILoggingComponent>(),
                     c.Resolve<IUsersComponent>(),
                     c.Resolve<IClientComponent>()))
                 .AsWebApiActionFilterFor<ApiController>()
                 .InstancePerRequest();

            builder.Register(c => new AuthenticationFilter(
                    c.Resolve<IUsersComponent>(),
                    c.Resolve<ILoggingComponent>(),
                    c.Resolve<IClientComponent>()))
                .AsWebApiAuthenticationFilterFor<ApiController>()
                .InstancePerRequest();

            builder.Register(c => new ModelValidationActionFilter())
                .AsWebApiActionFilterFor<ApiController>()
                .InstancePerRequest();

            builder.Register(c => new FunctionalPermissionSecurityApiActionFilter(
                    c.Resolve<IPermissionsComponent>(),
                    c.Resolve<IUsersComponent>(),
                    c.Resolve<IMultilingualComponent>()))
                .AsWebApiActionFilterFor<ApiController>()
                .InstancePerRequest();

            builder.Register(c => new ClientModuleAuthorizationFilter(
                    c.Resolve<IClientComponent>(),
                    c.Resolve<IUsersComponent>(),
                    c.Resolve<IMultilingualComponent>(),
                    new[] { Orion.Clients.Enums.Module.RootCauseAnalysis }))
                .AsWebApiAuthorizationFilterFor<ApiController>()
                .InstancePerRequest();

            builder.Register(c => new UserItemSecurityApiActionFilter(
                    c.Resolve<IUsersComponent>(),
                    c.Resolve<IPermissionsComponent>(),
                    c.Resolve<ISecurityItemResolver>(),
                    c.Resolve<IMultilingualComponent>()))
               .AsWebApiActionFilterFor<SecureApiODataController>()
               .InstancePerRequest();

            builder.Register(c => new UserItemSecurityApiActionFilter(
                    c.Resolve<IUsersComponent>(),
                    c.Resolve<IPermissionsComponent>(),
                    c.Resolve<ISecurityItemResolver>(),
                    c.Resolve<IMultilingualComponent>()))
                .AsWebApiActionFilterFor<SecureApiController>()
                .InstancePerRequest();

            builder.Register(c => new FeaturePermissionSecurityApiActionFilter(
                    c.Resolve<IClientComponent>(),
                    c.Resolve<IMultilingualComponent>(),
                    c.Resolve<IUsersComponent>()))
                .AsWebApiActionFilterFor<SecureApiController>()
                .InstancePerRequest();
        }
    }
}