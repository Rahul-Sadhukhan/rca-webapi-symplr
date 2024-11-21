// <copyright file="BaseTest.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Orion.Common.Models;
using Rca.Tests.Models;

namespace Rca.Tests
{
    public abstract class BaseTest
    {
        protected BaseTest()
        {
            AutoMapperConfig.Configure();
        }

        protected static HttpRequestMessage CreateSimpleHttpRequestMessage()
        {
            using (var httpRequestMessage = new HttpRequestMessage())
            {
                using (var httpConfiguration = new HttpConfiguration())
                {
                    httpConfiguration.EnableDependencyInjection();
                    httpRequestMessage.SetConfiguration(httpConfiguration);
                }

                return httpRequestMessage;
            }
        }

        protected void AssertExceptionMessage(ErrorModel errorModel, string message)
        {
            errorModel.Should().NotBeNull();
            var errorModelResult = JsonConvert.DeserializeObject<ResultErrorModel>(JsonConvert.SerializeObject(errorModel));
            errorModelResult.Should().NotBeNull();
            errorModelResult.Errors.Should().NotBeNull();
            errorModelResult.Errors.Count().Should().BeGreaterThan(0);
            foreach (var item in errorModelResult.Errors)
            {
                item.Message.Should().Be(message);
            }
        }
    }
}
