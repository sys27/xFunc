// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class IfTests : BaseParserTests
{
    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    [TestCase("if x == 0 && y != 0, 2, 8)")]
    [TestCase("if(, 2, 8)")]
    [TestCase("if(x == 0 && y != 0 2, 8)")]
    [TestCase("if(x == 0 && y != 0, , 8)")]
    [TestCase("if(x == 0 && y != 0, 2 8)")]
    [TestCase("if(x == 0 && y != 0, 2, )")]
    [TestCase("if(x == 0 && y != 0, 2, 8")]
    public void IfMissingPartsTest(string function)
        => ParseErrorTest(function);

    [Test]
    public void TernaryTest()
    {
        var expected = new If(
            Bool.True,
            Number.One,
            new UnaryMinus(Number.One)
        );

        ParseTest("true ? 1 : -1", expected);
    }

    [Test]
    [TestCase("true ? 1 :")]
    [TestCase("true ? : -1")]
    [TestCase("true ? 1")]
    public void TernaryMissingTest(string function)
        => ParseErrorTest(function);

    [Test]
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