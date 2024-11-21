// <copyright file="ItemODataControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using System.Collections.Generic;
using System.Linq;
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
    public class ItemODataControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetItems_Always_CallsItemODataService(
            [Frozen] Mock<IItemODataService> mockItemODataService)
        {
            // arrange
            mockItemODataService
                .Setup(x => x.GetItemQueryable())
                .Returns(new List<ItemODataModel>
                {
                    new ItemODataModel
                    {
                        ItemId = 1,
                        Name = "Test Item",
                        Description = "Test Item Description",
                    },
                }.AsQueryable());

            IQueryable<ItemODataModel> result;
            using (var controller = new ItemODataController(mockItemODataService.Object))
            {
                result = controller.GetItem();
            }

            // assert
            result.Should().NotBeNull();
            mockItemODataService.Verify(x => x.GetItemQueryable(), Times.Once);
        }
    }
}
