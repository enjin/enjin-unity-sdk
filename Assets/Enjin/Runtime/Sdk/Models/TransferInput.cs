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

using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Enjin.SDK.Models
{
    /// <summary>
    /// Models transfer input when making requests.
    /// </summary>
    /// <seealso cref="Enjin.SDK.PlayerSchema.AdvancedSendAsset"/>
    /// <seealso cref="Enjin.SDK.ProjectSchema.AdvancedSendAsset"/>
    [PublicAPI]
    public class TransferInput
    {
        [JsonProperty("from")]
        [CanBeNull]
        private string _from;

        [JsonProperty("to")]
        [CanBeNull]
        private string _to;

        [JsonProperty("assetId")]
        [CanBeNull]
        private string _assetId;

        [JsonProperty("assetIndex")]
        [CanBeNull]
        private string _assetIndex;

        [JsonProperty("value")]
        [CanBeNull]
        private string _value;

        /// <summary>
        /// Sets the source of the funds.
        /// </summary>
        /// <param name="address">The source.</param>
        /// <returns>This input chaining.</returns>
        public TransferInput From([CanBeNull] string address)
        {
            _from = address;
            return this;
        }

        /// <summary>
        /// Sets the destination of the funds.
        /// </summary>
        /// <param name="address">The destination.</param>
        /// <returns>This input chaining.</returns>
        public TransferInput To([CanBeNull] string address)
        {
            _to = address;
            return this;
        }

        /// <summary>
        /// Sets the asset ID to transfer or ENJ if not used or set to <c>null</c>.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>This input chaining.</returns>
        public TransferInput AssetId([CanBeNull] string id)
        {
            _assetId = id;
            return this;
        }

        /// <summary>
        /// Sets the index for non-fungible assets.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>This input chaining.</returns>
        public TransferInput AssetIndex([CanBeNull] string index)
        {
            _assetIndex = index;
            return this;
        }

        /// <summary>
        /// Sets the number of assets to transfer.
        /// </summary>
        /// <param name="value">The amount.</param>
        /// <returns>This input chaining.</returns>
        /// <remarks>
        /// If transferring ENJ, the value is the amount to send in Wei (10^18 e.g. 1 ENJ = 1000000000000000000).
        /// </remarks>
        public TransferInput Value([CanBeNull] string value)
        {
            _value = value;
            return this;
        }
    }
}