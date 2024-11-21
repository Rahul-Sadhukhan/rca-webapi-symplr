// <copyright file="RcaModuleTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using FluentAssertions;
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
using Orion.Tests;
using Orion.Users;
using Rca.Data;
using Xunit;

namespace Rca.Tests
{
    public class RcaModuleTests : AutofacRegistrationTestBase
    {
        private readonly IContainer _container;

        public RcaModuleTests()
        {
            var builder = new ContainerBuilder();
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

            builder.RegisterType<RcaDbContext>()
                .As<IRcaDbContext>()
                .WithParameter("connectionString", "XXX");

            _container = builder.Build();
        }

        [Fact]
        public void RcaModuleIsConfigured_Always_SuccessfulResolveComponent()
        {
            var module = Activator.CreateInstance(typeof(RcaModule)) as IModule;
            var moduleRegisteredTypes = GetRegisteredServiceTypesFromModule(module);

            foreach (var type in moduleRegisteredTypes)
            {
                var instance = _container.Resolve(type);
                instance.Should().NotBe(null, $"Type {type.Name} should be properly registered.");
            }

            moduleRegisteredTypes.Count().Should().BeGreaterThan(0);
        }
    }
}
