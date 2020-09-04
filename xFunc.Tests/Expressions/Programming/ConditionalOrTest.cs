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
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class ConditionalOrTest
    {
        [Fact]
        public void CalculateOrTrueTest1()
        {
            var parameters = new ParameterCollection { new Parameter("x", 0) };
            var lessThen = new LessThan(Variable.X, new Number(10));
            var greaterThen = new GreaterThan(Variable.X, new Number(-10));
            var or = new ConditionalOr(lessThen, greaterThen);

            Assert.True((bool) or.Execute(parameters));
        }

        [Fact]
        public void CalculateOrTrueTest2()
        {
            var parameters = new ParameterCollection { new Parameter("x", 0) };
            var lessThen = new LessThan(Variable.X, new Number(-10));
            var greaterThen = new GreaterThan(Variable.X, new Number(-10));
            var or = new ConditionalOr(lessThen, greaterThen);

            Assert.True((bool) or.Execute(parameters));
        }

        [Fact]
        public void ExecuteInvalidParametersTest()
        {
            var or = new ConditionalOr(Number.One, Number.Two);

            Assert.Throws<ResultIsNotSupportedException>(() => or.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var lessThen = new LessThan(Variable.X, new Number(10));
            var greaterThen = new GreaterThan(Variable.X, new Number(10));
            var exp = new ConditionalOr(lessThen, greaterThen);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}