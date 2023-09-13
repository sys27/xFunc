// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class LogicalOperatorTests : BaseParserTests
{
    [Test]
    [TestCase("~2")]
    [TestCase("not(2)")]
    public void NotTest(string function)
        => ParseTest(function, new Not(Number.Two));

    [Test]
    [TestCase("true & false")]
    [TestCase("true and false")]
    public void BoolConstTest(string function)
        => ParseTest(function, new And(Bool.True, Bool.False));

    [Test]
    public void LogicAddPriorityTest()
    {
        var expected = new And(
            new GreaterThan(new Number(3), new Number(4)),
            new LessThan(Number.One, new Number(3))
        );

        ParseTest("3 > 4 & 1 < 3", expected);
    }

    [Test]
    [TestCase("1 | 2")]
    [TestCase("1 or 2")]
    public void OrTest(string function)
        => ParseTest(function, new Or(Number.One, Number.Two));

    [Test]
    public void XOrTest()
        => ParseTest("1 xor 2", new XOr(Number.One, Number.Two));

    [Test]
    public void NOrTest()
        => ParseTest("true nor true", new NOr(Bool.True, Bool.True));

    [Test]
    public void NAndTest()
        => ParseTest("true nand true", new NAnd(Bool.True, Bool.True));

    [Test]
    public void ImplicationTest()
        => ParseTest("true impl true", new Implication(Bool.True, Bool.True));

    [Test]
    public void EqualityTest()
        => ParseTest("true eq true", new Equality(Bool.True, Bool.True));

    [Test]
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