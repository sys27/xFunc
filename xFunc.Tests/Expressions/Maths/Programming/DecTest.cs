// Copyright 2012-2015 Dmitry Kischenko
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

namespace xFunc.Test.Expressions.Maths.Programming
{

    public class DecTest
    {

        [Fact]
        public void DecCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var dec = new Dec(new Variable("x"));
            var result = (double)dec.Calculate(parameters);

            Assert.Equal(9.0, result);
            Assert.Equal(9.0, parameters["x"]);
        }

        [Fact]
        public void DecBoolTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var dec = new Inc(new Variable("x"));

            Assert.Throws<NotSupportedException>(() => dec.Calculate(parameters));
        }

    }

}
