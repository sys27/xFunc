// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class DefineTests : BaseParserTests
{
    [Fact]
    public void DefTest()
        => ParseTest("def(x, 2)", new Define(Variable.X, Number.Two));

    [Theory]
    [InlineData("def x, 2)")]
    [InlineData("def(, 2)")]
    [InlineData("def(x 2)")]
    [InlineData("def(x,)")]
    [InlineData("def(x, 2")]
    public void DefMissingOpenParen(string function)
        => ParseErrorTest(function);
}