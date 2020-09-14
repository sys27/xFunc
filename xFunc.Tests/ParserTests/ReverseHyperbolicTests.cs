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
    public class ReverseHyperbolicTests : BaseParserTests
    {
        [Theory]
        [InlineData("arsinh(2)")]
        [InlineData("arsh(2)")]
        public void ArsinhTest(string function)
            => ParseTest(function, new Arsinh(Number.Two));

        [Theory]
        [InlineData("arcosh(2)")]
        [InlineData("arch(2)")]
        public void ArcoshTest(string function)
            => ParseTest(function, new Arcosh(Number.Two));

        [Theory]
        [InlineData("artanh(2)")]
        [InlineData("arth(2)")]
        public void ArtanhTest(string function)
            => ParseTest(function, new Artanh(Number.Two));

        [Theory]
        [InlineData("arcoth(2)")]
        [InlineData("arcth(2)")]
        public void ArcothTest(string function)
            => ParseTest(function, new Arcoth(Number.Two));

        [Theory]
        [InlineData("arsech(2)")]
        [InlineData("arsch(2)")]
        public void ArsechTest(string function)
            => ParseTest(function, new Arsech(Number.Two));

        [Fact]
        public void ArcschTest()
            => ParseTest("arcsch(2)", new Arcsch(Number.Two));
    }
}