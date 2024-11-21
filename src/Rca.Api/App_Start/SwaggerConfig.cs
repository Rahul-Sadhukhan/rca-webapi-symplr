// <copyright file="SwaggerConfig.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;
using Rca.Api.App_Start;
using Swashbuckle.Application;
using Swashbuckle.OData;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Rca.Api.App_Start
{
    /// <summary>
    /// Swagger configuration.
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Method to call Swagger registration for this API application.
        /// </summary>
        public static void Register()
        {
            bool.TryParse(ConfigurationManager.AppSettings["EnableSwagger"], out bool isSwaggerEnabled);

            if (!isSwaggerEnabled)
            {
                return;
            }

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                    // hold additional metadata for an API. Version and title are required but you can also provide
                    // additional fields by chaining methods off SingleApiVersion.
                    c.SingleApiVersion("v1", "Rca.Api");

                    // If you want the output Swagger docs to be indented properly, enable the "PrettyPrint" option.
                    c.PrettyPrint();

                    // If you annotate Controllers and API Types with
                    // Xml comments (http://msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                    // those comments into the generated docs and UI. You can enable this by providing the path to one or
                    // more Xml comment files.
                    c.IncludeXmlComments(GetXmlCommentsPath());

                    // Wrap the default SwaggerGenerator with additional behavior (e.g. caching) or provide an
                    // alternative implementation for ISwaggerProvider with the CustomProvider option.
                    c.CustomProvider(defaultProvider => new ODataSwaggerProvider(defaultProvider, c, GlobalConfiguration.Configuration));
                })
                .EnableSwaggerUi(c =>
                {
                    // By default, swagger-ui will validate specs against swagger.io's online validator and display the result
                    // in a badge at the bottom of the page. Use these options to set a different validator URL or to disable the
                    // feature entirely.
                    c.DisableValidator();
                });
        }

        private static string GetXmlCommentsPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\RCA.API.XML");
        }
    }
}