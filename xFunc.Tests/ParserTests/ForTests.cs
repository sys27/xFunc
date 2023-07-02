// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ForTests : BaseParserTests
{
    [Fact]
    public void ForTest()
    {
        var expected = new For(
            Number.Two,
            new Assign(Variable.X, Number.Zero),
            new LessThan(Variable.X, new Number(10)),
            new Assign(Variable.X, new Add(Variable.X, Number.One))
        );

        ParseTest("for(2, x := 0, x < 10, x := x + 1)", expected);
    }

    [Fact]
    public void ForWithIncTest()
    {
        var expected = new For(
            Number.Two,
            new Assign(Variable.X, Number.Zero),
            new LessThan(Variable.X, new Number(10)),
            new Inc(Variable.X)
        );

        ParseTest("for(2, x := 0, x < 10, x++)", expected);
    }

    [Theory]
    [InlineData("for 2, x := 0, x < 10, x++)")]
    [InlineData("for(, x := 0, x < 10, x++)")]
    [InlineData("for(2 x := 0, x < 10, x++)")]
    [InlineData("for(2, , x < 10, x++)")]
    [InlineData("for(2, x := z x < 10, x++)")]
    [InlineData("for(2, x := 0, , x++)")]
    [InlineData("for(2, x := 0, x < 10 x++)")]
    [InlineData("for(2, x := 0, x < 10, )")]
    [InlineData("for(2, x := 0, x < 10, x++")]
    public void ForMissingPartsTest(string function)
        => ParseErrorTest(function);
}