﻿/* Copyright 2021 Enjin Pte. Ltd.
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

using Enjin.SDK.Graphql;
using JetBrains.Annotations;

namespace Enjin.SDK.ProjectSchema
{
    /// <summary>
    /// Request for completing a trade between two wallets.
    /// </summary>
    /// <seealso cref="CreateTrade"/>
    /// <seealso cref="IProjectSchema"/>
    [PublicAPI]
    public class CompleteTrade : GraphqlRequest<CompleteTrade>, ITransactionRequestArguments<CompleteTrade>
    {
        /// <summary>
        /// Sole constructor.
        /// </summary>
        public CompleteTrade() : base("enjin.sdk.project.CompleteTrade")
        {
        }

        /// <summary>
        /// Sets the trade ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>This request for chaining.</returns>
        public CompleteTrade TradeId([CanBeNull] string id)
        {
            return SetVariable("tradeId", id);
        }
    }
}