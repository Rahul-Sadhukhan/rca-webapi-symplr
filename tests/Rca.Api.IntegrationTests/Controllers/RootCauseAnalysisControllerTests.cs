// <copyright file="RootCauseAnalysisControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Orion.Tests.IntegrationHelpers;
using Rca.Api.IntegrationTests.Proxies;
using Rca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rca.Api.IntegrationTests.Controllers
{
    public class RootCauseAnalysisControllerTests : BaseTest
    {
        [Fact]
        public void HttpGet_GetRootCauseAnalysisById_Unauthorized()
        {
            // arrange
            var proxy = new RootCauseAnalysisControllerProxy();

            // act and assert
            proxy.GetRootCauseAnalysisById(1 , message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpGet_GetRootCauseAnalysisById_ReturnsOk()
        {
            // arrange
            var proxy = new RootCauseAnalysisControllerProxy();
            proxy.Authenticate();
            var id = 1;

            // act and assert
            proxy.GetRootCauseAnalysisById(id, message => AssertHttpErrorStatus(message, HttpStatusCode.OK));
        }

        [Fact]
        public void HttpGet_GetRootCauseAnalysisDetails_Unauthorized()
        {
            // arrange
            var proxy = new RootCauseAnalysisControllerProxy();

            // act and assert
            proxy.GetRootCauseAnalysisDetails(1, message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpGet_GetRootCauseAnalysisDetails_ReturnsBadRequest()
        {
            // arrange
            var proxy = new RootCauseAnalysisControllerProxy();
            proxy.Authenticate();
            var id = -1;

            // act and assert
            proxy.GetRootCauseAnalysisDetails(id, message => AssertHttpErrorStatus(message, HttpStatusCode.BadRequest));
        }

        [Fact]
        public void HttpPost_CreateRootCauseAnalysis_Unauthorized()
        {
            // arrange
            var proxy = new RootCauseAnalysisControllerProxy();
            var createRCAModel = new CreateRCAModel
            {
                RcaName = "Test RCA",
                Description = "test description",
                IncidentDate = DateTime.UtcNow,
                Theme = "Test Theme",
                ItemIds = new List<long> { 1, 2 },
                Participants = new List<CreateParticipantModel> { new CreateParticipantModel { ParticipantName = "Test" } },
            };

            // act and assert
            proxy.Create(createRCAModel, message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpPost_CreateRootCauseAnalysis_ReturnsBadRequest()
        {
            // arrange
            var proxy = new RootCauseAnalysisControllerProxy();
            proxy.Authenticate();
            var createRCAModel = new CreateRCAModel
            {
                RcaName = "",
                Description = "test description",
                IncidentDate = DateTime.UtcNow,
                Theme = "Test Theme",
                ItemIds = new List<long> { 1, 2 },
                Participants = new List<CreateParticipantModel> { new CreateParticipantModel { ParticipantName = "Test" } },
            };

            // act and assert
            proxy.Create(createRCAModel, message => AssertHttpErrorStatus(message, HttpStatusCode.BadRequest));
        }
    }
}
