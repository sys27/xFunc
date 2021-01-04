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

using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class DivAssignTest
    {
        [Fact]
        public void DivAssignCalc()
        {
            var parameters = new ParameterCollection { new Parameter("x", 10) };
            var div = new DivAssign(Variable.X, Number.Two);
            var result = div.Execute(parameters);
            var expected = new NumberValue(5.0);

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void DivAssignAsExpressionTest()
        {
            var parameters = new ParameterCollection { new Parameter("x", 10) };
            var add = new Add(new DivAssign(Variable.X, Number.Two), Number.Two);
            var result = add.Execute(parameters);

            Assert.Equal(new NumberValue(7.0), result);
            Assert.Equal(new NumberValue(5.0), parameters["x"]);
        }

        [Fact]
        public void DivNullParameters()
        {
            var exp = new DivAssign(Variable.X, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Execute());
        }

        [Fact]
        public void DivValueBoolParameters()
        {
            var exp = new DivAssign(Variable.X, Bool.False);
            var parameters = new ParameterCollection { new Parameter("x", 1) };

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
        }

        [Fact]
        public void BoolDivNumberTest()
        {
            var parameters = new ParameterCollection { new Parameter("x", true) };
            var add = new DivAssign(Variable.X, Number.Two);

            Assert.Throws<ResultIsNotSupportedException>(() => add.Execute(parameters));
        }

        [Fact]
        public void SameEqualsTest()
        {
            var exp = new DivAssign(Variable.X, Number.One);

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualsNullTest()
        {
            var exp = new DivAssign(Variable.X, Number.One);

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void EqualsDifferentTypeTest()
        {
            var exp1 = new DivAssign(Variable.X, Number.One);
            var exp2 = new MulAssign(Variable.X, Number.One);

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new DivAssign(Variable.X, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new DivAssign(Variable.X, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new DivAssign(Variable.X, Number.Two);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}