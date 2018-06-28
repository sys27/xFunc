// Copyright 2012-2018 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{

    public class IfTest
    {

        [Fact]
        public void CalculateIfElseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };

            var cond = new Equal(Variable.X, new Number(10));
            var @if = new If(cond, new Number(20), new Number(0));

            Assert.Equal(20.0, @if.Execute(parameters));

            parameters["x"] = 0;

            Assert.Equal(0.0, @if.Execute(parameters));
        }

        [Fact]
        public void CalculateIfElseNegativeNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };

            var cond = new Equal(Variable.X, new Number(0));
            var @if = new If(cond, new Number(1), new UnaryMinus(new Number(1)));

            Assert.Equal(1.0, @if.Execute(parameters));

            parameters["x"] = 10;

            Assert.Equal(-1.0, @if.Execute(parameters));
        }

        [Fact]
        public void CalculateIfTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };

            var cond = new Equal(Variable.X, new Number(10));
            var @if = new If(cond, new Number(20));

            Assert.Equal(20.0, @if.Execute(parameters));
        }

        [Fact]
        public void CalculateElseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };

            var cond = new Equal(Variable.X, new Number(10));
            var @if = new If(cond, new Number(20));

            Assert.Equal(0.0, @if.Execute(parameters));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new If(new Equal(Variable.X, new Number(10)), new Number(3), new Number(2));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
