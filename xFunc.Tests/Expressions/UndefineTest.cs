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
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class UndefineTest
    {

        [Fact]
        public void ExecuteFailTest()
        {
            Assert.Throws<NotSupportedException>(() => new Undefine(Variable.X).Execute());
        }

        [Fact]
        public void ExecuteParamNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Undefine(Variable.X).Execute(null));
        }

        [Fact]
        public void KeyNotSupportedTest()
        {
            Assert.Throws<NotSupportedException>(() => new Undefine(Variable.X).Key = new Number(1));
        }

        [Fact]
        public void KeyNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Undefine(Variable.X).Key = null);
        }

        [Fact]
        public void EqualRefTest()
        {
            var exp = new Undefine(Variable.X);

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualDiffTypesTest()
        {
            var exp1 = new Undefine(Variable.X);
            var exp2 = new Number(2);

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void EqualTest()
        {
            var exp1 = new Undefine(Variable.X);
            var exp2 = new Undefine(Variable.X);

            Assert.True(exp1.Equals(exp2));
        }

        [Fact]
        public void Equal2Test()
        {
            var exp1 = new Undefine(Variable.X);
            var exp2 = new Undefine(new Variable("y"));

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void UndefVarTest()
        {
            var parameters = new ParameterCollection { { "a", 1 } };

            var undef = new Undefine(new Variable("a"));
            undef.Execute(parameters);
            Assert.False(parameters.ContainsKey("a"));
        }

        [Fact]
        public void UndefFuncTest()
        {
            var key1 = new UserFunction("f", new IExpression[0], 0);
            var key2 = new UserFunction("f", 1);

            var functions = new FunctionCollection { { key1, new Number(1) }, { key2, new Number(2) } };

            var undef = new Undefine(key1);
            var result = undef.Execute(functions);

            Assert.False(functions.ContainsKey(key1));
            Assert.True(functions.ContainsKey(key2));
            Assert.Equal("The 'f()' function is removed.", result);
        }

        [Fact]
        public void UndefFuncWithParamsTest()
        {
            var key1 = new UserFunction("f", new IExpression[0], 0);
            var key2 = new UserFunction("f", 1);

            var functions = new FunctionCollection { { key1, new Number(1) }, { key2, new Number(2) } };

            var undef = new Undefine(key2);
            var result = undef.Execute(functions);

            Assert.True(functions.ContainsKey(key1));
            Assert.False(functions.ContainsKey(key2));
            Assert.Equal("The 'f(x1)' function is removed.", result);
        }

        [Fact]
        public void UndefConstTest()
        {
            var parameters = new ParameterCollection();

            var undef = new Undefine(new Variable("π"));

            Assert.Throws<ArgumentException>(() => undef.Execute(parameters));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Undefine(Variable.X);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
