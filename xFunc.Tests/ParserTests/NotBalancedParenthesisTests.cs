// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class NotBalancedParenthesisTests : BaseParserTests
{
    [Theory]
    [InlineData("sin(2(")]
    [InlineData("sin)2)")]
    [InlineData("sin)2(")]
    [InlineData("{2,1")]
    [InlineData("}2,1")]
    [InlineData("(2")]
    [InlineData("func(2")]
    public void NotBalancedTest(string function)
        => ParseErrorTest(function);
}