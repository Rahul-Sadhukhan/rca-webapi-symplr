// <copyright file="ParticipantODataControllerTests.cs" company="symplr">
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
    public class ParticipantODataControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetParticipants_Always_CallsParticipantODataService(
            [Frozen] Mock<IParticipantODataService> mockParticipantODataService)
        {
            // arrange
            mockParticipantODataService
                .Setup(x => x.GetParticipantQueryable())
                .Returns(new List<ParticipantODataModel>
                {
                    new ParticipantODataModel
                    {
                        ParticipantId = 1,
                        ParticipantName = "TestParticipantName",
                    },
                }.AsQueryable());

            IQueryable<ParticipantODataModel> result;
            using (var controller = new ParticipantODataController(mockParticipantODataService.Object))
            {
                result = controller.GetParticipant();
            }

            // assert
            result.Should().NotBeNull();
            mockParticipantODataService.Verify(x => x.GetParticipantQueryable(), Times.Once);
        }
    }
}
