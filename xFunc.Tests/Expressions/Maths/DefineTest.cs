// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{
    
    public class DefineTest
    {

        [Fact]
        public void SimpDefineTest()
        {
            IExpression exp = new Define(new Variable("x"), new Number(1));
            ParameterCollection parameters = new ParameterCollection();

            var answer = exp.Execute(parameters);

            Assert.Equal(1.0, parameters["x"]);
            Assert.Equal(double.NaN, answer);
        }

        [Fact]
        public void DefineWithFuncTest()
        {
            IExpression exp = new Define(new Variable("x"), new Sin(new Number(1)));
            ParameterCollection parameters = new ParameterCollection();
            ExpressionParameters expParams = new ExpressionParameters(AngleMeasurement.Radian, parameters);

            var answer = exp.Execute(expParams);

            Assert.Equal(Math.Sin(1), parameters["x"]);
            Assert.Equal(double.NaN, answer);
        }

        [Fact]
        public void DefineExpTest()
        {
            IExpression exp = new Define(new Variable("x"), new Mul(new Number(4), new Add(new Number(8), new Number(1))));
            ParameterCollection parameters = new ParameterCollection();

            var answer = exp.Execute(parameters);

            Assert.Equal(36.0, parameters["x"]);
            Assert.Equal(double.NaN, answer);
        }

        [Fact]
        public void OverrideConstTest()
        {
            IExpression exp = new Define(new Variable("π"), new Number(1));
            ParameterCollection parameters = new ParameterCollection();

            var answer = exp.Execute(parameters);

            Assert.Equal(1.0, parameters["π"]);
        }

    }

}
