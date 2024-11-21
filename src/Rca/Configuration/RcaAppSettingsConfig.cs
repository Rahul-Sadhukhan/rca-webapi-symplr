// <copyright file="RcaAppSettingsConfig.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Configuration
{
    using Orion.Configuration.Models.Configs.WebApi;

    /// <summary>
    /// RCA AppSettings Configurations.
    /// </summary>
    public class RcaAppSettingsConfig : AppSettingsConfig
    {
        /// <summary>
        /// Gets or sets application path.
        /// </summary>
        public string ApplicationPath { get; set; }

        /// <summary>
        /// Gets or sets GlobalAtlasUrl.
        /// </summary>
        public string GlobalAtlasUrl { get; set; }
    }
}
