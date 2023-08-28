// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class NotBalancedParenthesisTests : BaseParserTests
{
    [Test]
    [TestCase("sin(2(")]
    [TestCase("sin)2)")]
    [TestCase("sin)2(")]
    [TestCase("{2,1")]
    [TestCase("}2,1")]
    [TestCase("(2")]
    [TestCase("func(2")]
    public void NotBalancedTest(string function)
        => ParseErrorTest(function);
}