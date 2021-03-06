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

using xFunc.Maths;
using xFunc.Maths.Expressions;
using Xunit;
using xFunc.Maths.Results;
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Angles;
using System;
using xFunc.Maths.Analyzers.TypeAnalyzers;

namespace xFunc.Tests
{
    public class ProcessorTest
    {
        [Fact]
        public void SimplifierNull()
            => Assert.Throws<ArgumentNullException>(() => new Processor(null, null, null, null));

        [Fact]
        public void DifferentiatorNull()
        {
            var simplifier = new Simplifier();

            Assert.Throws<ArgumentNullException>(() => new Processor(simplifier, null));
        }

        [Fact]
        public void TypeAnalyzerNull()
        {
            var simplifier = new Simplifier();
            var differentiator = new Differentiator();

            Assert.Throws<ArgumentNullException>(() => new Processor(simplifier, differentiator, null, null));
        }

        [Fact]
        public void CtorTest()
        {
            var simplifier = new Simplifier();
            var differentiator = new Differentiator();

            var processor = new Processor(simplifier, differentiator);
        }

        [Fact]
        public void CtorTest2()
        {
            var simplifier = new Simplifier();
            var differentiator = new Differentiator();
            var typeAnalyzer = new TypeAnalyzer();
            var parameters = new ExpressionParameters();

            var processor = new Processor(simplifier, differentiator, typeAnalyzer, parameters);
        }

        [Fact]
        public void SolveDoubleTest()
        {
            var processor = new Processor();
            var result = processor.Solve<NumberResult>("1 + 1.1");

            Assert.Equal(2.1, result.Result);
        }

        [Fact]
        public void SolveComplexTest()
        {
            var processor = new Processor();

            var result = processor.Solve<ComplexNumberResult>("conjugate(2.3 + 1.4i)");
            var expected = Complex.Conjugate(new Complex(2.3, 1.4));

            Assert.Equal(expected, result.Result);
        }

        [Fact]
        public void SolveBoolTest()
        {
            var processor = new Processor();
            var result = processor.Solve<BooleanResult>("true & false");

            Assert.False(result.Result);
        }

        [Fact]
        public void SolveStringTest()
        {
            var processor = new Processor();
            var result = processor.Solve<StringResult>("x := 1");

            Assert.Equal("The value '1' was assigned to the variable 'x'.", result.Result);
        }

        [Fact]
        public void SolveExpTest()
        {
            var processor = new Processor();
            var result = processor.Solve<ExpressionResult>("deriv(x)");

            Assert.Equal(Number.One, result.Result);
        }

        [Fact]
        public void SolveExpDoNotSimplifyTest()
        {
            var processor = new Processor();
            var result = processor.Solve<ExpressionResult>("deriv(x + 1)", false);

            Assert.Equal(Number.One, result.Result);
        }

        [Fact]
        public void SolveAngleTest()
        {
            var processor = new Processor();

            var result = processor.Solve<AngleNumberResult>("90 degree");
            var expected = AngleValue.Degree(90);

            Assert.Equal(expected, result.Result);
        }

        [Fact]
        public void ParseTest()
        {
            var processor = new Processor();

            var result = processor.Parse("x + 1");
            var expected = new Add(Variable.X, Number.One);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimplifyTest()
        {
            var processor = new Processor();

            var exp = new Add(Number.One, Number.One);
            var result = processor.Simplify(exp);

            Assert.Equal(Number.Two, result);
        }

        [Fact]
        public void SimplifyFunctionTest()
        {
            var processor = new Processor();
            var result = processor.Simplify("1 + 1");

            Assert.Equal(Number.Two, result);
        }

        [Fact]
        public void SimplifyNullTest()
        {
            var processor = new Processor();

            Assert.Throws<ArgumentNullException>(() => processor.Simplify(null as IExpression));
        }

        [Fact]
        public void DifferentiateExpTest()
        {
            var processor = new Processor();
            var result = processor.Differentiate(new Add(Variable.X, Number.One));

            Assert.Equal(Number.One, result);
        }

        [Fact]
        public void DifferentiateFunctionTest()
        {
            var processor = new Processor();
            var result = processor.Differentiate("x + 1");

            Assert.Equal(Number.One, result);
        }

        [Fact]
        public void DifferentiateNullExpTest()
        {
            var processor = new Processor();

            Assert.Throws<ArgumentNullException>(() => processor.Differentiate(null as IExpression));
        }

        [Fact]
        public void DifferentiateVarTest()
        {
            var processor = new Processor();

            var y = new Variable("y");
            var result = processor.Differentiate(new Add(y, Number.One), y);

            Assert.Equal(Number.One, result);
        }

        [Fact]
        public void DifferentiateParamsTest()
        {
            var processor = new Processor();

            var y = new Variable("y");
            var result = processor.Differentiate(new Add(y, Number.One), y, new ExpressionParameters());

            Assert.Equal(Number.One, result);
        }
    }
}