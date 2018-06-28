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

    public class WhileTest
    {

        [Fact]
        public void CalculateWhileTest()
        {
            var parameters = new ExpressionParameters();
            parameters.Variables.Add(new Parameter("x", 0));

            var body = new Define(Variable.X, new Add(Variable.X, new Number(2)));
            var cond = new LessThan(Variable.X, new Number(10));

            var @while = new While(body, cond);
            @while.Execute(parameters);

            Assert.Equal(10.0, parameters.Variables["x"]);
        }

        [Fact]
        public void CloneTest()
        {
            var body = new Define(Variable.X, new Add(Variable.X, new Number(2)));
            var cond = new LessThan(Variable.X, new Number(10));

            var exp = new While(body, cond);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
