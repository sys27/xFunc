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

using xFunc.Maths;
using xFunc.Maths.Expressions;
using Moq;
using Xunit;
using xFunc.Maths.Results;
using System.Numerics;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Analyzers;

namespace xFunc.Tests
{
    public class ProcessorTest
    {
        [Fact]
        public void CtorTest()
        {
            var processor = new Processor();

            Assert.NotNull(processor.Parameters);

            Assert.Equal(NumeralSystem.Decimal, processor.NumeralSystem);
            Assert.Equal(AngleMeasurement.Degree, processor.Parameters.AngleMeasurement);
        }

        [Fact]
        public void SolveDoubleTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "1 + 1.1";

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Solve<NumberResult>(strExp);

            Assert.Equal(2.1, result.Result);
        }

        [Fact]
        public void SolveDoubleHexTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "1 + 1";

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object)
            {
                NumeralSystem = NumeralSystem.Hexidecimal
            };
            var result = processor.Solve<StringResult>(strExp);

            Assert.Equal("0x2", result.Result);
        }

        [Fact]
        public void SolveComplexTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "conjugate(2.3 + 1.4i)";
            var complex = new Complex(2.3, 1.4);

            simplifier.Setup(s => s.Analyze(It.IsAny<Conjugate>())).Returns<Conjugate>(e => e);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Solve<ComplexNumberResult>(strExp);

            Assert.Equal(Complex.Conjugate(complex), result.Result);
        }

        [Fact]
        public void SolveBoolTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "true & false";

            simplifier.Setup(s => s.Analyze(It.IsAny<And>())).Returns<And>(e => e);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Solve<BooleanResult>(strExp);

            Assert.False(result.Result);
        }

        [Fact]
        public void SolveStringTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "x := 1";

            simplifier.Setup(s => s.Analyze(It.IsAny<Define>())).Returns<Define>(e => e);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Solve<StringResult>(strExp);

            Assert.Equal("The value '1' was assigned to the variable 'x'.", result.Result);
        }

        [Fact]
        public void SolveExpTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "deriv(x)";
            var exp = new Derivative(differentiator.Object, simplifier.Object, Variable.X, Variable.X);
            var diff = new Number(1);

            simplifier.Setup(s => s.Analyze(It.IsAny<Number>())).Returns<Number>(e => e);
            simplifier.Setup(s => s.Analyze(It.IsAny<Derivative>())).Returns<Derivative>(e => e);

            differentiator.Setup(d => d.Analyze(It.IsAny<Derivative>())).Returns(() => diff);
            differentiator.SetupProperty(d => d.Variable);
            differentiator.SetupProperty(d => d.Parameters);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Solve<ExpressionResult>(strExp);

            Assert.Equal(new Number(1), result.Result);
        }

        [Fact]
        public void ParseTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Parse("x + 1");

            Assert.Equal(exp, result);
        }

        [Fact]
        public void ParseBoolTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Parse("x + 1");

            Assert.Equal(exp, result);
        }

        [Fact]
        public void SimplifyTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var exp = new Add(Variable.X, new Number(1));

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Simplify(exp);

            simplifier.Verify(s => s.Analyze(It.IsAny<Add>()), Times.Once());

            Assert.Equal(exp, result);
        }

        [Fact]
        public void DiffExpTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));
            var diff = new Number(1);

            differentiator.Setup(d => d.Analyze(exp)).Returns(() => diff);

            var processor = new Processor(
                simplifier.Object,
                differentiator.Object);
            var result = processor.Differentiate(exp);

            differentiator.Verify(d => d.Analyze(exp), Times.Once());

            Assert.Equal(diff, result);
        }

        [Fact]
        public void DiffVarTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));
            var diff = new Number(1);

            differentiator.Setup(d => d.Analyze(exp)).Returns(() => diff);
            differentiator.SetupProperty(d => d.Variable);

            var diffObj = differentiator.Object;
            var processor = new Processor(
                simplifier.Object,
                diffObj);
            var result = processor.Differentiate(exp, Variable.X);

            differentiator.Verify(d => d.Analyze(exp), Times.Once());

            Assert.Equal("x", diffObj.Variable.Name);
            Assert.Equal(diff, result);
        }

        [Fact]
        public void DiffParamsTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));
            var diff = new Number(1);

            differentiator.Setup(d => d.Analyze(exp)).Returns(() => diff);
            differentiator.SetupProperty(d => d.Variable);
            differentiator.SetupProperty(d => d.Parameters);

            var diffObj = differentiator.Object;
            var processor = new Processor(
                simplifier.Object,
                diffObj);
            var result = processor.Differentiate(exp, Variable.X, new ExpressionParameters());

            differentiator.Verify(d => d.Analyze(exp), Times.Once());

            Assert.Equal("x", diffObj.Variable.Name);
            Assert.NotNull(diffObj.Parameters);
            Assert.Equal(diff, result);
        }
    }
}