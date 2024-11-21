// <copyright file="ParticipantODataControllerProxy.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Orion.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rca.Api.IntegrationTests.Proxies.OData
{
    internal class ParticipantODataControllerProxy : ApiProxy
    {
        private const string ParticipantEndpoint = "/odata/participants";

        public ODataResponseModel<List<T>> GetParticipants<T>(int skip, int top, Action<HttpResponseMessage> responseValidator)
        {
            var url = UriHelper.CreateUri($"{ParticipantEndpoint}?&$count=true&$skip={skip}&$top={top}");
            var result = Get<ODataResponseModel<List<T>>>(url, responseValidator);
            return result;
        }
    }
}
