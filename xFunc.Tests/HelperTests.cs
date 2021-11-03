// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests
{
    public class HelperTests
    {
        [Fact]
        public void HasVarTest1()
        {
            var exp = new Sin(new Mul(Number.Two, Variable.X));
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.True(expected);
        }

        [Fact]
        public void HasVarTest2()
        {
            var exp = new Sin(new Mul(Number.Two, new Number(3)));
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.False(expected);
        }

        [Fact]
        public void HasVarDiffTest1()
        {
            var exp = new GCD(new IExpression[] { Variable.X, Number.Two, new Number(4) });
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.True(expected);
        }

        [Fact]
        public void HasVarDiffTest2()
        {
            var exp = new GCD(new IExpression[] { Variable.Y, Number.Two, new Number(4) });
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.False(expected);
        }

        [Fact]
        public void GetLogicParametersTest()
        {
            var function = "a | b & c & (a | c)";
            var exp = new Parser().Parse(function);

            var expected = new ParameterCollection(false)
             {
                 new Parameter("a", false),
                 new Parameter("b", false),
                 new Parameter("c", false)
             };

            var actual = Helpers.GetParameters(exp);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetLogicParametersNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Helpers.GetParameters(null));
        }

        [Fact]
        public void ConvertLogicExpressionToCollectionTest()
        {
            var exp = new Implication(
                new Or(new Variable("a"), new Variable("b")),
                new Not(new Variable("c"))
            );
            var actual = new List<IExpression>(Helpers.ConvertExpressionToCollection(exp));

            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void ConvertLogicExpressionToCollectionNullTest()
            => Assert.Throws<ArgumentNullException>(() => Helpers.ConvertExpressionToCollection(null));
    }
}