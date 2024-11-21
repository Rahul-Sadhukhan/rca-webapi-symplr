// <copyright file="RcaDashboardODataControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Linq;
using System.Web.Http.Results;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Orion.Tests.Attributes.AutoFixtureAttributes;
using Rca.Api.Controllers.OData;
using Rca.Models.OData;
using Rca.Services.OData;
using Xunit;

namespace Rca.Api.Tests.Controllers.OData
{
    public class RcaDashboardODataControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetRcaDashboardInfo_CallsRcaDashboardODataService_WithCorrectArguments(
            [Frozen] Mock<IRcaDashboardODataService> mockRcaDashboardODataService)
        {
            // arrange
            using (var controller = new RcaDashboardODataController(mockRcaDashboardODataService.Object))
            {
                // act
                var result = controller.GetRcaDashboardInfo();

                // assert
                mockRcaDashboardODataService.Verify(x => x.GetRcaDashboardInfoQueryable(), Times.Once);
                result.Should().NotBeNull();
                result.Should().BeOfType(typeof(OkNegotiatedContentResult<IQueryable<RcaDashboardODataModel>>));
            }
        }
    }
}
