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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class TrigonometricSimplifierTest : BaseSimplifierTest
    {
        [Theory]
        [InlineData(typeof(Arcsin), typeof(Sin))]
        [InlineData(typeof(Arccos), typeof(Cos))]
        [InlineData(typeof(Arctan), typeof(Tan))]
        [InlineData(typeof(Arccot), typeof(Cot))]
        [InlineData(typeof(Arcsec), typeof(Sec))]
        [InlineData(typeof(Arccsc), typeof(Csc))]
        [InlineData(typeof(Sin), typeof(Arcsin))]
        [InlineData(typeof(Cos), typeof(Arccos))]
        [InlineData(typeof(Tan), typeof(Arctan))]
        [InlineData(typeof(Cot), typeof(Arccot))]
        [InlineData(typeof(Sec), typeof(Arcsec))]
        [InlineData(typeof(Csc), typeof(Arccsc))]
        [InlineData(typeof(Arsinh), typeof(Sinh))]
        [InlineData(typeof(Arcosh), typeof(Cosh))]
        [InlineData(typeof(Artanh), typeof(Tanh))]
        [InlineData(typeof(Arcoth), typeof(Coth))]
        [InlineData(typeof(Arsech), typeof(Sech))]
        [InlineData(typeof(Arcsch), typeof(Csch))]
        [InlineData(typeof(Sinh), typeof(Arsinh))]
        [InlineData(typeof(Cosh), typeof(Arcosh))]
        [InlineData(typeof(Tanh), typeof(Artanh))]
        [InlineData(typeof(Coth), typeof(Arcoth))]
        [InlineData(typeof(Sech), typeof(Arsech))]
        [InlineData(typeof(Csch), typeof(Arcsch))]
        public void ReserseFunctionsTest(Type outer, Type inner)
        {
            var innerFunction = Create(inner, Variable.X);
            var outerFunction = Create(outer, innerFunction);
            var expected = Variable.X;

            SimplifyTest(outerFunction, expected);
        }

        [Theory]
        [InlineData(typeof(Arcsin))]
        [InlineData(typeof(Arccos))]
        [InlineData(typeof(Arctan))]
        [InlineData(typeof(Arccot))]
        [InlineData(typeof(Arcsec))]
        [InlineData(typeof(Arccsc))]
        [InlineData(typeof(Sin))]
        [InlineData(typeof(Cos))]
        [InlineData(typeof(Tan))]
        [InlineData(typeof(Cot))]
        [InlineData(typeof(Sec))]
        [InlineData(typeof(Csc))]
        public void SimplifyArgumentTest(Type type)
        {
            var exp = Create(type, new Add(Number.One, Number.Two));
            var expected = Create(type, new Number(3));

            SimplifyTest(exp, expected);
        }
    }
}