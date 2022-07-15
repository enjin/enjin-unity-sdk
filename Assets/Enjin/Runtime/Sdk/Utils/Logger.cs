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
using JetBrains.Annotations;
using UnityEngine;

namespace Enjin.SDK.Utils
{
    /// <summary>
    /// Basic logger class for logging messages using the <see cref="Debug"/> class.
    /// </summary>
    [PublicAPI]
    public class Logger : ILogger
    {
        /// <summary>
        /// Logs the message at the given logging level. Only utilizes <see cref="LogLevel.INFO"/>,
        /// <see cref="LogLevel.WARN"/>, and <see cref="LogLevel.ERROR"/> log levels.
        /// </summary>
        /// <param name="level">The logging level.</param>
        /// <param name="message">The message.</param>
        public void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.INFO:
                    Debug.Log(message);
                    break;
                case LogLevel.WARN:
                    Debug.LogWarning(message);
                    break;
                case LogLevel.ERROR:
                    Debug.LogError(message);
                    break;
            }
        }

        /// <summary>
        /// Formats and logs the message and exception. Ignores the logging level.
        /// </summary>
        /// <param name="level">The logging level (ignored).</param>
        /// <param name="message">The message.</param>
        /// <param name="e">The exception.</param>
        public void Log(LogLevel level, string message, Exception e)
        {
            Debug.LogException(new Exception(message, e));
        }

        /// <inheritdoc/>
        public bool IsLoggable(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.INFO:
                case LogLevel.WARN:
                case LogLevel.ERROR:
                    return true;
                default:
                    return false;
            }
        }
    }
}