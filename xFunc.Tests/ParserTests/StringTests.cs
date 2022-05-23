// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class StringTests : BaseParserTests
{
    [Theory]
    [InlineData("\"\"", "")]
    [InlineData("''", "")]
    [InlineData("\"hello\"", "hello")]
    [InlineData("'hello'", "hello")]
    [InlineData("\"hello, 'inline'\"", "hello, 'inline'")]
    [InlineData("'hello, \"inline\"'", "hello, \"inline\"")]
    [InlineData("\"sin(x)\"", "sin(x)")]
    public void Quotes(string exp, string result)
        => ParseTest(exp, new StringExpression(result));

    [Theory]
    [InlineData("\"hello")]
    [InlineData("'hello")]
    [InlineData("hello\"")]
    [InlineData("hello'")]
    [InlineData("\"hello, 'inside'")]
    public void MissingQuote(string exp)
        => ParseErrorTest<TokenizeException>(exp);
}