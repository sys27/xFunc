// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class WhileTests : BaseParserTests
    {
        [Fact]
        public void WhileTest()
        {
            var expected = new While(
                new Define(Variable.X, new Add(Variable.X, Number.One)),
                new Equal(Number.One, Number.One)
            );

            ParseTest("while(x := x + 1, 1 == 1)", expected);
        }

        [Theory]
        [InlineData("while x := x + 1, 1 == 1)")]
        [InlineData("while(, 1 == 1)")]
        [InlineData("while(x := x + 1 1 == 1)")]
        [InlineData("while(x := x + 1, )")]
        [InlineData("while(x := x + 1, 1 == 1")]
        public void WhileMissingPartsTest(string function)
            => ParseErrorTest(function);
    }
}