// <copyright file="CauseControllerProxy.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Rca.Data.Entities;
using Rca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rca.Api.IntegrationTests.Proxies
{
    public class CauseControllerProxy : ApiProxy
    {
        private const string CausesEndPoint = "causes";

        public IEnumerable<ClassificationModel> GetClassifications(Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"{CausesEndPoint}/classifications");
            return Get<IEnumerable<ClassificationModel>>(uri, responseValidator);
        }

        public CauseModel GetCauseById(int causeId, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"{CausesEndPoint}/{causeId}");
            return Get<CauseModel>(uri, responseValidator);
        }

        public CauseModel CreateCause(CauseModel model, int rcaId, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"{CausesEndPoint}/{rcaId}");
            return Post<CauseModel>(uri, model, responseValidator);
        }

        public List<CauseSelectListModel> GetRCAClassificationCauses(int rcaId, int classificationId, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"{CausesEndPoint}/rootcauseanalysis/{rcaId}/classifications/{classificationId}");
            return Get<List<CauseSelectListModel>>(uri, responseValidator);
        }

        public IEnumerable<ClassificationModel> GetClassificationsByRCAId(int rcaId, Action<HttpResponseMessage> responseValidator)
        {
            var uri = UriHelper.CreateUri($"{CausesEndPoint}/rootcauseanalysis/{rcaId}");
            return Get<IEnumerable<ClassificationModel>>(uri, responseValidator);
        }
    }
}
