// <copyright file="RcaConfigModel.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Configuration
{
    using Orion.Configuration.Models.Configs.Default;

    /// <summary>
    /// RCA Configuration Model.
    /// </summary>
    public class RcaConfigModel
    {
        /// <summary>
        /// Gets or sets app settings.
        /// </summary>
        public RcaAppSettingsConfig AppSettings { get; set; }

        /// <summary>
        /// Gets or sets http run time.
        /// </summary>
        public HttpRuntimeConfig HttpRuntime { get; set; }

        /// <summary>
        /// Gets or sets connection strings.
        /// </summary>
        public ConnectionStringConfig ConnectionStrings { get; set; }
    }
}
