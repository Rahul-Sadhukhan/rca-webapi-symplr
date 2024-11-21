// <copyright file="ItemODataControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using AutoFixture.Xunit2;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Orion.Tests.IntegrationHelpers;
using Rca.Api.IntegrationTests.Proxies.OData;
using Rca.Models.OData;
using Rca.Services.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rca.Api.IntegrationTests.Controllers.OData
{
    public class ItemODataControllerTests : BaseTest
    {
        [Theory]
        [InlineData(25)]
        [InlineData(50)]
        public void GetItems_NoAuthentication_Unauthorized(int top)
        {
            // arrange
            var proxy = new ItemODataControllerProxy();

            // act and assert
            proxy.GetItems<dynamic>(0, top, message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Theory]
        [InlineData(25)]
        [InlineData(50)]
        public void GetItems_Valid_Returns200Response(int top)
        {
            // arrange
            var proxy = new ItemODataControllerProxy();
            proxy.Authenticate();

            // act and assert
            var survey = proxy.GetItems<dynamic>(0, top, AssertHttpSuccessStatus);

            survey.Should().NotBeNull();
        }

    }
}
