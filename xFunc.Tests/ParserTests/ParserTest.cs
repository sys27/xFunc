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
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.ParserTests
{
    public class ParserTest : BaseParserTests
    {
        [Fact]
        public void HasVarTest1()
        {
            var exp = new Sin(new Mul(new Number(2), Variable.X));
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.True(expected);
        }

        [Fact]
        public void HasVarTest2()
        {
            var exp = new Sin(new Mul(new Number(2), new Number(3)));
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.False(expected);
        }

        [Fact]
        public void HasVarDiffTest1()
        {
            var exp = new GCD(new IExpression[] { Variable.X, new Number(2), new Number(4) });
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.True(expected);
        }

        [Fact]
        public void HasVarDiffTest2()
        {
            var exp = new GCD(new IExpression[] { new Variable("y"), new Number(2), new Number(4) });
            var expected = Helpers.HasVariable(exp, Variable.X);

            Assert.False(expected);
        }

        [Fact]
        public void DifferentiatorNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Parser(null, null));
        }

        [Fact]
        public void SimplifierNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Parser(new Differentiator(), null));
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
                .Id("log")
                .OpenParenthesis()
                .Number(9)
                .Comma()
                .Number(3)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Log(new Number(9), new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseLogWithOneParam()
        {
            var tokens = Builder()
                .Id("log")
                .OpenParenthesis()
                .Number(9)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ParseRoot()
        {
            var tokens = Builder()
                .Id("root")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(3)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Root(Variable.X, new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseRootWithOneParam()
        {
            var tokens = Builder()
                .Id("root")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ParseDerivWithOneParam()
        {
            var tokens = Builder()
                .Id("derivative")
                .OpenParenthesis()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .CloseParenthesis()
                .Tokens;

            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Derivative(diff, simp, new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseDerivWithTwoParam()
        {
            var tokens = Builder()
                .Id("derivative")
                .OpenParenthesis()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Comma()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = parser.Parse(tokens);
            var expected = new Derivative(diff, simp, new Sin(Variable.X), Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ErrorWhileParsingTree()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Number(2)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void UserFunc()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(OperatorToken.Plus)
                .Id("func")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(1), new UserFunction("func", new IExpression[] { Variable.X }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseTest()
        {
            var tokens = Builder()
                .Id("cos")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Operation(OperatorToken.Plus)
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(new Cos(Variable.X), new Sin(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GCDTest()
        {
            var tokens = Builder()
                .Id("gcd")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GCD(new Number(12), new Number(16));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void GCDOfThreeTest()
        {
            var tokens = Builder()
                .Id("gcd")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .Comma()
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GCD(new IExpression[] { new Number(12), new Number(16), new Number(8) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LCMTest()
        {
            var tokens = Builder()
                .Id("lcm")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new LCM(new Number(12), new Number(16));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SimplifyTest()
        {
            var tokens = Builder()
                .Id("simplify")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
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
                .Id("factorial")
                .OpenParenthesis()
                .Number(4)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Fact(new Number(4));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SumToTest()
        {
            var tokens = Builder()
                .Id("sum")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(20)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sum(new IExpression[] { Variable.X, new Number(20) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ProductToTest()
        {
            var tokens = Builder()
                .Id("product")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(20)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Product(new IExpression[] { Variable.X, new Number(20) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VectorTest()
        {
            var tokens = Builder()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Vector(new IExpression[] { new Number(2), new Number(3), new Number(4) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VectorTwoDimTest()
        {
            var tokens = Builder()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Matrix(new IExpression[]
            {
                new Vector(new IExpression[] { new Number(2), new Number(3) }),
                new Vector(new IExpression[] { new Number(4), new Number(7) })
            });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MatrixWithDiffVectorSizeTest()
        {
            var tokens = Builder()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .Comma()
                .Number(2)
                .CloseBrace()
                .CloseBrace()
                .Tokens;

            Assert.Throws<MatrixIsInvalidException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void TooMuchParamsTest()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(3)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ConditionalAndTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Operation(OperatorToken.ConditionalAnd)
                .VariableY()
                .Operation(OperatorToken.NotEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Programming.And(
                new Equal(Variable.X, new Number(0)),
                new NotEqual(new Variable("y"), new Number(0)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConditionalAndMissingSecondOperand()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Operation(OperatorToken.ConditionalAnd)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Operation(OperatorToken.ConditionalOr)
                .VariableY()
                .Operation(OperatorToken.NotEqual)
                .Number(0)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.Programming.Or(
                new Equal(Variable.X, new Number(0)),
                new NotEqual(new Variable("y"), new Number(0)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConditionalOrMissingSecondOperand()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Operation(OperatorToken.ConditionalOr)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void EqualTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Equal)
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
                .Operation(OperatorToken.NotEqual)
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
                .Operation(OperatorToken.LessThan)
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
                .Operation(OperatorToken.LessOrEqual)
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
                .Operation(OperatorToken.GreaterThan)
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
                .Operation(OperatorToken.GreaterOrEqual)
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
                .Operation(OperatorToken.Increment)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Inc(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DecTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Decrement)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Dec(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void BoolConstTest()
        {
            var tokens = Builder()
                .True()
                .Operation(OperatorToken.And)
                .False()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.And(new Bool(true), new Bool(false));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AndKeywordTest()
        {
            var tokens = Builder()
                .True()
                .Keyword(KeywordToken.And)
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
                .Operation(OperatorToken.GreaterThan)
                .Number(4)
                .Operation(OperatorToken.And)
                .Number(1)
                .Operation(OperatorToken.LessThan)
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
                .Id("a")
                .Operation(OperatorToken.Or)
                .Id("b")
                .Operation(OperatorToken.And)
                .Id("c")
                .Operation(OperatorToken.And)
                .OpenParenthesis()
                .Id("a")
                .Operation(OperatorToken.Or)
                .Id("c")
                .CloseParenthesis()
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
        public void ConvertLogicExpressionToCollectionTest()
        {
            var exp = new Implication(new Maths.Expressions.LogicalAndBitwise.Or(new Variable("a"), new Variable("b")), new Not(new Variable("c")));
            var actual = new List<IExpression>(Helpers.ConvertExpressionToCollection(exp));

            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void ConvertLogicExpressionToCollectionNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Helpers.ConvertExpressionToCollection(null));
        }

        [Fact]
        public void ImTest()
        {
            var tokens = Builder()
                .Id("im")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Im(
                new Sub(
                    new Number(3),
                    new Mul(
                        new Number(2),
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReTest()
        {
            var tokens = Builder()
                .Id("re")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Re(
                new Sub(
                    new Number(3),
                    new Mul(
                        new Number(2),
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = Builder()
                .Id("phase")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Phase(
                new Sub(
                    new Number(3),
                    new Mul(
                        new Number(2),
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = Builder()
                .Id("conjugate")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Conjugate(
                new Sub(
                    new Number(3),
                    new Mul(
                        new Number(2),
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = Builder()
                .Id("reciprocal")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Reciprocal(
                new Sub(
                    new Number(3),
                    new Mul(
                        new Number(2),
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloTest()
        {
            var tokens = Builder()
                .Number(7)
                .Operation(OperatorToken.Modulo)
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
                .Operation(OperatorToken.Plus)
                .Number(7)
                .Operation(OperatorToken.Modulo)
                .Number(2)
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(2), new Mod(new Number(7), new Number(2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ModuloKeywordAddTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(OperatorToken.Plus)
                .Number(7)
                .Keyword(KeywordToken.Mod)
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
                .Id("min")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Min(new IExpression[] { new Number(1), new Number(2) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MaxTest()
        {
            var tokens = Builder()
                .Id("max")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Max(new IExpression[] { new Number(1), new Number(2) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AvgTest()
        {
            var tokens = Builder()
                .Id("avg")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Avg(new IExpression[] { new Number(1), new Number(2) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CountTest()
        {
            var tokens = Builder()
                .Id("count")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Count(new IExpression[] { new Number(1), new Number(2) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VarTest()
        {
            var tokens = Builder()
                .Id("var")
                .OpenParenthesis()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Var(new IExpression[] { new Number(4), new Number(9) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void VarpTest()
        {
            var tokens = Builder()
                .Id("varp")
                .OpenParenthesis()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Varp(new IExpression[] { new Number(4), new Number(9) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void StdevTest()
        {
            var tokens = Builder()
                .Id("stdev")
                .OpenParenthesis()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Stdev(new IExpression[] { new Number(4), new Number(9) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void StdevpTest()
        {
            var tokens = Builder()
                .Id("stdevp")
                .OpenParenthesis()
                .Number(4)
                .Comma()
                .Number(9)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Stdevp(new IExpression[] { new Number(4), new Number(9) });

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DelTest()
        {
            var tokens = Builder()
                .Id("del")
                .OpenParenthesis()
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(3)
                .Operation(OperatorToken.Multiplication)
                .VariableY()
                .Operation(OperatorToken.Plus)
                .Number(4)
                .Operation(OperatorToken.Multiplication)
                .Id("z")
                .CloseParenthesis()
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
                .Id("add")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubTest()
        {
            var tokens = Builder()
                .Id("sub")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sub(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MulTest()
        {
            var tokens = Builder()
                .Id("mul")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivFuncTest()
        {
            var tokens = Builder()
                .Id("div")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Div(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowFuncTest()
        {
            var tokens = Builder()
                .Id("pow")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
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
                .Operation(OperatorToken.Division)
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
                .Operation(OperatorToken.Exponentiation)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowRightAssociativityTest()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(OperatorToken.Exponentiation)
                .Number(2)
                .Operation(OperatorToken.Exponentiation)
                .Number(3)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Number(1), new Pow(new Number(2), new Number(3)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PowUnaryMinusTest()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(1)
                .Operation(OperatorToken.Exponentiation)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new UnaryMinus(new Pow(new Number(1), new Number(2)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(OperatorToken.Factorial)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Fact(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactPowTest()
        {
            var tokens = Builder()
                .Number(4)
                .Operation(OperatorToken.Factorial)
                .Operation(OperatorToken.Exponentiation)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Pow(new Fact(new Number(4)), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FactWithoutNumberTest()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Factorial)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void FactBoolTest()
        {
            var tokens = Builder()
                .True()
                .Operation(OperatorToken.Factorial)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new UnaryMinus(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SignTest()
        {
            var tokens = Builder()
                .Id("sign")
                .OpenParenthesis()
                .Operation(OperatorToken.Minus)
                .Number(10)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sign(new UnaryMinus(new Number(10)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusAfterCommaTest()
        {
            var tokens = Builder()
                .Id("gcd")
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .Operation(OperatorToken.Minus)
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new GCD(new Number(2), new UnaryMinus(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotTest()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Not)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Not(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotKeywordTest()
        {
            var tokens = Builder()
                .Keyword(KeywordToken.Not)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Not(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NotAfterNumberTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(OperatorToken.Not)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NotAfterBracketTest()
        {
            var tokens = Builder()
                .OpenParenthesis()
                .Number(2)
                .Operation(OperatorToken.Plus)
                .Number(1)
                .CloseParenthesis()
                .Operation(OperatorToken.Not)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void OrTest()
        {
            var tokens = Builder()
                .Number(1)
                .Operation(OperatorToken.Or)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Maths.Expressions.LogicalAndBitwise.Or(new Number(1), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void OrKeywordTest()
        {
            var tokens = Builder()
                .Number(1)
                .Keyword(KeywordToken.Or)
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
                .Keyword(KeywordToken.XOr)
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
                .Keyword(KeywordToken.NOr)
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
                .Keyword(KeywordToken.NAnd)
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
                .Operation(OperatorToken.Implication)
                .True()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Implication(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ImplicationKeywordTest()
        {
            var tokens = Builder()
                .True()
                .Keyword(KeywordToken.Impl)
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
                .Operation(OperatorToken.Equality)
                .True()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Equality(new Bool(true), new Bool(true));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void EqualityKeywordTest()
        {
            var tokens = Builder()
                .True()
                .Keyword(KeywordToken.Eq)
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
                .Id("abs")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Abs(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanTest()
        {
            var tokens = Builder()
                .Id("tan")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CotTest()
        {
            var tokens = Builder()
                .Id("cot")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SecTest()
        {
            var tokens = Builder()
                .Id("sec")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CscTest()
        {
            var tokens = Builder()
                .Id("csc")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LnTest()
        {
            var tokens = Builder()
                .Id("ln")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Ln(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LgTest()
        {
            var tokens = Builder()
                .Id("lg")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lg(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LbTest()
        {
            var tokens = Builder()
                .Id("lb")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lb(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ExpTest()
        {
            var tokens = Builder()
                .Id("exp")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Exp(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void FloorTest()
        {
            var tokens = Builder()
                .Id("floor")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Floor(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CeilTest()
        {
            var tokens = Builder()
                .Id("ceil")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Ceil(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void RoundTest()
        {
            var tokens = Builder()
                .Id("round")
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Round(new Number(2), new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SqrtTest()
        {
            var tokens = Builder()
                .Id("sqrt")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sqrt(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsinTest()
        {
            var tokens = Builder()
                .Id("arcsin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsin(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccosTest()
        {
            var tokens = Builder()
                .Id("arccos")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccos(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArctanTest()
        {
            var tokens = Builder()
                .Id("arctan")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arctan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccotTest()
        {
            var tokens = Builder()
                .Id("arccot")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsecTest()
        {
            var tokens = Builder()
                .Id("arcsec")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccscTest()
        {
            var tokens = Builder()
                .Id("arccsc")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccsc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SinhTest()
        {
            var tokens = Builder()
                .Id("sinh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CoshTest()
        {
            var tokens = Builder()
                .Id("cosh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanhTest()
        {
            var tokens = Builder()
                .Id("tanh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CothTest()
        {
            var tokens = Builder()
                .Id("coth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Coth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SechTest()
        {
            var tokens = Builder()
                .Id("sech")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CschTest()
        {
            var tokens = Builder()
                .Id("csch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csch(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsinhTest()
        {
            var tokens = Builder()
                .Id("arsinh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcoshTest()
        {
            var tokens = Builder()
                .Id("arcosh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArtanhTest()
        {
            var tokens = Builder()
                .Id("artanh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Artanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcothTest()
        {
            var tokens = Builder()
                .Id("arcoth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcoth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsechTest()
        {
            var tokens = Builder()
                .Id("arsech")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcschTest()
        {
            var tokens = Builder()
                .Id("arcsch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsch(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DotProductTest()
        {
            var tokens = Builder()
                .Id("dotproduct")
                .OpenParenthesis()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new DotProduct(
                new Vector(new IExpression[] { new Number(1), new Number(2) }),
                new Vector(new IExpression[] { new Number(3), new Number(4) })
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CrossProductTest()
        {
            var tokens = Builder()
                .Id("crossproduct")
                .OpenParenthesis()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(5)
                .Comma()
                .Number(6)
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new CrossProduct(
                new Vector(new IExpression[] { new Number(1), new Number(2), new Number(3) }),
                new Vector(new IExpression[] { new Number(4), new Number(5), new Number(6) })
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TransposeTest()
        {
            var tokens = Builder()
                .Id("transpose")
                .OpenParenthesis()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Transpose(new Matrix(
                new IExpression[]
                {
                    new Vector(new IExpression[] { new Number(2), new Number(3) }),
                    new Vector(new IExpression[] { new Number(4), new Number(7) })
                }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DeterminantTest()
        {
            var tokens = Builder()
                .Id("determinant")
                .OpenParenthesis()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Determinant(new Matrix(
                new IExpression[]
                {
                    new Vector(new IExpression[] { new Number(2), new Number(3) }),
                    new Vector(new IExpression[] { new Number(4), new Number(7) })
                }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void InverseTest()
        {
            var tokens = Builder()
                .Id("inverse")
                .OpenParenthesis()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Inverse(new Matrix(
                new IExpression[]
                {
                    new Vector(new IExpression[] { new Number(2), new Number(3) }),
                    new Vector(new IExpression[] { new Number(4), new Number(7) })
                }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void RoundNotEnoughParameters()
        {
            var tokens = Builder()
                .Id("round")
                .OpenParenthesis()
                .CloseParenthesis()
                .Tokens;

            Assert.Throws<ArgumentException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void NotEnoughParamsTest()
        {
            var tokens = Builder()
                .Id("derivative")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NumAndVar()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(2)
                .VariableX()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(
                new UnaryMinus(new Number(2)),
                Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NumAndFunc()
        {
            var tokens = Builder()
                .Number(5)
                .Operation(OperatorToken.Multiplication)
                .Id("cos")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(
                new Number(5),
                new Cos(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void Pi()
        {
            var tokens = Builder()
                .Number(3)
                .Id("pi")
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(
                new Number(3),
                new Variable("pi"));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NumberMulBracketTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Plus)
                .VariableY()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(
                new Number(2),
                new Add(
                    Variable.X,
                    new Variable("y")));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NumberMulVectorTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(
                new Number(2),
                new Vector(new IExpression[] { new Number(1), new Number(2) }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void NumberMulMatrixTest()
        {
            var tokens = Builder()
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .OpenBrace()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .CloseBrace()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Mul(
                new Number(2),
                new Matrix(new IExpression[]
                {
                    new Vector(new IExpression[] { new Number(1), new Number(2) }),
                    new Vector(new IExpression[] { new Number(3), new Number(4) })
                }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MatrixMissingCloseBraceTest()
        {
            var tokens = Builder()
                .OpenBrace()
                .OpenBrace()
                .Number(1)
                .CloseBrace()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void UnaryPlusTest()
        {
            var tokens = Builder()
                .OpenParenthesis()
                .Operation(OperatorToken.Plus)
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Number(2);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SinUnaryPlusTest()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .Operation(OperatorToken.Plus)
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sin(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryPlusVariableTest()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .Operation(OperatorToken.Plus)
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sin(Variable.X);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void Integer()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(2764786)
                .Operation(OperatorToken.Plus)
                .Number(46489879)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(
                new UnaryMinus(new Number(2764786)),
                new Number(46489879)
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void Double()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(45.3)
                .Operation(OperatorToken.Plus)
                .Number(87.64)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Add(
                new UnaryMinus(new Number(45.3)),
                new Number(87.64)
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubAfterOpenBracket()
        {
            var tokens = Builder()
                .OpenParenthesis()
                .Operation(OperatorToken.Minus)
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new UnaryMinus(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseSinWithIncorrectParametersCount()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Operation(OperatorToken.Plus)
                .Id("cos")
                .OpenParenthesis()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ParseSinWithoutParameters()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .Number(1)
                .CloseParenthesis()
                .Id("cos")
                .OpenParenthesis()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ParseCosWithIncorrectParametersCount()
        {
            var tokens = Builder()
                .Id("cos")
                .OpenParenthesis()
                .Id("sin")
                .OpenParenthesis()
                .Number(1)
                .CloseParenthesis()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Operation(OperatorToken.Plus)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ParseCosWithoutOperator()
        {
            var tokens = Builder()
                .Id("cos")
                .OpenParenthesis()
                .CloseParenthesis()
                .Number(1)
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}