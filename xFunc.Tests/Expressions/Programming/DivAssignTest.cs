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
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var div = new DivAssign(Variable.X, new Number(2));
            var result = div.Execute(parameters);
            var expected = 5.0;

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void BoolDivNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var add = new DivAssign(Variable.X, new Number(2));

            Assert.Throws<NotSupportedException>(() => add.Execute(parameters));
        }

        [Fact]
        public void NotVarTest()
        {
            Assert.Throws<NotSupportedException>(() => new DivAssign(new Number(1), new Number(1)));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new DivAssign(Variable.X, new Number(2));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
