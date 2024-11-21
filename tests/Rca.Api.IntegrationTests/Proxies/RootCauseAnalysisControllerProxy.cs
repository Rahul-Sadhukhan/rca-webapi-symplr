// <copyright file="RootCauseAnalysisControllerProxy.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Rca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rca.Api.IntegrationTests.Proxies
{
    public class RootCauseAnalysisControllerProxy : ApiProxy
    {
        public RootCauseAnalysisModel GetRootCauseAnalysisById(int rcaId, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"rootcauseanalysis/{rcaId}");
            return Get<RootCauseAnalysisModel>(uri, responseValidator);
        }

        public RootCauseAnalysisModel GetRootCauseAnalysisDetails(int rcaId, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"rootcauseanalysisdetails/{rcaId}");
            return Get<RootCauseAnalysisModel>(uri, responseValidator);
        }

        public CreateRCAModel Create(CreateRCAModel createRcaModel, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"create");
            return Post<CreateRCAModel>(uri, createRcaModel, responseValidator);
        }
    }
}
