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
using xFunc.Maths.Tokenization;
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

        private TokensBuilder Builder()
        {
            return new TokensBuilder();
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
            var tokens = Builder()
                .Function(Functions.Log, 2)
                .OpenBracket()
                .Number(9)
                .Comma()
                .Number(3)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Log(new Number(3), new Number(9));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseLogWithOneParam()
        {
            var tokens = Builder()
                .Function(Functions.Log, 1)
                .OpenBracket()
                .Number(9)
                .CloseBracket()
                .Tokens;

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseRoot()
        {
            var tokens = Builder()
                .Function(Functions.Root, 2)
                .OpenBracket()
                .VariableX()
                .Comma()
                .Number(3)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Root(Variable.X, new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseRootWithOneParam()
        {
            var tokens = Builder()
                .Function(Functions.Root, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Tokens;

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseDerivWithOneParam()
        {
            var tokens = Builder()
                .Function(Functions.Derivative, 1)
                .OpenBracket()
                .Function(Functions.Sine, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .CloseBracket()
                .Tokens;

            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Derivative(diff, simp, new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseDefine()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Assign)
                .Number(3)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(Variable.X, new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseDefineWithOneParam()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Assign)
                .Tokens;

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseDefineFirstParamIsNotVar()
        {
            var tokens = Builder()
                .Number(5)
                .Operation(Operations.Assign)
                .Number(3)
                .Tokens;

            Assert.Throws<NotSupportedException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ErrorWhileParsingTree()
        {
            var tokens = Builder()
                .Function(Functions.Sine, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Number(2)
                .Tokens;

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void DefineComplexParserTest()
        {
            var tokens = Builder()
                .Variable("aaa")
                .Operation(Operations.Assign)
                .ComplexNumber(new Complex(3, 2))
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(new Variable("aaa"), new ComplexNumber(new Complex(3, 2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DefineUserFuncTest()
        {
            var tokens = Builder()
                .UserFunction("func", 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Operation(Operations.Assign)
                .Function(Functions.Sine, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(new UserFunction("func", new[] { Variable.X }, 1), new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UserFunc()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(Operations.Addition)
                .UserFunction("func", 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(1), new UserFunction("func", new[] { Variable.X }, 1));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UndefParseTest()
        {
            var tokens = Builder()
                .Function(Functions.Undefine, 1)
                .OpenBracket()
                .UserFunction("f", 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Undefine(new UserFunction("f", new[] { Variable.X }, 1));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UndefWithoutParamsTest()
        {
            var tokens = Builder()
                .Function(Functions.Undefine, 0)
                .OpenBracket()
                .CloseBracket()
                .Tokens;

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParserTest()
        {
            var tokens = Builder()
                .Function(Functions.Cosine, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Operation(Operations.Addition)
                .Function(Functions.Sine, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(new Cos(Variable.X), new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GCDTest()
        {
            var tokens = Builder()
                .Function(Functions.GCD, 2)
                .OpenBracket()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GCD(new Number(12), new Number(16));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GCDOfThreeTest()
        {
            var tokens = Builder()
                .Function(Functions.GCD, 3)
                .OpenBracket()
                .Number(12)
                .Comma()
                .Number(16)
                .Comma()
                .Number(8)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GCD(new[] { new Number(12), new Number(16), new Number(8) }, 3);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LCMTest()
        {
            var tokens = Builder()
                .Function(Functions.LCM, 2)
                .OpenBracket()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new LCM(new Number(12), new Number(16));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SimplifyTest()
        {
            var tokens = Builder()
                .Function(Functions.Simplify, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .Tokens;

            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Simplify(simp, Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactorialTest()
        {
            var tokens = Builder()
                .Function(Functions.Factorial, 1)
                .OpenBracket()
                .Number(4)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Fact(new Number(4));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SumToTest()
        {
            var tokens = Builder()
                .Function(Functions.Sum, 2)
                .OpenBracket()
                .VariableX()
                .Comma()
                .Number(20)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sum(new IExpression[] { Variable.X, new Number(20) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ProductToTest()
        {
            var tokens = Builder()
                .Function(Functions.Product, 2)
                .OpenBracket()
                .VariableX()
                .Comma()
                .Number(20)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Product(new IExpression[] { Variable.X, new Number(20) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VectorTest()
        {
            var tokens = Builder()
                .Function(Functions.Vector, 3)
                .OpenBracket()
                .Number(2)
                .Comma()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Matrices.Vector(new[] { new Number(2), new Number(3), new Number(4) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VectorTwoDimTest()
        {
            var tokens = Builder()
                .Function(Functions.Matrix, 2)
                .OpenBracket()
                .Function(Functions.Vector, 2)
                .OpenBracket()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBracket()
                .Comma()
                .Function(Functions.Vector, 2)
                .OpenBracket()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBracket()
                .CloseBracket()
                .Tokens;

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
            var tokens = Builder()
                .Function(Functions.Matrix, 2)
                .OpenBracket()
                .Function(Functions.Vector, 2)
                .OpenBracket()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBracket()
                .Comma()
                .Function(Functions.Vector, 3)
                .OpenBracket()
                .Number(4)
                .Comma()
                .Number(7)
                .Comma()
                .Number(2)
                .CloseBracket()
                .CloseBracket()
                .Tokens;

            Assert.Throws<MatrixIsInvalidException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void TooMuchParamsTest()
        {
            var tokens = Builder()
                .Function(Functions.Sine, 2)
                .OpenBracket()
                .VariableX()
                .Comma()
                .Number(3)
                .CloseBracket()
                .Tokens;

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ForTest()
        {
            var tokens = Builder()
                .Function(Functions.For, 4)
                .OpenBracket()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(Operations.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(Operations.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(Operations.Assign)
                .VariableX()
                .Operation(Operations.Addition)
                .Number(1)
                .CloseBracket()
                .Tokens;

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
            var tokens = Builder()
                .Function(Functions.While, 2)
                .OpenBracket()
                .VariableX()
                .Operation(Operations.Assign)
                .VariableX()
                .Operation(Operations.Addition)
                .Number(1)
                .Comma()
                .Number(1)
                .Operation(Operations.Equal)
                .Number(1)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new While(
                new Define(Variable.X, new Add(Variable.X, new Number(1))),
                new Equal(new Number(1), new Number(1)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IfTest()
        {
            var tokens = Builder()
                .Function(Functions.If, 3)
                .OpenBracket()
                .VariableX()
                .Operation(Operations.Equal)
                .Number(0)
                .Operation(Operations.ConditionalAnd)
                .VariableY()
                .Operation(Operations.NotEqual)
                .Number(0)
                .Comma()
                .Number(2)
                .Comma()
                .Number(8)
                .CloseBracket()
                .Tokens;

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
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Equal)
                .Number(0)
                .Operation(Operations.ConditionalAnd)
                .VariableY()
                .Operation(Operations.NotEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Programming.And(
                new Equal(Variable.X, new Number(0)),
                new NotEqual(new Variable("y"), new Number(0)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Equal)
                .Number(0)
                .Operation(Operations.ConditionalOr)
                .VariableY()
                .Operation(Operations.NotEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Programming.Or(
                new Equal(Variable.X, new Number(0)),
                new NotEqual(new Variable("y"), new Number(0)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void EqualTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Equal)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Equal(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotEqualTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.NotEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new NotEqual(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LessThenTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.LessThan)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new LessThan(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.LessOrEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new LessOrEqual(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GreaterThenTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.GreaterThan)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GreaterThan(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.GreaterOrEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GreaterOrEqual(Variable.X, new Number(0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IncTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Increment)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Inc(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IncForTest()
        {
            var tokens = Builder()
                .Function(Functions.For, 4)
                .OpenBracket()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(Operations.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(Operations.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(Operations.Increment)
                .CloseBracket()
                .Tokens;

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
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Decrement)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Dec(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AddAssing()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.AddAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new AddAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MulAssing()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.MulAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new MulAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubAssing()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.SubAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new SubAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivAssing()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.DivAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new DivAssign(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void BoolConstTest()
        {
            var tokens = Builder()
                .True()
                .Operation(Operations.And)
                .False()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.And(new Bool(true), new Bool(false));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LogicAddPriorityTest()
        {
            var tokens = Builder()
                .Number(3)
                .Operation(Operations.GreaterThan)
                .Number(4)
                .Operation(Operations.And)
                .Number(1)
                .Operation(Operations.LessThan)
                .Number(3)
                .Tokens;

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
            var tokens = Builder()
                .Variable("a")
                .Operation(Operations.Or)
                .Variable("b")
                .Operation(Operations.And)
                .Variable("c")
                .Operation(Operations.And)
                .OpenBracket()
                .Variable("a")
                .Operation(Operations.Or)
                .Variable("c")
                .CloseBracket()
                .Tokens;
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
            var tokens = Builder()
                .ComplexNumber(new Complex(3, 2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(3, 2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeTest()
        {
            var tokens = Builder()
                .ComplexNumber(new Complex(3, -2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(3, -2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsTest()
        {
            var tokens = Builder()
                .ComplexNumber(new Complex(-3, -2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(-3, -2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyRePartTest()
        {
            var tokens = Builder()
                .ComplexNumber(new Complex(3, 0))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(3, 0));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyImPartTest()
        {
            var tokens = Builder()
                .ComplexNumber(new Complex(0, 2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(0, 2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyImPartNegativeTest()
        {
            var tokens = Builder()
                .ComplexNumber(new Complex(0, -2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(new Complex(0, -2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexWithVarTest1()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Subtraction)
                .ComplexNumber(new Complex(0, 2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Sub(Variable.X, new ComplexNumber(new Complex(0, 2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexWithVarTest2()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(Operations.Addition)
                .ComplexNumber(new Complex(3, -2))
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(Variable.X, new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ImTest()
        {
            var tokens = Builder()
                .Function(Functions.Im, 1)
                .OpenBracket()
                .ComplexNumber(new Complex(3, -2))
                .CloseBracket()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Im(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReTest()
        {
            var tokens = Builder()
                .Function(Functions.Re, 1)
                .OpenBracket()
                .ComplexNumber(new Complex(3, -2))
                .CloseBracket()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Re(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = Builder()
                .Function(Functions.Phase, 1)
                .OpenBracket()
                .ComplexNumber(new Complex(3, -2))
                .CloseBracket()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Phase(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = Builder()
                .Function(Functions.Conjugate, 1)
                .OpenBracket()
                .ComplexNumber(new Complex(3, -2))
                .CloseBracket()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Conjugate(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = Builder()
                .Function(Functions.Reciprocal, 1)
                .OpenBracket()
                .ComplexNumber(new Complex(3, -2))
                .CloseBracket()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Reciprocal(new ComplexNumber(new Complex(3, -2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloTest()
        {
            var tokens = Builder()
                .Number(7)
                .Operation(Operations.Modulo)
                .Number(2)
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Mod(new Number(7), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloAsFuncTest()
        {
            var tokens = Builder()
                .Number(7)
                .Operation(Operations.Modulo)
                .Number(2)
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Mod(new Number(7), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloAddTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(Operations.Addition)
                .Number(7)
                .Operation(Operations.Modulo)
                .Number(2)
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(2), new Mod(new Number(7), new Number(2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MinTest()
        {
            var tokens = Builder()
                .Function(Functions.Min, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Min(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MaxTest()
        {
            var tokens = Builder()
                .Function(Functions.Max, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Max(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AvgTest()
        {
            var tokens = Builder()
                .Function(Functions.Avg, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Avg(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CountTest()
        {
            var tokens = Builder()
                .Function(Functions.Count, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Count(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VarTest()
        {
            var tokens = Builder()
                .Function(Functions.Var, 2)
                .OpenBracket()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Var(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VarpTest()
        {
            var tokens = Builder()
                .Function(Functions.Varp, 2)
                .OpenBracket()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Varp(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void StdevTest()
        {
            var tokens = Builder()
                .Function(Functions.Stdev, 2)
                .OpenBracket()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Stdev(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void StdevpTest()
        {
            var tokens = Builder()
                .Function(Functions.Stdevp, 2)
                .OpenBracket()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Stdevp(new[] { new Number(4), new Number(9) }, 2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DelTest()
        {
            var tokens = Builder()
                .Function(Functions.Del, 1)
                .OpenBracket()
                .Number(2)
                .Operation(Operations.Multiplication)
                .VariableX()
                .Operation(Operations.Addition)
                .Number(3)
                .Operation(Operations.Multiplication)
                .VariableY()
                .Operation(Operations.Addition)
                .Number(4)
                .Operation(Operations.Multiplication)
                .Variable("z")
                .CloseBracket()
                .Tokens;

            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Del(diff, simp, new Add(new Add(new Mul(new Number(2), Variable.X), new Mul(new Number(3), new Variable("y"))), new Mul(new Number(4), new Variable("z"))));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AddTest()
        {
            var tokens = Builder()
                .Function(Functions.Add, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubTest()
        {
            var tokens = Builder()
                .Function(Functions.Sub, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sub(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MulTest()
        {
            var tokens = Builder()
                .Function(Functions.Mul, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivFuncTest()
        {
            var tokens = Builder()
                .Function(Functions.Div, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Div(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowFuncTest()
        {
            var tokens = Builder()
                .Function(Functions.Pow, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivTest()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(Operations.Division)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Div(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowTest()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(Operations.Exponentiation)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(Operations.Factorial)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Fact(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var tokens = Builder()
                .Operation(Operations.UnaryMinus)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new UnaryMinus(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotTest()
        {
            var tokens = Builder()
                .Operation(Operations.Not)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Not(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void OrTest()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(Operations.Or)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.Or(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void XOrTest()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(Operations.XOr)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new XOr(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NOrTest()
        {
            var tokens = Builder()
                .True()
                .Operation(Operations.NOr)
                .True()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new NOr(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NAndTest()
        {
            var tokens = Builder()
                .True()
                .Operation(Operations.NAnd)
                .True()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new NAnd(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ImplicationTest()
        {
            var tokens = Builder()
                .True()
                .Operation(Operations.Implication)
                .True()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Implication(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void EqualityTest()
        {
            var tokens = Builder()
                .True()
                .Operation(Operations.Equality)
                .True()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Equality(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AbsTest()
        {
            var tokens = Builder()
                .Function(Functions.Absolute, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Abs(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanTest()
        {
            var tokens = Builder()
                .Function(Functions.Tangent, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CotTest()
        {
            var tokens = Builder()
                .Function(Functions.Cotangent, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SecTest()
        {
            var tokens = Builder()
                .Function(Functions.Secant, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CscTest()
        {
            var tokens = Builder()
                .Function(Functions.Cosecant, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LnTest()
        {
            var tokens = Builder()
                .Function(Functions.Ln, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Ln(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LgTest()
        {
            var tokens = Builder()
                .Function(Functions.Lg, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lg(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LbTest()
        {
            var tokens = Builder()
                .Function(Functions.Lb, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lb(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ExpTest()
        {
            var tokens = Builder()
                .Function(Functions.Exp, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Exp(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FloorTest()
        {
            var tokens = Builder()
                .Function(Functions.Floor, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Floor(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CeilTest()
        {
            var tokens = Builder()
                .Function(Functions.Ceil, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Ceil(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void RoundTest()
        {
            var tokens = Builder()
                .Function(Functions.Round, 2)
                .OpenBracket()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Round(new Number(2), new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DefTest()
        {
            var tokens = Builder()
                .Function(Functions.Define, 2)
                .OpenBracket()
                .VariableX()
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(Variable.X, new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SqrtTest()
        {
            var tokens = Builder()
                .Function(Functions.Sqrt, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sqrt(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsinTest()
        {
            var tokens = Builder()
                .Function(Functions.Arcsine, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsin(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccosTest()
        {
            var tokens = Builder()
                .Function(Functions.Arccosine, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccos(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArctanTest()
        {
            var tokens = Builder()
                .Function(Functions.Arctangent, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arctan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccotTest()
        {
            var tokens = Builder()
                .Function(Functions.Arccotangent, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsecTest()
        {
            var tokens = Builder()
                .Function(Functions.Arcsecant, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccscTest()
        {
            var tokens = Builder()
                .Function(Functions.Arccosecant, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccsc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SinhTest()
        {
            var tokens = Builder()
                .Function(Functions.Sineh, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CoshTest()
        {
            var tokens = Builder()
                .Function(Functions.Cosineh, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanhTest()
        {
            var tokens = Builder()
                .Function(Functions.Tangenth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CothTest()
        {
            var tokens = Builder()
                .Function(Functions.Cotangenth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Coth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SechTest()
        {
            var tokens = Builder()
                .Function(Functions.Secanth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CschTest()
        {
            var tokens = Builder()
                .Function(Functions.Cosecanth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csch(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsinhTest()
        {
            var tokens = Builder()
                .Function(Functions.Arsineh, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcoshTest()
        {
            var tokens = Builder()
                .Function(Functions.Arcosineh, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArtanhTest()
        {
            var tokens = Builder()
                .Function(Functions.Artangenth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Artanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcothTest()
        {
            var tokens = Builder()
                .Function(Functions.Arcotangenth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcoth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsechTest()
        {
            var tokens = Builder()
                .Function(Functions.Arsecanth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcschTest()
        {
            var tokens = Builder()
                .Function(Functions.Arcosecanth, 1)
                .OpenBracket()
                .Number(2)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsch(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SignTest()
        {
            var tokens = Builder()
                .Function(Functions.Sign, 1)
                .OpenBracket()
                .Operation(Operations.UnaryMinus)
                .Number(10)
                .CloseBracket()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sign(new UnaryMinus(new Number(10)));

            Assert.Equal(expected, exp);
        }

    }

}