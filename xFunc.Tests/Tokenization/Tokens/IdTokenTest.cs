// Copyright 2012-2020 Dmytro Kyshchenko
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

using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization.Tokens
{
    public class IdTokenTest
    {
        [Fact]
        public void EqualsNullTest()
        {
            var token = new IdToken("x");

            Assert.False(token.Equals(null));
            Assert.NotNull(token);
        }

        [Fact]
        public void EqualsSameObjectTest()
        {
            var token = new IdToken("x");

            Assert.True(token.Equals(token));
            Assert.Equal(token, token);
        }

        [Fact]
        public void EqualsDiffTypeTest()
        {
            var token = new IdToken("x");

            Assert.False(token.Equals(1));
            Assert.NotEqual((object) 1, token);
        }

        [Fact]
        public void EqualsDiffVarTest()
        {
            var token1 = new IdToken("x");
            var token2 = new IdToken("y");

            Assert.False(token1.Equals(token2));
            Assert.NotEqual(token1, token2);
        }

        [Fact]
        public void ToStringTest()
        {
            var variable = "x";
            var token = new IdToken(variable);

            Assert.Equal(variable, token.ToString());
        }
    }
}