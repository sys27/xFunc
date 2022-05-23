// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class RelationalOperatorTests : BaseParserTests
{
    [Fact]
    public void LessThenTest()
    {
        var expected = new LessThan(Variable.X, Number.Zero);

        ParseTest("x < 0", expected);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var expected = new LessOrEqual(Variable.X, Number.Zero);

        ParseTest("x <= 0", expected);
    }

    [Fact]
    public void GreaterThenTest()
    {
        var expected = new GreaterThan(Variable.X, Number.Zero);

        ParseTest("x > 0", expected);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var expected = new GreaterOrEqual(Variable.X, Number.Zero);

        ParseTest("x >= 0", expected);
    }

    [Fact]
    public void PrecedenceTest()
    {
        var expected = new NotEqual(
            new Variable("a"),
            new LessThan(new Variable("b"), new Variable("c"))
        );

        ParseTest("a != b < c", expected);
    }
}