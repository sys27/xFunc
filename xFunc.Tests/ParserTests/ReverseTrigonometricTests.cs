// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class ReverseTrigonometricTests : BaseParserTests
    {
        [Fact]
        public void ArcsinTest()
            => ParseTest("arcsin(2)", new Arcsin(Number.Two));

        [Fact]
        public void ArccosTest()
            => ParseTest("arccos(2)", new Arccos(Number.Two));

        [Theory]
        [InlineData("arctan(2)")]
        [InlineData("arctg(2)")]
        public void ArctanTest(string function)
            => ParseTest(function, new Arctan(Number.Two));

        [Theory]
        [InlineData("arccot(2)")]
        [InlineData("arcctg(2)")]
        public void ArccotTest(string function)
            => ParseTest(function, new Arccot(Number.Two));

        [Fact]
        public void ArcsecTest()
            => ParseTest("arcsec(2)", new Arcsec(Number.Two));

        [Theory]
        [InlineData("arccsc(2)")]
        [InlineData("arccosec(2)")]
        public void ArccscTest(string function)
            => ParseTest(function, new Arccsc(Number.Two));
    }
}