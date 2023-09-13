// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class WhileTests : BaseParserTests
{
    [Test]
    public void WhileTest()
    {
        var expected = new While(
            new Assign(Variable.X, new Add(Variable.X, Number.One)),
            new Equal(Number.One, Number.One)
        );

        ParseTest("while(x := x + 1, 1 == 1)", expected);
    }

    [Test]
    [TestCase("while x := x + 1, 1 == 1)")]
    [TestCase("while(, 1 == 1)")]
    [TestCase("while(x := x + 1 1 == 1)")]
    [TestCase("while(x := x + 1, )")]
    [TestCase("while(x := x + 1, 1 == 1")]
    public void WhileMissingPartsTest(string function)
        => ParseErrorTest(function);
}