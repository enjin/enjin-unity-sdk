/* Copyright 2021 Enjin Pte. Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Enjin.SDK.Graphql;
using Enjin.SDK.Serialization;
using Enjin.SDK.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Enjin.SDK
{
    /// <summary>
    /// Base class for schema with functionality to send GraphQL requests to the platform and process the responses.
    /// </summary>
    [PublicAPI]
    public class BaseSchema
    {
        /// <summary>
        /// Represents the logger provider that this class may use.
        /// </summary>
        /// <value>The logger provider.</value>
        [CanBeNull]
        public LoggerProvider LoggerProvider { get; private set; }

        /// <summary>
        /// The middleware of this class.
        /// </summary>
        protected readonly ClientMiddleware Middleware;

        /// <summary>
        /// The schema type of this class.
        /// </summary>
        protected readonly string Schema;

        private static readonly Encoding ENCODING = Encoding.UTF8;
        private static readonly string JSON = "application/json";

        private static readonly JsonSerializerSettings SERIALIZER_SETTINGS = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver(),
            Converters = { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore,
        };

        /// <summary>
        /// Sole constructor.
        /// </summary>
        /// <param name="middleware">The middleware.</param>
        /// <param name="schema">The schema.</param>
        /// <param name="loggerProvider">The logger provider.</param>
        protected BaseSchema(ClientMiddleware middleware,
                             string schema,
                             [CanBeNull] LoggerProvider loggerProvider)
        {
            Middleware = middleware;
            Schema = schema;
            LoggerProvider = loggerProvider;
        }

        /// <summary>
        /// Sends a request asynchronously to the platform and provides the task that will contain the response.
        /// </summary>
        /// <param name="request">The GraphQL request.</param>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <returns>The task for this operation.</returns>
        [ItemCanBeNull]
        protected Task<GraphqlResponse<T>> SendRequest<T>(IGraphqlRequest request)
        {
            var uri = new Uri(Middleware.HttpClient.BaseAddress, $"/graphql/{Schema}");
            var payload = CreateRequestBody(request).ToString();
            var content = new StringContent(payload, ENCODING, JSON);

            return Middleware.HttpClient.PostAsync(uri, content).ContinueWith(task =>
            {
                var result = task.Result;

                try
                {
                    return JsonConvert.DeserializeObject<GraphqlResponse<T>>(result.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    LoggerProvider?.Log(LogLevel.ERROR,
                                        "An error occured while processing a request or a response.",
                                        e);
                    return null;
                }
                finally
                {
                    result?.Dispose();
                }
            });
        }

        /// <summary>
        /// Creates the serialized request body to be sent to the platform.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The serialized object.</returns>
        private JObject CreateRequestBody(IGraphqlRequest request)
        {
            var query = Middleware.Registry.GetOperationForName(request.Namespace)?.CompiledContents;
            var variables = JsonConvert.SerializeObject(request.Variables, Formatting.Indented, SERIALIZER_SETTINGS);

            return new JObject
            {
                { "query", query },
                { "variables", variables }
            };
        }
    }
}