// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public abstract class BaseParserTests
    {
        protected readonly Parser parser;

        protected BaseParserTests() => parser = new Parser();

        protected void ParseTest(string function, IExpression expected)
        {
            var exp = parser.Parse(function);

            Assert.Equal(expected, exp);
        }

        protected void ParseErrorTest(string function)
            => Assert.Throws<ParseException>(() => parser.Parse(function));

        protected void ParseErrorTest<T>(string function) where T : Exception
            => Assert.Throws<T>(() => parser.Parse(function));
    }
}