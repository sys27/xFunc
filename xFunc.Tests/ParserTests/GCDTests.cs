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

using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class GCDTests : BaseParserTests
    {
        [Theory]
        [InlineData("gcd(12, 16)")]
        [InlineData("gcf(12, 16)")]
        [InlineData("hcf(12, 16)")]
        public void GCDTest(string function)
            => ParseTest(function, new GCD(new Number(12), new Number(16)));

        [Fact]
        public void GCDOfThreeTest()
        {
            var expected = new GCD(new IExpression[] { new Number(12), new Number(16), new Number(8) });

            ParseTest("gcd(12, 16, 8)", expected);
        }

        [Fact]
        public void UnaryMinusAfterCommaTest()
        {
            var expected = new GCD(Number.Two, new UnaryMinus(Variable.X));

            ParseTest("gcd(2, -x)", expected);
        }

        [Theory]
        [InlineData("lcm(12, 16)")]
        [InlineData("scm(12, 16)")]
        public void LCMTest(string function)
            => ParseTest(function, new LCM(new Number(12), new Number(16)));
    }
}