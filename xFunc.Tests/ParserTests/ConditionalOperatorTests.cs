// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ConditionalOperatorTests : BaseParserTests
{
    [Fact]
    public void ConditionalAndTest()
    {
        var expected = new ConditionalAnd(
            new Equal(Variable.X, Number.Zero),
            new NotEqual(Variable.Y, Number.Zero)
        );

        ParseTest("x == 0 && y != 0", expected);
    }

    [Fact]
    public void ConditionalOrTest()
    {
        var expected = new ConditionalOr(
            new Equal(Variable.X, Number.Zero),
            new NotEqual(Variable.Y, Number.Zero)
        );

        ParseTest("x == 0 || y != 0", expected);
    }

    [Fact]
    public void ConditionalPrecedenceTest()
    {
        var expected = new ConditionalOr(
            Variable.X,
            new ConditionalAnd(Variable.Y, new Variable("z"))
        );

        ParseTest("x || y && z", expected);
    }

    [Theory]
    [InlineData("x == 0 &&")]
    [InlineData("x == 0 ||")]
    public void ConditionalMissingSecondOperand(string function)
        => ParseErrorTest(function);
}