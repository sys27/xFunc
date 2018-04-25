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
using System.Numerics;
using xFunc.Maths;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests
{

    public class MathParserTest
    {

        private Parser parser;

        public MathParserTest()
        {
            parser = new Parser();
        }

        [Fact]
        public void HasVarTest1()
        {
            var exp = new Sin(new Mul(new Number(2), Variable.X));
            bool expected = Helpers.HasVariable(exp, Variable.X);

            Assert.True(expected);
        }

        [Fact]
        public void HasVarTest2()
        {
            var exp = new Sin(new Mul(new Number(2), new Number(3)));
            bool expected = Helpers.HasVariable(exp, Variable.X);

            Assert.False(expected);
        }

        [Fact]
        public void HasVarDiffTest1()
        {
            var exp = new GCD(new IExpression[] { Variable.X, new Number(2), new Number(4) }, 3);
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.True(expected);
        }

        [Fact]
        public void HasVarDiffTest2()
        {
            var exp = new GCD(new IExpression[] { new Variable("y"), new Number(2), new Number(4) }, 3);
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.False(expected);
        }

        [Fact]
        public void ParseNull()
        {
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Fact]
        public void ParseEmptyTokens()
        {
            Assert.Throws<ArgumentException>(() => parser.Parse(new List<IToken>()));
        }

        [Fact]
        public void ParseLog()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Log, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(9),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Log(new Number(3), new Number(9));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseLogWithOneParam()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Log, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseRoot()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Root, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Root(Variable.X, new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseRootWithOneParam()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Root, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseDerivWithOneParam()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Derivative, 1),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Derivative(diff, simp, new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseDefine()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            var exp = parser.Parse(tokens);
            var expected = new Define(Variable.X, new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseDefineWithOneParam()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseDefineFirstParamIsNotVar()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(5),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            Assert.Throws<NotSupportedException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ErrorWhileParsingTree()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new NumberToken(2)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void DefineComplexParserTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("aaa"),
                new OperationToken(Operations.Assign),
                new ComplexNumberToken(new Complex(3, 2))
            };

            var exp = parser.Parse(tokens);
            var expected = new Define(new Variable("aaa"), new ComplexNumber(new Complex(3, 2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DefineUserFuncTest()
        {
            var tokens = new List<IToken>
            {
                new UserFunctionToken("func", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new OperationToken(Operations.Assign),
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Define(new UserFunction("func", new[] { Variable.X }, 1), new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UserFunc()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.Addition),
                new UserFunctionToken("func", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(1), new UserFunction("func", new[] { Variable.X }, 1));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UndefParseTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Undefine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new UserFunctionToken("f", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Undefine(new UserFunction("f", new[] { Variable.X }, 1));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UndefWithoutParamsTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Undefine, 0),
                new SymbolToken(Symbols.OpenBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParserTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Cosine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new OperationToken(Operations.Addition),
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Add(new Cos(Variable.X), new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GCDTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.GCD, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new GCD(new Number(12), new Number(16));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GCDOfThreeTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.GCD, 3),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.Comma),
                new NumberToken(8),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new GCD(new[] { new Number(12), new Number(16), new Number(8) }, 3);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LCMTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.LCM, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new LCM(new Number(12), new Number(16));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SimplifyTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Simplify, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Simplify(simp, Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactorialTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Factorial, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Fact(new Number(4));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SumToTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sum, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Sum(new IExpression[] { Variable.X, new Number(20) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ProductToTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Product, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Product(new IExpression[] { Variable.X, new Number(20) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VectorTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Vector, 3),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.Comma),
                new NumberToken(4),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Matrices.Vector(new[] { new Number(2), new Number(3), new Number(4) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VectorTwoDimTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new [] { new Number(2), new Number(3) }),
                new Maths.Expressions.Matrices.Vector(new [] { new Number(4), new Number(7) })
            });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MatrixWithDiffVectorSizeTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 3),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<MatrixIsInvalidException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void TooMuchParamsTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sine, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ForTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.For, 4),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(0),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.LessThan),
                new NumberToken(10),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new For(
                new Number(2),
                new Define(Variable.X, new Number(0)),
                new LessThan(Variable.X, new Number(10)),
                new Define(Variable.X, new Add(Variable.X, new Number(1))));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void WhileTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.While, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(1),
                new OperationToken(Operations.Equal),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new While(
                new Define(Variable.X, new Add(Variable.X, new Number(1))),
                new Equal(new Number(1), new Number(1)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IfTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.If, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
                new OperationToken(Operations.ConditionalAnd),
                new VariableToken("y"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(8),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new If(
                new Maths.Expressions.Programming.And(
                    new Equal(Variable.X, new Number(0)),
                    new NotEqual(new Variable("y"), new Number(0))),
                new Number(2),
                new Number(8));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConditionalAndTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
                new OperationToken(Operations.ConditionalAnd),
                new VariableToken("y"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0)
            };

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Programming.And(
                new Equal(Variable.X, new Number(0)),
                new NotEqual(new Variable("y"), new Number(0)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
                new OperationToken(Operations.ConditionalOr),
                new VariableToken("y"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0)
            };

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Programming.Or(
                new Equal(Variable.X, new Number(0)),
                new NotEqual(new Variable("y"), new Number(0)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void EqualTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
            };

            var exp = parser.Parse(tokens);
            var expected = new Equal(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotEqualTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse(tokens);
            var expected = new NotEqual(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LessThenTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessThan),
                new NumberToken(0),
            };

            var exp = parser.Parse(tokens);
            var expected = new LessThan(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessOrEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse(tokens);
            var expected = new LessOrEqual(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GreaterThenTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterThan),
                new NumberToken(0),
            };

            var exp = parser.Parse(tokens);
            var expected = new GreaterThan(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterOrEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse(tokens);
            var expected = new GreaterOrEqual(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IncTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Increment)
            };

            var exp = parser.Parse(tokens);
            var expected = new Inc(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IncForTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.For, 4),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(0),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.LessThan),
                new NumberToken(10),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.Increment),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new For(
                new Number(2),
                new Define(Variable.X, new Number(0)),
                new LessThan(Variable.X, new Number(10)),
                new Inc(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DecTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Decrement)
            };

            var exp = parser.Parse(tokens);
            var expected = new Dec(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AddAssing()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.AddAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new AddAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MulAssing()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.MulAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new MulAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubAssing()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.SubAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new SubAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivAssing()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.DivAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new DivAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void BoolConstTest()
        {
            var tokens = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.And),
                new BooleanToken(false)
            };

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.And(new Bool(true), new Bool(false));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LogicAddPriorityTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(3),
                new OperationToken(Operations.GreaterThan),
                new NumberToken(4),
                new OperationToken(Operations.And),
                new NumberToken(1),
                new OperationToken(Operations.LessThan),
                new NumberToken(3),
            };

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.And(
                new GreaterThan(new Number(3), new Number(4)),
                new LessThan(new Number(1), new Number(3)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GetLogicParametersTest()
        {
            //string function = "a | b & c & (a | c)";
            var tokens = new List<IToken>
            {
                new VariableToken("a"),
                new OperationToken(Operations.Or),
                new VariableToken("b"),
                new OperationToken(Operations.And),
                new VariableToken("c"),
                new OperationToken(Operations.And),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("a"),
                new OperationToken(Operations.Or),
                new VariableToken("c"),
                new SymbolToken(Symbols.CloseBracket)
            };
            var expected = new ParameterCollection(false)
            {
                new Parameter("a", false),
                new Parameter("b", false),
                new Parameter("c", false)
            };

            var actual = Helpers.GetParameters(tokens);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertLogicExpressionToColletionTest()
        {
            var exp = new Implication(new Maths.Expressions.LogicalAndBitwise.Or(new Variable("a"), new Variable("b")), new Not(new Variable("c")));
            var actual = new List<IExpression>(Helpers.ConvertExpressionToCollection(exp));

            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void ConvertLogicExpressionToColletionNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Helpers.ConvertExpressionToCollection(null));
        }

        [Fact]
        public void ComplexNumberTest()
        {
            var tokens = new List<IToken>
            {
                new ComplexNumberToken(new Complex(3, 2))
            };
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(3, 2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeTest()
        {
            var tokens = new List<IToken>
            {
                new ComplexNumberToken(new Complex(3, -2))
            };
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(3, -2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsTest()
        {
            var tokens = new List<IToken>
            {
                new ComplexNumberToken(new Complex(-3, -2))
            };
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(-3, -2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyRePartTest()
        {
            var tokens = new List<IToken>
            {
                new ComplexNumberToken(new Complex(3, 0))
            };
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(3, 0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyImPartTest()
        {
            var tokens = new List<IToken>
            {
                new ComplexNumberToken(new Complex(0, 2))
            };
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(0, 2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyImPartNegativeTest()
        {
            var tokens = new List<IToken>
            {
                new ComplexNumberToken(new Complex(0, -2))
            };
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(0, -2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexWithVarTest1()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Subtraction),
                new ComplexNumberToken(new Complex(0, 2))
            };
            var exp = parser.Parse(tokens);
            var expected = new Sub(Variable.X, new ComplexNumber(new Complex(0, 2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexWithVarTest2()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new ComplexNumberToken(new Complex(3, -2))
            };
            var exp = parser.Parse(tokens);
            var expected = new Add(Variable.X, new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ImTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Im, 1),
                new SymbolToken(Symbols.OpenBracket),
                new ComplexNumberToken(new Complex(3, -2)),
                new SymbolToken(Symbols.CloseBracket)
            };
            var exp = parser.Parse(tokens);
            var expected = new Im(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Re, 1),
                new SymbolToken(Symbols.OpenBracket),
                new ComplexNumberToken(new Complex(3, -2)),
                new SymbolToken(Symbols.CloseBracket)
            };
            var exp = parser.Parse(tokens);
            var expected = new Re(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Phase, 1),
                new SymbolToken(Symbols.OpenBracket),
                new ComplexNumberToken(new Complex(3, -2)),
                new SymbolToken(Symbols.CloseBracket)
            };
            var exp = parser.Parse(tokens);
            var expected = new Phase(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Conjugate, 1),
                new SymbolToken(Symbols.OpenBracket),
                new ComplexNumberToken(new Complex(3, -2)),
                new SymbolToken(Symbols.CloseBracket)
            };
            var exp = parser.Parse(tokens);
            var expected = new Conjugate(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Reciprocal, 1),
                new SymbolToken(Symbols.OpenBracket),
                new ComplexNumberToken(new Complex(3, -2)),
                new SymbolToken(Symbols.CloseBracket)
            };
            var exp = parser.Parse(tokens);
            var expected = new Reciprocal(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(7),
                new OperationToken(Operations.Modulo),
                new NumberToken(2)
            };
            var exp = parser.Parse(tokens);
            var expected = new Mod(new Number(7), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloAsFuncTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(7),
                new OperationToken(Operations.Modulo),
                new NumberToken(2)
            };
            var exp = parser.Parse(tokens);
            var expected = new Mod(new Number(7), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloAddTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(7),
                new OperationToken(Operations.Modulo),
                new NumberToken(2)
            };
            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(2), new Mod(new Number(7), new Number(2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MinTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Min, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Min(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MaxTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Max, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Max(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AvgTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Avg, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Avg(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CountTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Count, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Count(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VarTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Var, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Var(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VarpTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Varp, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Varp(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void StdevTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Stdev, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Stdev(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void StdevpTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Stdevp, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Stdevp(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DelTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Del, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(3),
                new OperationToken(Operations.Multiplication),
                new VariableToken("y"),
                new OperationToken(Operations.Addition),
                new NumberToken(4),
                new OperationToken(Operations.Multiplication),
                new VariableToken("z"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Del(diff, simp, new Add(new Add(new Mul(new Number(2), Variable.X), new Mul(new Number(3), new Variable("y"))), new Mul(new Number(4), new Variable("z"))));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AddTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Add, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sub, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Sub(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MulTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Mul, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Mul(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivFuncTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Div, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Div(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowFuncTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Pow, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.Division),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new Div(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Factorial)
            };

            var exp = parser.Parse(tokens);
            var expected = new Fact(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var tokens = new List<IToken>
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new UnaryMinus(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotTest()
        {
            var tokens = new List<IToken>
            {
                new OperationToken(Operations.Not),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new Not(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void OrTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.Or),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.Or(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void XOrTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.XOr),
                new NumberToken(2)
            };

            var exp = parser.Parse(tokens);
            var expected = new XOr(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NOrTest()
        {
            var tokens = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.NOr),
                new BooleanToken(true)
            };

            var exp = parser.Parse(tokens);
            var expected = new NOr(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NAndTest()
        {
            var tokens = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.NAnd),
                new BooleanToken(true)
            };

            var exp = parser.Parse(tokens);
            var expected = new NAnd(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ImplicationTest()
        {
            var tokens = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.Implication),
                new BooleanToken(true)
            };

            var exp = parser.Parse(tokens);
            var expected = new Implication(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void EqualityTest()
        {
            var tokens = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.Equality),
                new BooleanToken(true)
            };

            var exp = parser.Parse(tokens);
            var expected = new Equality(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AbsTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Absolute, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Abs(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Tangent, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Tan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CotTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Cotangent, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Cot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SecTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Secant, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Sec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CscTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Cosecant, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Csc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LnTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Ln, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Ln(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LgTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Lg, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Lg(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LbTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Lb, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Lb(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ExpTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Exp, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Exp(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FloorTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Floor, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Floor(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CeilTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Ceil, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Ceil(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void RoundTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Round, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Round(new Number(2), new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DefTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Define, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Define(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SqrtTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sqrt, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Sqrt(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsinTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arcsine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arcsin(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccosTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arccosine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arccos(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArctanTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arctangent, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arctan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccotTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arccotangent, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arccot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsecTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arcsecant, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arcsec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccscTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arccosecant, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arccsc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SinhTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sineh, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Sinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CoshTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Cosineh, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Cosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanhTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Tangenth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Tanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CothTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Cotangenth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Coth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SechTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Secanth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Sech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CschTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Cosecanth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Csch(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsinhTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arsineh, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arsinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcoshTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arcosineh, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arcosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArtanhTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Artangenth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Artanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcothTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arcotangenth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arcoth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsechTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arsecanth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arsech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcschTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Arcosecanth, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            var expected = new Arcsch(new Number(2));

            Assert.Equal(expected, exp);
        }

    }

}
