// <copyright file="CauseControllerTests.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using FluentAssertions;
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
    public class CauseControllerTests : BaseTest
    {
        [Fact]
        public void HttpGet_GetClassifications_NoAuthentication_Unauthorized()
        {
            // arrange
            var proxy = new CauseControllerProxy();

            // act and assert
            proxy.GetClassifications(message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpGet_GetClassifications_Authentication_Returns200ResponseAndGetsClassifications()
        {
            // arrange
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();

            // act and assert
            var surveys = proxy.GetClassifications(AssertHttpSuccessStatus);

            surveys.Should().NotBeNull();
        }

        [Fact]
        public void HttpGet_GetCauseById_NoAuthentication_Unauthorized()
        {
            // arrange
            var proxy = new CauseControllerProxy();

            // act and assert
            proxy.GetCauseById(20, message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpGet_GetCauseById_ValidCauseIdPassed_Authentication()
        {
            // arrange
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();

            // act and assert
            proxy.GetCauseById(1, message => AssertHttpErrorStatus(message, HttpStatusCode.OK));
        }

        [Fact]
        public void HttpPost_CreateCause_Authentication_And_SuccessfullyCreateQuestion()
        {
            // arrange
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();
            var model = new CauseModel
            {
                CauseId = 0,
                RCAId = 1,
                Description = "sample text",
                ClassificationId = 1,
                ParentCauseId = null,
            };

            // act and assert
            proxy.CreateCause(
                model,
                1,
                message => AssertHttpErrorStatus(message, HttpStatusCode.OK));
        }

        [Fact]
        public void HttpPost_CreateCause_NoAuthentication_Unauthorized()
        {
            // arrange
            var proxy = new CauseControllerProxy();
            var model = new CauseModel
            {
                RCAId = 1,
            };

            // act and assert
            proxy.CreateCause(
                model,
                2,
                message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpPost_CreateCause_InValidRCAIdPassed_ReturnsValidationException()
        {
            // arrange
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();
            var model = new CauseModel
            {
                CauseId = 0,
                RCAId = 1,
                Description = "sample text",
                ClassificationId = 1,
                ParentCauseId = null,
            };

            // act and assert
            proxy.CreateCause(model, -1, message => AssertHttpErrorStatus(message, HttpStatusCode.BadRequest));
        }

        [Fact]
        public void HttpGet_GetRCAClassificationCauses_NoAuthentication_Unauthorized()
        {
            // arrange
            var rcaId = 1;
            var classificationId = 1;
            var proxy = new CauseControllerProxy();

            // act and assert
            proxy.GetRCAClassificationCauses(rcaId, classificationId, message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpGet_GetRCAClassificationCauses_InValidParametersPassed_Authenticated_ReturnsBadRequest()
        {
            // arrange
            var rcaId = -1;
            var classificationId = 1;
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();

            // act and assert
            proxy.GetRCAClassificationCauses(rcaId, classificationId, message => AssertHttpErrorStatus(message, HttpStatusCode.BadRequest));
        }

        [Fact]
        public void HttpGet_GetRCAClassificationCauses_ValidParametersPassed_Authentication()
        {
            // arrange
            var rcaId = 1;
            var classificationId = 1;
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();

            // act and assert
            proxy.GetRCAClassificationCauses(rcaId, classificationId, message => AssertHttpErrorStatus(message, HttpStatusCode.OK));
        }

        [Fact]
        public void HttpGet_GetClassificationsByRCAId_NoAuthentication_Unauthorized()
        {
            // arrange
            var rcaId = 1;
            var proxy = new CauseControllerProxy();

            // act and assert
            proxy.GetClassificationsByRCAId(rcaId, message => AssertHttpErrorStatus(message, HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void HttpGet_GetClassificationsByRCAId_InValidParameterPassed_Authenticated_ReturnsBadRequest()
        {
            // arrange
            var rcaId = -1;
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();

            // act and assert
            proxy.GetClassificationsByRCAId(rcaId, message => AssertHttpErrorStatus(message, HttpStatusCode.BadRequest));
        }

        [Fact]
        public void HttpGet_GetClassificationsByRCAId_ValidParametersPassed_Authentication()
        {
            // arrange
            var rcaId = 1;
            var proxy = new CauseControllerProxy();
            proxy.Authenticate();

            // act and assert
            proxy.GetClassificationsByRCAId(rcaId, message => AssertHttpErrorStatus(message, HttpStatusCode.OK));
        }
    }
}
