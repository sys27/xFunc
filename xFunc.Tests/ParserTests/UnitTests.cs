// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class UnitTests : BaseParserTests
{
    [Test]
    [TestCase("convert(1, 'rad')")]
    [TestCase("convert(1, \"rad\")")]
    public void ConvertParseTest(string function)
    {
        var converter = new Converter();
        var convert = new xFunc.Maths.Expressions.Units.Convert(converter, Number.One, new StringExpression("rad"));

        ParseTest(function, convert);
    }
}