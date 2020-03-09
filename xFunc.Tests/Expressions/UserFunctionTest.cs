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
using System.Collections.Generic;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class UserFunctionTest
    {
        [Fact]
        public void ExecuteTest1()
        {
            var functions = new FunctionCollection
            {
                { new UserFunction("f", new IExpression[] { Variable.X }), new Ln(Variable.X) }
            };

            var func = new UserFunction("f", new IExpression[] { new Number(1) });
            Assert.Equal(Math.Log(1), func.Execute(functions));
        }

        [Fact]
        public void ExecuteTest2()
        {
            var functions = new FunctionCollection();

            var func = new UserFunction("f", new IExpression[] { new Number(1) });

            Assert.Throws<KeyNotFoundException>(() => func.Execute(functions));
        }

        [Fact]
        public void ExecuteRecursiveTest()
        {
            var expParams = new ExpressionParameters();

            var exp = new If(new Equal(Variable.X, new Number(0)),
                new Number(1),
                new Mul(Variable.X, new UserFunction("f", new[] { new Sub(Variable.X, new Number(1)) })));
            expParams.Functions.Add(new UserFunction("f", new[] { Variable.X }), exp);

            var func = new UserFunction("f", new[] { new Number(4) });

            Assert.Equal(24.0, func.Execute(expParams));
        }

        [Fact]
        public void ExecuteNullTest()
        {
            var exp = new UserFunction("f", new IExpression[0]);

            Assert.Throws<ArgumentNullException>(() => exp.Execute());
        }

        [Fact]
        public void ArgumentsAreNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new UserFunction("f", null));
        }

        [Fact]
        public void SetNullTest()
        {
            var exp = new UserFunction("f", new[] { new Number(5) });

            Assert.Throws<ArgumentNullException>(() => exp[0] = null);
        }

        [Fact]
        public void EqualDiffNameTest()
        {
            var exp1 = new UserFunction("f", new[] { new Number(5) });
            var exp2 = new UserFunction("f2", new[] { new Number(5) });

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void EqualDiffCountTest()
        {
            var exp1 = new UserFunction("f", new[] { new Number(5) });
            var exp2 = new UserFunction("f", new[] { new Number(5), new Number(2) });

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void EqualDiffTypeTest()
        {
            var exp1 = new UserFunction("f", new[] { new Number(5) });

            Assert.False(exp1.Equals(Variable.X));
        }

        [Fact]
        public void ComplexNumberAnalyzeNull()
        {
            var exp = new UserFunction("f", new[] { new Number(5) });

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new UserFunction("f", new[] { new Number(5) });
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}