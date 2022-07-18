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

using Enjin.SDK.Models;
using NUnit.Framework;

namespace Enjin.Tests.Unit
{
    [TestFixture]
    public class NotificationEventTest
    {
        private const EventType DefaultEventType = EventType.UNKNOWN;
        private const string DefaultChannel = "xyz";
        private const string DefaultMessage = @"{'name': 'value'}";

        [Theory]
        public void Data_GetsDeserializedData()
        {
            // Arrange
            var notificationEvent = CreateDefaultFakeNotificationEvent();
            
            Assume.That(notificationEvent.Message, Is.Not.Null.And.Not.Empty);
            
            // Act
            var actual = notificationEvent.Data;

            // Assert
            Assert.NotNull(actual);
        }

        private static NotificationEvent CreateDefaultFakeNotificationEvent() =>
            new NotificationEvent(DefaultEventType, DefaultChannel, DefaultMessage);
    }
}