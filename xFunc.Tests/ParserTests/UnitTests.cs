// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.Converters;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class UnitTests : BaseParserTests
    {
        [Theory]
        [InlineData("convert(1, 'rad')")]
        [InlineData("convert(1, \"rad\")")]
        public void ConvertParseTest(string function)
        {
            var converter = new Converter();
            var convert = new Convert(converter, Number.One, new StringExpression("rad"));

            ParseTest(function, convert);
        }
    }
}