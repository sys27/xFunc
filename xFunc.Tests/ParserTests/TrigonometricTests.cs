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
    public class TrigonometricTests : BaseParserTests
    {
        [Fact]
        public void SinTest()
            => ParseTest("sin(2)", new Sin(Number.Two));

        [Fact]
        public void CosTest()
            => ParseTest("cos(2)", new Cos(Number.Two));

        [Theory]
        [InlineData("tan(2)")]
        [InlineData("tg(2)")]
        public void TanTest(string function)
            => ParseTest(function, new Tan(Number.Two));

        [Theory]
        [InlineData("cot(2)")]
        [InlineData("ctg(2)")]
        public void CotTest(string function)
            => ParseTest(function, new Cot(Number.Two));

        [Fact]
        public void SecTest()
            => ParseTest("sec(2)", new Sec(Number.Two));

        [Theory]
        [InlineData("csc(2)")]
        [InlineData("cosec(2)")]
        public void CscTest(string function)
            => ParseTest(function, new Csc(Number.Two));
    }
}