// Copyright 2012-2018 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokens
{

    public class OperationTokenTest
    {

        [Fact]
        public void EqualsNullTest()
        {
            var token = new OperationToken(Operations.Multiplication);

            Assert.False(token.Equals(null));
            Assert.NotNull(token);
        }

        [Fact]
        public void EqualsSameObjectTest()
        {
            var token = new OperationToken(Operations.Multiplication);

            Assert.True(token.Equals(token));
            Assert.Equal(token, token);
        }

        [Fact]
        public void EqualsDiffTypeTest()
        {
            var token = new OperationToken(Operations.Multiplication);

            Assert.False(token.Equals(1));
            Assert.NotEqual((object)1, token);
        }

        [Fact]
        public void EqualsDiffOperationTest()
        {
            var token1 = new OperationToken(Operations.Multiplication);
            var token2 = new OperationToken(Operations.Subtraction);

            Assert.False(token1.Equals(token2));
            Assert.NotEqual(token1, token2);
        }

        [Fact]
        public void ToStringTest()
        {
            var token = new OperationToken(Operations.Multiplication);

            Assert.Equal("Operation: Multiplication", token.ToString());
        }

    }

}
