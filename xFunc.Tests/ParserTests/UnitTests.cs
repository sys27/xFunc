// Copyright 2012-2021 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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