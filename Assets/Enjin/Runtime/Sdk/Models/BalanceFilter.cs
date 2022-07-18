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

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Enjin.SDK.Models
{
    /// <summary>
    /// Models a filter input for balance queries.
    /// </summary>
    /// <seealso cref="Enjin.SDK.Shared.GetBalances"/>
    [PublicAPI]
    public class BalanceFilter : Filter<BalanceFilter>
    {
        [JsonProperty("assetId")]
        [CanBeNull]
        private string _assetId;

        [JsonProperty("assetId_in")]
        [CanBeNull]
        private List<string> _assetIdIn;

        [JsonProperty("wallet")]
        [CanBeNull]
        private string _wallet;

        [JsonProperty("wallet_in")]
        [CanBeNull]
        private List<string> _walletIn;

        [JsonProperty("value")]
        private int? _value;

        [JsonProperty("value_is")]
        private Operator? _valueIs;

        /// <summary>
        /// Sets the asset ID to filter for.
        /// </summary>
        /// <param name="assetId">The asset ID.</param>
        /// <returns>This filter for chaining.</returns>
        public BalanceFilter AssetId([CanBeNull] string assetId)
        {
            _assetId = assetId;
            return this;
        }

        /// <summary>
        /// Sets the asset IDs to filter for.
        /// </summary>
        /// <param name="assetIds">The asset IDs.</param>
        /// <returns>This filter for chaining.</returns>
        public BalanceFilter AssetIdIn([CanBeNull] params string[] assetIds)
        {
            _assetIdIn = assetIds?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the filter to include balances equal to the passed value.
        /// </summary>
        /// <param name="value">The value to compare by.</param>
        /// <returns>This filter for chaining.</returns>
        public BalanceFilter Value(int? value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        /// Sets the filter operator type for values.
        /// </summary>
        /// <param name="valueIs">The operator for comparison.</param>
        /// <returns>This filter for chaining.</returns>
        public BalanceFilter ValueIs(Operator? valueIs)
        {
            _valueIs = valueIs;
            return this;
        }

        /// <summary>
        /// Sets the wallet to filter for.
        /// </summary>
        /// <param name="wallet">The wallet address.</param>
        /// <returns>This filter for chaining.</returns>
        public BalanceFilter Wallet([CanBeNull] string wallet)
        {
            _wallet = wallet;
            return this;
        }

        /// <summary>
        /// Sets the wallets to filter for.
        /// </summary>
        /// <param name="wallets">The wallet addresses.</param>
        /// <returns>This filter for chaining.</returns>
        public BalanceFilter WalletIn([CanBeNull] params string[] wallets)
        {
            _walletIn = wallets?.ToList();
            return this;
        }
    }
}