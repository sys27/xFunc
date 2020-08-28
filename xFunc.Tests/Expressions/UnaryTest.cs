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
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class UnaryTest
    {
        [Fact]
        public void EqualsTest1()
        {
            var sine1 = new Sin(Number.Two);
            var sine2 = new Sin(Number.Two);

            Assert.Equal(sine1, sine2);
        }

        [Fact]
        public void EqualsTest2()
        {
            var sine = new Sin(Number.Two);
            var ln = new Ln(Number.Two);

            Assert.NotEqual<IExpression>(sine, ln);
        }

        [Fact]
        public void ArgNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Sin(null));
        }
    }
}