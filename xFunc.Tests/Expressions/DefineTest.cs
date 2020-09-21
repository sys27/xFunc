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
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class DefineTest
    {
        [Fact]
        public void InvalidTypeTest()
        {
            Assert.Throws<NotSupportedException>(() => new Define(Number.One, Number.One));
        }

        [Fact]
        public void SimpDefineTest()
        {
            var exp = new Define(Variable.X, Number.One);
            var parameters = new ExpressionParameters();

            var answer = exp.Execute(parameters);

            Assert.Equal(1.0, parameters.Variables["x"]);
            Assert.Equal(1.0, answer);
        }

        [Fact]
        public void DefineWithFuncTest()
        {
            var exp = new Define(Variable.X, new Sin(AngleValue.Radian(1).AsExpression()));
            var parameters = new ParameterCollection();
            var expParams = new ExpressionParameters(parameters);

            var answer = exp.Execute(expParams);

            Assert.Equal(Math.Sin(1), parameters["x"]);
            Assert.Equal(Math.Sin(1), answer);
        }

        [Fact]
        public void DefineExpTest()
        {
            var exp = new Define(Variable.X, new Mul(new Number(4), new Add(new Number(8), Number.One)));
            var parameters = new ExpressionParameters();

            var answer = exp.Execute(parameters);

            Assert.Equal(36.0, parameters.Variables["x"]);
            Assert.Equal(36.0, answer);
        }

        [Fact]
        public void OverrideConstTest()
        {
            var exp = new Define(new Variable("π"), Number.One);
            var parameters = new ExpressionParameters();

            exp.Execute(parameters);

            Assert.Equal(1.0, parameters.Variables["π"]);
        }

        [Fact]
        public void DefineFuncTest()
        {
            var uf = new UserFunction("s", new IExpression[0]);
            var func = new Sin(Number.One);
            var exp = new Define(uf, func);
            var parameters = new ExpressionParameters();

            var result = exp.Execute(parameters);

            Assert.Equal(func, parameters.Functions[uf]);
            Assert.Equal(func, result);
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
            Assert.Equal(func, result);
        }

        [Fact]
        public void ParamsNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Define(new Variable("π"), Number.One).Execute(null));
        }

        [Fact]
        public void ExecuteWithoutParametesTest()
        {
            Assert.Throws<NotSupportedException>(() => new Define(new Variable("π"), Number.One).Execute());
        }

        [Fact]
        public void KeyIsNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Define(null, Number.One));
        }

        [Fact]
        public void ValueIsNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Define(Variable.X, null));
        }

        [Fact]
        public void EqualsSameReferenceTest()
        {
            var def = new Define(Variable.X, Number.One);

            Assert.True(def.Equals(def));
        }

        [Fact]
        public void EqualsDifferentTypesTest()
        {
            var def = new Define(Variable.X, Number.One);
            var number = Number.One;

            Assert.False(def.Equals(number));
        }

        [Fact]
        public void EqualsDifferentOnjectsTest()
        {
            var def1 = new Define(Variable.X, Number.One);
            var def2 = new Define(new Variable("y"), Number.Two);

            Assert.False(def1.Equals(def2));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Define(Variable.X, Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new Define(Variable.X, Number.Zero);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new Define(Variable.X, Number.Zero);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}