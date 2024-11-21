// <copyright file="Global.asax.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

namespace Rca.Api
{
    using System.Web.Http;
    using Orion.Multilingual;

    /// <summary>
    /// Main entry point class for the RCA.API application.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class WebApiApplication : System.Web.HttpApplication
#pragma warning restore SA1649 // File name should match first type name
    {
        /// <summary>
        /// Main entry point method that starts application.
        /// </summary>
        protected void Application_Start()
        {
            AutoMapperConfig.Configure();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var multilingualComponent = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IMultilingualComponent)) as IMultilingualComponent;
            multilingualComponent?.Initialize();
        }
    }
}
