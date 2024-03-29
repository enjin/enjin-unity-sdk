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
using Enjin.SDK.Http;
using Enjin.SDK.Utils;
using JetBrains.Annotations;

namespace Enjin.SDK
{
    /// <summary>
    /// Client for using the player schema.
    /// </summary>
    /// <seealso cref="EnjinHosts"/>
    [PublicAPI]
    public class PlayerClient : PlayerSchema.PlayerSchema, IClient
    {
        /// <inheritdoc/>
        public bool IsAuthenticated => Middleware.HttpHandler.IsAuthenticated;

        /// <inheritdoc/>
        public bool IsClosed { get; private set; }

        private PlayerClient(Uri baseUri, HttpLogLevel httpLogLevel, [CanBeNull] LoggerProvider loggerProvider)
            : base(new ClientMiddleware(baseUri, httpLogLevel, loggerProvider), loggerProvider)
        {
        }

        ~PlayerClient()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public void Auth(string token)
        {
            Middleware.HttpHandler.AuthToken = token;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Middleware.HttpClient.Dispose();
            IsClosed = true;
        }

        /// <summary>
        /// Creates a builder for this class.
        /// </summary>
        /// <returns>The builder.</returns>
        public static PlayerClientBuilder Builder()
        {
            return new PlayerClientBuilder();
        }

        /// <summary>
        /// Builder class for <see cref="PlayerClient"/>.
        /// </summary>
        [PublicAPI]
        public class PlayerClientBuilder
        {
            private HttpLogLevel? _httpLogLevel;

            [CanBeNull]
            private Uri _baseUri;

            [CanBeNull]
            private LoggerProvider _loggerProvider;

            internal PlayerClientBuilder()
            {
            }

            /// <summary>
            /// Builds the client.
            /// </summary>
            /// <returns>The client.</returns>
            /// <exception cref="InvalidOperationException">
            /// Thrown if the base URI is a null value at the time this method is called.
            /// </exception>
            [NotNull]
            public PlayerClient Build()
            {
                if (_baseUri == null)
                    throw new InvalidOperationException($"Cannot build {nameof(PlayerClient)} with null base URI.");

                return new PlayerClient(_baseUri,
                                        _httpLogLevel ?? Http.HttpLogLevel.NONE,
                                        _loggerProvider);
            }

            /// <summary>
            /// Sets the base URI the client will be using.
            /// </summary>
            /// <param name="baseUri">The base URI.</param>
            /// <returns>This builder for chaining.</returns>
            /// <seealso cref="EnjinHosts"/>
            public PlayerClientBuilder BaseUri(Uri baseUri)
            {
                _baseUri = baseUri;
                return this;
            }

            /// <summary>
            /// Sets the log level for HTTP traffic.
            /// </summary>
            /// <param name="logLevel">The log level.</param>
            /// <returns>This builder for chaining.</returns>
            public PlayerClientBuilder HttpLogLevel(HttpLogLevel logLevel)
            {
                _httpLogLevel = logLevel;
                return this;
            }

            /// <summary>
            /// Sets the logger provider for the client to use.
            /// </summary>
            /// <param name="loggerProvider">The logger provider.</param>
            /// <returns>This builder for chaining.</returns>
            public PlayerClientBuilder LoggerProvider(LoggerProvider loggerProvider)
            {
                _loggerProvider = loggerProvider;
                return this;
            }
        }
    }
}