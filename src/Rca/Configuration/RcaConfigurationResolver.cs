// <copyright file="RcaConfigurationResolver.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Configuration
{
    using System.Collections.Generic;
    using Orion.Configuration.ConfigurationResolvers;
    using Orion.Configuration.Models.Configs.Default;

    /// <summary>
    /// RCA configuration resolver.
    /// </summary>
    public class RcaConfigurationResolver : ConfigurationResolverBase, IConfigurationResolver<RcaConfigModel>
    {
        public RcaConfigModel Resolve(Dictionary<string, object> configurationKeyValuePairs)
        {
            var appsettingsConfig = FilterConfigurationByTemplate(configurationKeyValuePairs, "App:AppSettings:");
            var connectionStringsConfig = FilterConfigurationByTemplate(configurationKeyValuePairs, "App:ConnectionStrings:");
            var httpRuntimeConfig = FilterConfigurationByTemplate(configurationKeyValuePairs, "App:HttpRuntime:");

            var rcaConfigModel = new RcaConfigModel()
            {
                AppSettings = BindDictionaryToModel<RcaAppSettingsConfig>(appsettingsConfig),
                ConnectionStrings = BindDictionaryToModel<ConnectionStringConfig>(connectionStringsConfig),
                HttpRuntime = BindDictionaryToModel<HttpRuntimeConfig>(httpRuntimeConfig),
            };

            Validate(rcaConfigModel); // validate via base class

            return rcaConfigModel;
        }
    }
}
