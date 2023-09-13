// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class StringTests : BaseParserTests
{
    [Test]
    [TestCase("\"\"", "")]
    [TestCase("''", "")]
    [TestCase("\"hello\"", "hello")]
    [TestCase("'hello'", "hello")]
    [TestCase("\"hello, 'inline'\"", "hello, 'inline'")]
    [TestCase("'hello, \"inline\"'", "hello, \"inline\"")]
    [TestCase("\"sin(x)\"", "sin(x)")]
    public void Quotes(string exp, string result)
        => ParseTest(exp, new StringExpression(result));

    [Test]
    [TestCase("\"hello")]
    [TestCase("'hello")]
    [TestCase("hello\"")]
    [TestCase("hello'")]
    [TestCase("\"hello, 'inside'")]
    public void MissingQuote(string exp)
        => ParseErrorTest<TokenizeException>(exp);
}