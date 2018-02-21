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
using System.Numerics;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokens
{

    public class ComplexNumberTokenTest
    {

        [Fact]
        public void EqualsNullTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, 2.1));

            Assert.False(token.Equals(null));
            Assert.NotNull(token);
        }

        [Fact]
        public void EqualsSameObjectTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, 2.1));

            Assert.True(token.Equals(token));
            Assert.Equal(token, token);
        }

        [Fact]
        public void EqualsDiffTypeTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, 2.1));

            Assert.False(token.Equals(1));
            Assert.NotEqual((object)1, token);
        }

        [Fact]
        public void EqualsDiffComplexTest()
        {
            var token1 = new ComplexNumberToken(new Complex(5.3, 2.1));
            var token2 = new ComplexNumberToken(new Complex(5.9, 43.1));

            Assert.False(token1.Equals(token2));
            Assert.NotEqual(token1, token2);
        }

        [Fact]
        public void ZeroImToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, 0));

            Assert.Equal("Complex Number: 5.3", token.ToString());
        }

        [Fact]
        public void PositiveImToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, 2.12));

            Assert.Equal("Complex Number: 5.3+2.12i", token.ToString());
        }

        [Fact]
        public void NegativeImToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, -2.12));

            Assert.Equal("Complex Number: 5.3-2.12i", token.ToString());
        }

        [Fact]
        public void ZeroReToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(0, 1.3));

            Assert.Equal("Complex Number: 1.3i", token.ToString());
        }

        [Fact]
        public void PositiveReToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(5.3, 2.12));

            Assert.Equal("Complex Number: 5.3+2.12i", token.ToString());
        }

        [Fact]
        public void NegativeReToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(-5.3, -2.12));

            Assert.Equal("Complex Number: -5.3-2.12i", token.ToString());
        }

        [Fact]
        public void ImOneToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(0, 1));

            Assert.Equal("Complex Number: i", token.ToString());
        }

        [Fact]
        public void ImNegOneToStringTest()
        {
            var token = new ComplexNumberToken(new Complex(0, -1));

            Assert.Equal("Complex Number: -i", token.ToString());
        }

    }

}
