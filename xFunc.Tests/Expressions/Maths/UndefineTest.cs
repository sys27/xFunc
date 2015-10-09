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
using Xunit;

namespace xFunc.Test.Expressions.Maths
{

    public class UndefineTest
    {

        [Fact]
        public void UndefVarTest()
        {
            var parameters = new ParameterCollection { { "a", 1 } };

            var undef = new Undefine(new Variable("a"));
            undef.Calculate(parameters);
            Assert.False(parameters.ContainsKey("a"));
        }

        [Fact]
        public void UndefFuncTest()
        {
            var key1 = new UserFunction("f", 0);
            var key2 = new UserFunction("f", 1);

            var functions = new FunctionCollection { { key1, new Number(1) }, { key2, new Number(2) } };

            var undef = new Undefine(key1);
            undef.Calculate(functions);
            Assert.False(functions.ContainsKey(key1));
            Assert.True(functions.ContainsKey(key2));
        }

        [Fact]
        public void UndefConstTest()
        {
            var parameters = new ParameterCollection();

            var undef = new Undefine(new Variable("π"));

            Assert.Throws<ArgumentException>(() => undef.Calculate(parameters));
        }

    }

}
