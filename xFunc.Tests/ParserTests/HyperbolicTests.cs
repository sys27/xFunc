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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class HyperbolicTests : BaseParserTests
    {
        [Theory]
        [InlineData("sinh(2)")]
        [InlineData("sh(2)")]
        public void SinhTest(string function)
            => ParseTest(function, new Sinh(Number.Two));

        [Theory]
        [InlineData("cosh(2)")]
        [InlineData("ch(2)")]
        public void CoshTest(string function)
            => ParseTest(function, new Cosh(Number.Two));

        [Theory]
        [InlineData("tanh(2)")]
        [InlineData("th(2)")]
        public void TanhTest(string function)
            => ParseTest(function, new Tanh(Number.Two));

        [Theory]
        [InlineData("coth(2)")]
        [InlineData("cth(2)")]
        public void CothTest(string function)
            => ParseTest(function, new Coth(Number.Two));

        [Fact]
        public void SechTest()
            => ParseTest("sech(2)", new Sech(Number.Two));

        [Fact]
        public void CschTest()
            => ParseTest("csch(2)", new Csch(Number.Two));
    }
}