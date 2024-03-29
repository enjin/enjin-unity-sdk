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

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Enjin.SDK.Http;
using Moq;
using NUnit.Framework;

namespace Enjin.Tests.Unit
{
    [TestFixture]
    public class ClientHandlerTest
    {
        private HttpMessageHandler MockMessageHandler { get; set; }
        private TestableClientHandler ClassUnderTest { get; set; }

        [SetUp]
        public void BeforeEach()
        {
            MockMessageHandler = Mock.Of<HttpMessageHandler>();
            ClassUnderTest = new TestableClientHandler(MockMessageHandler);
        }

        [Test]
        public void SendAsync_ValidAuthToken_AuthorizationIsSet()
        {
            // Arrange
            const string expectedSchema = "Bearer";
            const string expectedToken = "xyz";
            var dummyCancellationToken = new CancellationToken();
            var request = new HttpRequestMessage();
            ClassUnderTest.AuthToken = expectedToken;

            // Act
            ClassUnderTest.SendAsync(request, dummyCancellationToken);

            // Assert
            Assert.AreEqual(expectedSchema, request.Headers.Authorization.Scheme);
            Assert.AreEqual(expectedToken, request.Headers.Authorization.Parameter);
        }

        [Test]
        public void SendAsync_AuthTokenIsNullOrWhiteSpace_AuthorizationIsNotSet([Values("", "   ", null)] string token)
        {
            // Arrange
            var dummyCancellationToken = new CancellationToken();
            var request = new HttpRequestMessage();
            ClassUnderTest.AuthToken = token;

            Assume.That(request.Headers.Authorization, Is.Null);

            // Act
            ClassUnderTest.SendAsync(request, dummyCancellationToken);

            // Assert
            Assert.IsNull(request.Headers.Authorization);
        }

        [Test]
        public void IsAuthenticated_ValidAuthToken_ReturnsTrue()
        {
            // Arrange
            ClassUnderTest.AuthToken = "xyz";

            // Act
            var actual = ClassUnderTest.IsAuthenticated;

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsAuthenticated_AuthTokenIsNullOrWhiteSpace_ReturnsFalse([Values("", "   ", null)] string token)
        {
            // Arrange
            ClassUnderTest.AuthToken = token;

            // Act
            var actual = ClassUnderTest.IsAuthenticated;

            // Assert
            Assert.IsFalse(actual);
        }

        private class TestableClientHandler : ClientHandler
        {
            public TestableClientHandler(HttpMessageHandler innerHandler) : base(innerHandler)
            {
            }

            public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                           CancellationToken cancellationToken)
            {
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}