// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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