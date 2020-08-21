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
    public class AndTest
    {
        [Fact]
        public void CalculateAndTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(Variable.X, new Number(10));
            var greaterThen = new GreaterThan(Variable.X, new Number(-10));
            var and = new ConditionalAnd(lessThen, greaterThen);

            Assert.True((bool) and.Execute(parameters));
        }

        [Fact]
        public void CalculateAndFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(Variable.X, new Number(10));
            var greaterThen = new GreaterThan(Variable.X, new Number(10));
            var and = new ConditionalAnd(lessThen, greaterThen);

            Assert.False((bool) and.Execute(parameters));
        }

        [Fact]
        public void CalculateInvalidParametersTest()
        {
            var and = new ConditionalAnd(new Number(1), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => and.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var lessThen = new LessThan(Variable.X, new Number(10));
            var greaterThen = new GreaterThan(Variable.X, new Number(10));
            var exp = new ConditionalAnd(lessThen, greaterThen);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}