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
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokenization.Tokens;
using Moq;
using Xunit;
using xFunc.Maths.Results;
using System.Numerics;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Tokenization;

namespace xFunc.Tests
{

    public class ProcessorTest
    {

        [Fact]
        public void CtorTest()
        {
            var processor = new Processor();

            Assert.NotNull(processor.Lexer);
            Assert.NotNull(processor.Parser);
            Assert.NotNull(processor.Differentiator);
            Assert.NotNull(processor.Simplifier);
            Assert.NotNull(processor.Parameters);

            Assert.Equal(NumeralSystem.Decimal, processor.NumeralSystem);
            Assert.Equal(AngleMeasurement.Degree, processor.Parameters.AngleMeasurement);
        }

        [Fact]
        public void SolveDoubleTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();

            var strExp = "1 + 1.1";
            var exp = new Add(new Number(1), new Number(1.1));

            var tokens = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(1.1)
            };
            lexer.Setup(l => l.Tokenize(strExp)).Returns(() => tokens);
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, null);
            var result = processor.Solve<NumberResult>(strExp);

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal(2.1, result.Result);
        }

        [Fact]
        public void SolveDoubleHexTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();

            var strExp = "1 + 1";
            var exp = new Add(new Number(1), new Number(1));

            var tokens = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(1)
            };
            lexer.Setup(l => l.Tokenize(strExp)).Returns(() => tokens);
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, null)
            {
                NumeralSystem = NumeralSystem.Hexidecimal
            };
            var result = processor.Solve<StringResult>(strExp);

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal("0x2", result.Result);
        }

        [Fact]
        public void SolveComplexTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();

            var strExp = "conjugate(2.3 + 1.4i)";
            var complex = new Complex(2.3, 1.4);
            var exp = new Conjugate(new ComplexNumber(complex));

            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Conjugate, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2.3),
                new OperationToken(Operations.Addition),
                new NumberToken(1.4),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };
            lexer.Setup(l => l.Tokenize(strExp)).Returns(() => tokens);
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<Conjugate>())).Returns<Conjugate>(e => e);

            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, null);
            var result = processor.Solve<ComplexNumberResult>(strExp);

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal(Complex.Conjugate(complex), result.Result);
        }

        [Fact]
        public void SolveBoolTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();

            var strExp = "true & false";
            var exp = new And(new Bool(true), new Bool(false));

            var tokens = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.And),
                new BooleanToken(false)
            };
            lexer.Setup(l => l.Tokenize(strExp)).Returns(() => tokens);
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<And>())).Returns<And>(e => e);

            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, null);
            var result = processor.Solve<BooleanResult>(strExp);

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.False(result.Result);
        }

        [Fact]
        public void SolveStringTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();

            var strExp = "x := 1";
            var exp = new Define(Variable.X, new Number(1));

            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Define, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };
            lexer.Setup(l => l.Tokenize(strExp)).Returns(() => tokens);
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<Define>())).Returns<Define>(e => e);

            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, null);
            var result = processor.Solve<StringResult>(strExp);

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal("The value '1' was assigned to the variable 'x'.", result.Result);
        }

        [Fact]
        public void SolveExpTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            var strExp = "deriv(x)";
            var exp = new Derivative(Variable.X, Variable.X);
            var diff = new Number(1);

            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Derivative, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };
            lexer.Setup(l => l.Tokenize(strExp)).Returns(() => tokens);
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<Number>())).Returns<Number>(e => e);
            simplifier.Setup(s => s.Analyze(It.IsAny<Derivative>())).Returns<Derivative>(e => e);

            differentiator.Setup(d => d.Analyze(It.IsAny<Derivative>())).Returns(() => diff);
            differentiator.SetupProperty(d => d.Variable);
            differentiator.SetupProperty(d => d.Parameters);

            exp.Differentiator = differentiator.Object;
            exp.Simplifier = simplifier.Object;
            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, differentiator.Object);
            var result = processor.Solve<ExpressionResult>(strExp);

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal(new Number(1), result.Result);
        }

        [Fact]
        public void ParseTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();
            var simplifier = new Mock<ISimplifier>();

            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(1)
            };
            lexer.Setup(l => l.Tokenize("x + 1")).Returns(() => tokens);

            var exp = new Add(Variable.X, new Number(1));
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var processor = new Processor(lexer.Object, parser.Object, simplifier.Object, null);
            var result = processor.Parse("x + 1");

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal(exp, result);
        }

        [Fact]
        public void ParseBoolTest()
        {
            var lexer = new Mock<ILexer>();
            var parser = new Mock<IParser>();

            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(1)
            };
            lexer.Setup(l => l.Tokenize("x + 1")).Returns(() => tokens);

            var exp = new Add(Variable.X, new Number(1));
            parser.Setup(p => p.Parse(tokens)).Returns(() => exp);

            var processor = new Processor(lexer.Object, parser.Object, null, null)
            {
                DoSimplify = false
            };
            var result = processor.Parse("x + 1");

            lexer.Verify(l => l.Tokenize(It.IsAny<string>()), Times.Once());
            parser.Verify(p => p.Parse(It.IsAny<IEnumerable<IToken>>()), Times.Once());

            Assert.Equal(exp, result);
        }

        [Fact]
        public void SimplifyTest()
        {
            var simplifier = new Mock<ISimplifier>();
            var differentiator = new Mock<IDifferentiator>();

            simplifier.Setup(s => s.Analyze(It.IsAny<Add>())).Returns<Add>(e => e);

            var exp = new Add(Variable.X, new Number(1));

            var processor = new Processor(null, null, simplifier.Object, null);
            var result = processor.Simplify(exp);

            simplifier.Verify(s => s.Analyze(It.IsAny<Add>()), Times.Once());

            Assert.Equal(exp, result);
        }

        [Fact]
        public void DiffExpTest()
        {
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));
            var diff = new Number(1);

            differentiator.Setup(d => d.Analyze(exp)).Returns(() => diff);

            var processor = new Processor(null, null, null, differentiator.Object);
            var result = processor.Differentiate(exp);

            differentiator.Verify(d => d.Analyze(exp), Times.Once());

            Assert.Equal(diff, result);
        }

        [Fact]
        public void DiffVarTest()
        {
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));
            var diff = new Number(1);

            differentiator.Setup(d => d.Analyze(exp)).Returns(() => diff);
            differentiator.SetupProperty(d => d.Variable);

            var diffObj = differentiator.Object;
            var processor = new Processor(null, null, null, diffObj);
            var result = processor.Differentiate(exp, Variable.X);

            differentiator.Verify(d => d.Analyze(exp), Times.Once());

            Assert.Equal("x", diffObj.Variable.Name);
            Assert.Equal(diff, result);
        }

        [Fact]
        public void DiffParamsTest()
        {
            var differentiator = new Mock<IDifferentiator>();

            var exp = new Add(Variable.X, new Number(1));
            var diff = new Number(1);

            differentiator.Setup(d => d.Analyze(exp)).Returns(() => diff);
            differentiator.SetupProperty(d => d.Variable);
            differentiator.SetupProperty(d => d.Parameters);

            var diffObj = differentiator.Object;
            var processor = new Processor(null, null, null, diffObj);
            var result = processor.Differentiate(exp, Variable.X, new ExpressionParameters());

            differentiator.Verify(d => d.Analyze(exp), Times.Once());

            Assert.Equal("x", diffObj.Variable.Name);
            Assert.NotNull(diffObj.Parameters);
            Assert.Equal(diff, result);
        }

    }

}
