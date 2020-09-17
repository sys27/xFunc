// Copyright 2012-2020 Dmytro Kyshchenko
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

using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public abstract class BaseParserTests
    {
        protected readonly Parser parser;

        protected BaseParserTests()
        {
            parser = new Parser();
        }

        protected void ParseTest(string function, IExpression expected)
        {
            var exp = parser.Parse(function);

            Assert.Equal(expected, exp);
        }

        protected void ParseErrorTest(string function)
            => Assert.Throws<ParseException>(() => parser.Parse(function));

        protected void ErrorTest<T>(string function) where T : Exception
            => Assert.Throws<T>(() => parser.Parse(function));
    }
}