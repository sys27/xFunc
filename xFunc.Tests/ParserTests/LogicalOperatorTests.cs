// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class LogicalOperatorTests : BaseParserTests
{
    [Theory]
    [InlineData("~2")]
    [InlineData("not(2)")]
    public void NotTest(string function)
        => ParseTest(function, new Not(Number.Two));

    [Theory]
    [InlineData("true & false")]
    [InlineData("true and false")]
    public void BoolConstTest(string function)
        => ParseTest(function, new And(Bool.True, Bool.False));

    [Fact]
    public void LogicAddPriorityTest()
    {
        var expected = new And(
            new GreaterThan(new Number(3), new Number(4)),
            new LessThan(Number.One, new Number(3))
        );

        ParseTest("3 > 4 & 1 < 3", expected);
    }

    [Theory]
    [InlineData("1 | 2")]
    [InlineData("1 or 2")]
    public void OrTest(string function)
        => ParseTest(function, new Or(Number.One, Number.Two));

    [Fact]
    public void XOrTest()
        => ParseTest("1 xor 2", new XOr(Number.One, Number.Two));

    [Fact]
    public void NOrTest()
        => ParseTest("true nor true", new NOr(Bool.True, Bool.True));

    [Fact]
    public void NAndTest()
        => ParseTest("true nand true", new NAnd(Bool.True, Bool.True));

    [Theory]
    [InlineData("true -> true")]
    [InlineData("true −> true")]
    [InlineData("true => true")]
    [InlineData("true impl true")]
    public void ImplicationTest(string function)
        => ParseTest(function, new Implication(Bool.True, Bool.True));

    [Theory]
    [InlineData("true <-> true")]
    [InlineData("true <−> true")]
    [InlineData("true <=> true")]
    [InlineData("true eq true")]
    public void EqualityTest(string function)
        => ParseTest(function, new Equality(Bool.True, Bool.True));

    [Fact]
    public void AndXOrOrPrecedenceTest()
    {
        var expected = new Or(
            new Variable("a"),
            new XOr(
                new Variable("b"),
                new And(
                    new Variable("c"),
                    new Variable("d")
                )
            )
        );

        ParseTest("a | b xor c & d", expected);
    }
}