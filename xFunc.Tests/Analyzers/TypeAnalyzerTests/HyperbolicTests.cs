// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class HyperbolicTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(Arcosh))]
        [InlineData(typeof(Arcoth))]
        [InlineData(typeof(Arcsch))]
        [InlineData(typeof(Arsech))]
        [InlineData(typeof(Arsinh))]
        [InlineData(typeof(Artanh))]
        [InlineData(typeof(Cosh))]
        [InlineData(typeof(Coth))]
        [InlineData(typeof(Csch))]
        [InlineData(typeof(Sech))]
        [InlineData(typeof(Sinh))]
        [InlineData(typeof(Tanh))]
        public void TestUndefined(Type type)
        {
            var exp = Create(type, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Theory]
        [InlineData(typeof(Arcosh))]
        [InlineData(typeof(Arcoth))]
        [InlineData(typeof(Arcsch))]
        [InlineData(typeof(Arsech))]
        [InlineData(typeof(Arsinh))]
        [InlineData(typeof(Artanh))]
        public void TestAngleNumber(Type type)
        {
            var exp = Create(type, Number.Two);

            Test(exp, ResultTypes.AngleNumber);
        }

        [Theory]
        [InlineData(typeof(Cosh))]
        [InlineData(typeof(Coth))]
        [InlineData(typeof(Csch))]
        [InlineData(typeof(Sech))]
        [InlineData(typeof(Sinh))]
        [InlineData(typeof(Tanh))]
        public void TestNumber(Type type)
        {
            var exp = Create(type, Number.Two);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(Arcosh))]
        [InlineData(typeof(Arcoth))]
        [InlineData(typeof(Arcsch))]
        [InlineData(typeof(Arsech))]
        [InlineData(typeof(Arsinh))]
        [InlineData(typeof(Artanh))]
        [InlineData(typeof(Cosh))]
        [InlineData(typeof(Coth))]
        [InlineData(typeof(Csch))]
        [InlineData(typeof(Sech))]
        [InlineData(typeof(Sinh))]
        [InlineData(typeof(Tanh))]
        public void TestComplexNumber(Type type)
        {
            var exp = Create(type, new ComplexNumber(2, 2));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Theory]
        [InlineData(typeof(Arcosh))]
        [InlineData(typeof(Arcoth))]
        [InlineData(typeof(Arcsch))]
        [InlineData(typeof(Arsech))]
        [InlineData(typeof(Arsinh))]
        [InlineData(typeof(Artanh))]
        [InlineData(typeof(Cosh))]
        [InlineData(typeof(Coth))]
        [InlineData(typeof(Csch))]
        [InlineData(typeof(Sech))]
        [InlineData(typeof(Sinh))]
        [InlineData(typeof(Tanh))]
        public void TestParameterException(Type type)
        {
            var exp = Create(type, Bool.False);

            TestException(exp);
        }
    }
}