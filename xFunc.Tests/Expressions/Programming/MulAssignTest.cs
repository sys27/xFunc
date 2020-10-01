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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class MulAssignTest
    {
        [Fact]
        public void MulAssignCalc()
        {
            var parameters = new ParameterCollection { new Parameter("x", 10) };
            var mul = new MulAssign(Variable.X, Number.Two);
            var result = mul.Execute(parameters);
            var expected = 20.0;

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void MulAssignAsExpressionTest()
        {
            var parameters = new ParameterCollection { new Parameter("x", 10) };
            var add = new Add(Number.Two, new MulAssign(Variable.X, Number.Two));
            var result = add.Execute(parameters);

            Assert.Equal(22.0, result);
            Assert.Equal(20.0, parameters["x"]);
        }

        [Fact]
        public void MulNullParameters()
        {
            var exp = new MulAssign(Variable.X, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Execute());
        }

        [Fact]
        public void MulValueBoolParameters()
        {
            var exp = new MulAssign(Variable.X, Bool.False);
            var parameters = new ParameterCollection { new Parameter("x", 1) };

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
        }

        [Fact]
        public void BoolMulNumberTest()
        {
            var parameters = new ParameterCollection { new Parameter("x", true) };
            var mul = new MulAssign(Variable.X, Number.Two);

            Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute(parameters));
        }

        [Fact]
        public void SameEqualsTest()
        {
            var exp = new MulAssign(Variable.X, Number.One);

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualsNullTest()
        {
            var exp = new MulAssign(Variable.X, Number.One);

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void EqualsDifferentTypeTest()
        {
            var exp1 = new MulAssign(Variable.X, Number.One);
            var exp2 = new DivAssign(Variable.X, Number.One);

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new MulAssign(Variable.X, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new MulAssign(Variable.X, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new MulAssign(Variable.X, Number.Two);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}