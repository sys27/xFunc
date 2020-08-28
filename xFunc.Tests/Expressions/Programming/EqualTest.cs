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
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class EqualTest
    {
        [Fact]
        public void NumberEqualTest()
        {
            var equal = new Equal(new Number(10), new Number(10));
            var result = (bool) equal.Execute();

            Assert.True(result);
        }

        [Fact]
        public void NumberVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", 10),
                new Parameter("y", 10)
            };
            var equal = new Equal(Variable.X, new Variable("y"));
            var result = (bool) equal.Execute(parameters);

            Assert.True(result);
        }

        [Fact]
        public void BoolTrueEqualTest()
        {
            var equal = new Equal(Bool.True, Bool.True);
            var result = (bool) equal.Execute();

            Assert.True(result);
        }

        [Fact]
        public void BoolTrueVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", true),
                new Parameter("y", true)
            };
            var equal = new Equal(Variable.X, new Variable("y"));
            var result = (bool) equal.Execute(parameters);

            Assert.True(result);
        }

        [Fact]
        public void BoolTrueAndFalseEqualTest()
        {
            var equal = new Equal(Bool.True, Bool.False);
            var result = (bool) equal.Execute();

            Assert.False(result);
        }

        [Fact]
        public void BoolTrueAndFalseVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", true),
                new Parameter("y", false)
            };
            var equal = new Equal(Variable.X, new Variable("y"));
            var result = (bool) equal.Execute(parameters);

            Assert.False(result);
        }

        [Fact]
        public void BoolFalseEqualTest()
        {
            var equal = new Equal(Bool.False, Bool.False);
            var result = (bool) equal.Execute();

            Assert.True(result);
        }

        [Fact]
        public void BoolFalseVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", false),
                new Parameter("y", false)
            };
            var equal = new Equal(Variable.X, new Variable("y"));
            var result = (bool) equal.Execute(parameters);

            Assert.True(result);
        }

        [Fact]
        public void CalculateInvalidParametersTest()
        {
            var equal = new Equal(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

            Assert.Throws<ResultIsNotSupportedException>(() => equal.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Equal(Number.Two, new Number(3));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}