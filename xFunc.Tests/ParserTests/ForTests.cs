// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ForTests : BaseParserTests
{
    [Test]
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

    [Test]
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

    [Test]
    [TestCase("for 2, x := 0, x < 10, x++)")]
    [TestCase("for(, x := 0, x < 10, x++)")]
    [TestCase("for(2 x := 0, x < 10, x++)")]
    [TestCase("for(2, , x < 10, x++)")]
    [TestCase("for(2, x := z x < 10, x++)")]
    [TestCase("for(2, x := 0, , x++)")]
    [TestCase("for(2, x := 0, x < 10 x++)")]
    [TestCase("for(2, x := 0, x < 10, )")]
    [TestCase("for(2, x := 0, x < 10, x++")]
    public void ForMissingPartsTest(string function)
        => ParseErrorTest(function);
}