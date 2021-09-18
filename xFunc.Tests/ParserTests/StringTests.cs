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

using xFunc.Maths;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.ParserTests
{
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
}