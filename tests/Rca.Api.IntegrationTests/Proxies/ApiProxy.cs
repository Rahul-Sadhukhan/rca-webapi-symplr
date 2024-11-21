// <copyright file="ApiProxy.cs" company="symplr">
// Copyright © 2024 symplr. All rights reserved. Confidential and Proprietary.
// </copyright>

using Orion.Tests.IntegrationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rca.Api.IntegrationTests.Proxies
{
    public class ApiProxy : BaseTest
    {
        public T Get<T>(Uri uri, Action<HttpResponseMessage> responseValidator)
        {
            return Get<T>(uri, null, responseValidator);
        }

        public T Get<T>(Uri uri, object model, Action<HttpResponseMessage> responseValidator)
        {
            using (var response = ResponseHelper.Get(uri, model))
            {
                responseValidator?.Invoke(response);
                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var result = response.Content.ReadAsAsync<T>().Result;
                return result;
            }
        }

        public byte[] GetBytes(Uri uri, Action<HttpResponseMessage> responseValidator)
        {
            using (var response = ResponseHelper.Get(uri))
            {
                responseValidator?.Invoke(response);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = response.Content.ReadAsByteArrayAsync().Result;
                return result;
            }
        }

        public T Post<T>(Uri uri, object body, Action<HttpResponseMessage> responseValidator)
        {
            using (var response = ResponseHelper.Post(uri, body))
            {
                responseValidator?.Invoke(response);
                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var result = response.Content.ReadAsAsync<T>().Result;
                return result;
            }
        }

        public T Put<T>(Uri uri, Action<HttpResponseMessage> responseValidator)
        {
            return Put<T>(uri, null, responseValidator);
        }

        public T Put<T>(Uri uri, object body, Action<HttpResponseMessage> responseValidator)
        {
            using (var response = ResponseHelper.Put(uri, body))
            {
                responseValidator?.Invoke(response);
                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var result = response.Content.ReadAsAsync<T>().Result;
                return result;
            }
        }

        public T Delete<T>(Uri uri, Action<HttpResponseMessage> responseValidator)
        {
            using (var response = ResponseHelper.Delete(uri))
            {
                responseValidator?.Invoke(response);
                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var result = response.Content.ReadAsAsync<T>().Result;
                return result;
            }
        }

        public T PutFile<T>(Uri uri, string fileName, Action<HttpResponseMessage> responseValidator)
        {
            var fileBody = $"FileBody_{Guid.NewGuid().ToString()}";
            var fileDescription = $"FileDescription_{Guid.NewGuid().ToString()}";

            using (var requestContent = new MultipartFormDataContent())
            using (var file = new StringContent(fileBody))
            using (var description = new StringContent(fileDescription))
            {
                requestContent.Add(file, "file", fileName);
                requestContent.Add(description, "description");

                using (var response = ResponseHelper.PutFile(uri, requestContent))
                {
                    responseValidator?.Invoke(response);
                    if (!response.IsSuccessStatusCode)
                    {
                        return default(T);
                    }

                    var responseModel = response.Content.ReadAsAsync<T>().Result;
                    return responseModel;
                }
            }
        }

        public byte[] ExportFile(string url, Action<HttpResponseMessage> responseValidator)
        {
            using (var response = ResponseHelper.Get(UriHelper.CreateUri(url)))
            {
                responseValidator?.Invoke(response);

                var responseModel = response.Content.ReadAsByteArrayAsync().Result;
                return responseModel;
            }
        }
    }
}
