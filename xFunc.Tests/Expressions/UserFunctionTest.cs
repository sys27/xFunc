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
using System.Collections.Generic;
using xFunc.Maths.Expressions.Collections;
using Xunit;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Tests.Expressions
{

    public class UserFunctionTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var functions = new FunctionCollection();
            functions.Add(new UserFunction("f", new IExpression[] { Variable.X }, 1), new Ln(Variable.X));

            var func = new UserFunction("f", new IExpression[] { new Number(1) }, 1);
            Assert.Equal(Math.Log(1), func.Execute(functions));
        }

        [Fact]
        public void ExecuteTest2()
        {
            var functions = new FunctionCollection();

            var func = new UserFunction("f", new IExpression[] { new Number(1) }, 1);

            Assert.Throws<KeyNotFoundException>(() => func.Execute(functions));
        }

        [Fact]
        public void ExecuteRecursiveTest()
        {
            var expParams = new ExpressionParameters();

            var exp = new If(new Equal(Variable.X, new Number(0)),
                             new Number(1),
                             new Mul(Variable.X, new UserFunction("f", new[] { new Sub(Variable.X, new Number(1)) }, 1)));
            expParams.Functions.Add(new UserFunction("f", new[] { Variable.X }, 1), exp);

            var func = new UserFunction("f", new[] { new Number(4) }, 1);

            Assert.Equal(24.0, func.Execute(expParams));
        }

        [Fact]
        public void ExecuteNullTest()
        {
            var exp = new UserFunction("f", 0);

            Assert.Throws<ArgumentNullException>(() => exp.Execute(null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new UserFunction("f", new[] { new Number(5) }, 1);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
