// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class IfTests : BaseParserTests
{
    [Fact]
    public void IfThenElseTest()
    {
        var expected = new If(
            new ConditionalAnd(
                new Equal(Variable.X, Number.Zero),
                new NotEqual(Variable.Y, Number.Zero)),
            Number.Two,
            new Number(8)
        );

        ParseTest("if(x == 0 && y != 0, 2, 8)", expected);
    }

    [Fact]
    public void IfThenElseAsExpressionTest()
    {
        var expected = new Add(
            Number.One,
            new If(
                new Equal(Variable.X, Number.Zero),
                Number.Two,
                new Number(8)
            )
        );

        ParseTest("1 + if(x == 0, 2, 8)", expected);
    }

    [Fact]
    public void IfThenTest()
    {
        var expected = new If(
            new ConditionalAnd(
                new Equal(Variable.X, Number.Zero),
                new NotEqual(Variable.Y, Number.Zero)
            ),
            Number.Two
        );

        ParseTest("if(x == 0 && y != 0, 2)", expected);
    }

    [Theory]
    [InlineData("if x == 0 && y != 0, 2, 8)")]
    [InlineData("if(, 2, 8)")]
    [InlineData("if(x == 0 && y != 0 2, 8)")]
    [InlineData("if(x == 0 && y != 0, , 8)")]
    [InlineData("if(x == 0 && y != 0, 2 8)")]
    [InlineData("if(x == 0 && y != 0, 2, )")]
    [InlineData("if(x == 0 && y != 0, 2, 8")]
    public void IfMissingPartsTest(string function)
        => ParseErrorTest(function);

    [Fact]
    public void TernaryTest()
    {
        var expected = new If(
            Bool.True,
            Number.One,
            new UnaryMinus(Number.One)
        );

        ParseTest("true ? 1 : -1", expected);
    }

    [Theory]
    [InlineData("true ? 1 :")]
    [InlineData("true ? : -1")]
    [InlineData("true ? 1")]
    public void TernaryMissingTest(string function)
        => ParseErrorTest(function);

    [Fact]
    public void TernaryAsExpressionTest()
    {
        var expected = new Sin(
            new If(
                Bool.True,
                Number.One,
                new UnaryMinus(Number.One)
            )
        );

        ParseTest("sin(true ? 1 : -1)", expected);
    }
}