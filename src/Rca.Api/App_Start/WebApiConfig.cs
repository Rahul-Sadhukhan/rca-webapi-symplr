// <copyright file="WebApiConfig.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Orion.Api.DelegatingHandlers;
using Orion.Api.Helpers;
using Orion.Api.Loggers;
using Orion.Clients;
using Orion.Configuration;
using Orion.Configuration.Models;
using Orion.Logging;
using Orion.Users;
using Rca.Api.App_Start;
using Rca.Models.OData;

namespace Rca.Api
{
    /// <summary>
    /// WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            var jsonNetSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Include,
                Formatting = Formatting.None,
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            };

            config.Formatters.JsonFormatter.SerializerSettings = jsonNetSettings;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MessageHandlers.Add(new SwaggerAccessMessageHandler());

            AutofacConfig.Register(config);

            if (config.DependencyResolver.GetService(typeof(IConfigurationComponent<DefaultConfigurationModel>)) is IConfigurationComponent<DefaultConfigurationModel> configurationComponent && configurationComponent.GetConfiguration().CorsAllowed)
            {
                // Enable cors for any origin for debug configuration
                var corsAttr = new EnableCorsAttribute("*", "*", "*");
                config.EnableCors(corsAttr);
            }

            // Web API routes
            config.MapHttpAttributeRoutes();

            ConfigureOData(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
            var globalExceptionLogger = new GlobalExceptionLogger(
                (ILoggingComponent)config.DependencyResolver.GetService(typeof(ILoggingComponent)),
                (IUsersComponent)config.DependencyResolver.GetService(typeof(IUsersComponent)),
                (IClientComponent)config.DependencyResolver.GetService(typeof(IClientComponent)));
            config.Services.Add(typeof(IExceptionLogger), globalExceptionLogger);
        }

        private static void ConfigureOData(HttpConfiguration config)
        {
            // Web API OData routes
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<RcaDashboardODataModel>("dashboard").EntityType.HasKey(e => e.RcaId);
            builder.EntitySet<ParticipantODataModel>("participants").EntityType.HasKey(e => e.ParticipantId);
            builder.EntitySet<ItemODataModel>("items").EntityType.HasKey(e => e.ItemId);

            // Format enum value in odata response to be returned from [EnumMember] attribute instead of the enum value itself.
            RegisterEnums(builder);

            // Force all model properties to be in lowercase.
            builder.EnableLowerCamelCase();
            var edmModel = builder.GetEdmModel();
            config.MapODataServiceRoute("odata", "odata", edmModel);
            config.AddODataQueryFilter(
                new EnableQueryAttribute
                {
                    AllowedQueryOptions = AllowedQueryOptions.All,
                });

            // Configure allowed query operators
            config.Select().MaxTop(100);

            config.EnableDependencyInjection();

            // allow all filters in odata
            config.Select().Expand().Filter().OrderBy().MaxTop(500).Count();

            // configure odata to work with UTC dates.
            config.SetTimeZoneInfo(TimeZoneInfo.Utc);
        }

        private static void RegisterEnums(ODataModelBuilder builder)
        {
            var publicEnums = Assembly.Load("Rca")
                .GetTypes()
                .Where(t => t.IsEnum && t.IsPublic)
                .ToList();

            // Iterate through all public enums and check all enum values for attribute EnumMemberAttribute. If the enum value is decorated with the attribute then explicitly set it's name to the attribute's value.
            foreach (var publicEnum in publicEnums)
            {
                var enumTypeConfiguration = builder.AddEnumType(publicEnum);
                var enumValues = publicEnum.GetEnumValues();
                foreach (var enumValue in enumValues)
                {
                    // OData queries do not map EnumMemberAttribute descriptions well so we have to explicitly set the enum name.
                    var memberInfo = publicEnum.GetMember(enumValue.ToString()).Where(x => x.GetCustomAttributes(typeof(EnumMemberAttribute)).Any()).ToList();
                    if (!memberInfo.Any())
                    {
                        continue;
                    }

                    var enumMemberAttribute = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>();
                    enumTypeConfiguration.Members.AsQueryable().Single(x => x.Name == memberInfo[0].Name).Name = enumMemberAttribute.Value;
                }
            }
        }
    }
}
