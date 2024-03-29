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
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Enjin.SDK.Graphql;
using Enjin.SDK.Http;
using Enjin.SDK.Utils;
using JetBrains.Annotations;

namespace Enjin.SDK
{
    /// <summary>
    /// Middleware for communicating with the platform.
    /// </summary>
    /// <seealso cref="PlayerClient"/>
    /// <seealso cref="ProjectClient"/>
    [PublicAPI]
    public class ClientMiddleware
    {
        /// <summary>
        /// The handler for communication with the platform.
        /// </summary>
        public readonly ClientHandler HttpHandler;

        /// <summary>
        /// The HTTP client for sending requests and receiving responses.
        /// </summary>
        public readonly HttpClient HttpClient;

        /// <summary>
        /// The query registry being used.
        /// </summary>
        public readonly GraphqlQueryRegistry Registry;

        private static readonly string USER_AGENT_VERSION;

        static ClientMiddleware()
        {
            USER_AGENT_VERSION = typeof(ClientMiddleware)
                                 .Assembly
                                 .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                                 .First()
                                 .InformationalVersion;
        }

        /// <summary>
        /// Sole constructor.
        /// </summary>
        /// <param name="baseAddress">The base URI.</param>
        /// <param name="logLevel">The HTTP log level.</param>
        /// <param name="loggerProvider">The logger provider.</param>
        public ClientMiddleware(Uri baseAddress,
                                HttpLogLevel logLevel = HttpLogLevel.NONE,
                                [CanBeNull] LoggerProvider loggerProvider = null)
        {
            HttpHandler = CreateHttpHandler(logLevel, loggerProvider);
            HttpClient = CreateHttpClient(baseAddress);
            Registry = new GraphqlQueryRegistry();
        }

        private HttpClient CreateHttpClient(Uri baseAddress)
        {
            var client = new HttpClient(HttpHandler)
            {
                BaseAddress = baseAddress,
            };

            client.DefaultRequestHeaders.UserAgent.TryParseAdd($"Enjin Unity SDK v{USER_AGENT_VERSION}");

            return client;
        }

        private ClientHandler CreateHttpHandler(HttpLogLevel logLevel,
                                                [CanBeNull] LoggerProvider loggerProvider = null)
        {
            var clientHandler = new HttpClientHandler();
            return logLevel == HttpLogLevel.NONE || loggerProvider == null
                ? new ClientHandler(clientHandler)
                : new ClientHandler(new HttpLoggingHandler(logLevel, loggerProvider, clientHandler));
        }
    }
}