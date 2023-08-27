// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public abstract class BaseParserTests
{
    protected readonly Parser parser;

    protected BaseParserTests() => parser = new Parser();

    protected void ParseTest(string function, IExpression expected)
    {
        var exp = parser.Parse(function);

        Assert.That(exp, Is.EqualTo(expected));
    }

    protected void ParseErrorTest(string function)
        => Assert.Throws<ParseException>(() => parser.Parse(function));

    protected void ParseErrorTest<T>(string function) where T : Exception
        => Assert.Throws<T>(() => parser.Parse(function));
}