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
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class DefineTest
    {

        [Fact]
        public void SimpDefineTest()
        {
            var exp = new Define(Variable.X, new Number(1));
            var parameters = new ExpressionParameters();

            var answer = exp.Execute(parameters);

            Assert.Equal(1.0, parameters.Variables["x"]);
            Assert.Equal("The value '1' was assigned to the variable 'x'.", answer);
        }

        [Fact]
        public void DefineWithFuncTest()
        {
            var exp = new Define(Variable.X, new Sin(new Number(1)));
            var parameters = new ParameterCollection();
            var expParams = new ExpressionParameters(AngleMeasurement.Radian, parameters);

            var answer = exp.Execute(expParams);

            Assert.Equal(Math.Sin(1), parameters["x"]);
            Assert.Equal("The value 'sin(1)' was assigned to the variable 'x'.", answer);
        }

        [Fact]
        public void DefineExpTest()
        {
            var exp = new Define(Variable.X, new Mul(new Number(4), new Add(new Number(8), new Number(1))));
            var parameters = new ExpressionParameters();

            var answer = exp.Execute(parameters);

            Assert.Equal(36.0, parameters.Variables["x"]);
            Assert.Equal("The value '4 * (8 + 1)' was assigned to the variable 'x'.", answer);
        }

        [Fact]
        public void OverrideConstTest()
        {
            var exp = new Define(new Variable("π"), new Number(1));
            var parameters = new ExpressionParameters();

            exp.Execute(parameters);

            Assert.Equal(1.0, parameters.Variables["π"]);
        }

        [Fact]
        public void DefineFuncTest()
        {
            var uf = new UserFunction("s", new IExpression[0]);
            var func = new Sin(new Number(1));
            var exp = new Define(uf, func);
            var parameters = new ExpressionParameters();

            var result = exp.Execute(parameters);

            Assert.Equal(func, parameters.Functions[uf]);
            Assert.Equal("The expression 'sin(1)' was assigned to the function 's()'.", result);
        }

        [Fact]
        public void DefineFuncWithParamsTest()
        {
            var uf = new UserFunction("s", new IExpression[] { Variable.X });
            var func = new Sin(Variable.X);
            var exp = new Define(uf, func);
            var parameters = new ExpressionParameters();

            var result = exp.Execute(parameters);

            Assert.Equal(func, parameters.Functions[uf]);
            Assert.Equal("The expression 'sin(x)' was assigned to the function 's(x)'.", result);
        }

        [Fact]
        public void ParamsNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Define(new Variable("π"), new Number(1)).Execute(null));
        }

        [Fact]
        public void ExecuteWithoutParametesTest()
        {
            Assert.Throws<NotSupportedException>(() => new Define(new Variable("π"), new Number(1)).Execute());
        }

        [Fact]
        public void KeyIsNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Define(null, new Number(1)));
        }

        [Fact]
        public void ValueIsNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Define(Variable.X, null));
        }

        [Fact]
        public void EqualsSameReferenceTest()
        {
            var def = new Define(Variable.X, new Number(1));

            Assert.True(def.Equals(def));
        }

        [Fact]
        public void EqualsDifferentTypesTest()
        {
            var def = new Define(Variable.X, new Number(1));
            var number = new Number(1);

            Assert.False(def.Equals(number));
        }

        [Fact]
        public void EqualsDifferentOnjectsTest()
        {
            var def1 = new Define(Variable.X, new Number(1));
            var def2 = new Define(new Variable("y"), new Number(2));

            Assert.False(def1.Equals(def2));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Define(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}