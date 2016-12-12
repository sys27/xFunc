// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokens;
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
            var exp = new Sin(new Mul(new Number(2), new Variable("x")));
            bool expected = Helpers.HasVariable(exp, new Variable("x"));

            Assert.Equal(expected, true);
        }

        [Fact]
        public void HasVarTest2()
        {
            var exp = new Sin(new Mul(new Number(2), new Number(3)));
            bool expected = Helpers.HasVariable(exp, new Variable("x"));

            Assert.Equal(expected, false);
        }

        [Fact]
        public void HasVarDiffTest1()
        {
            var exp = new GCD(new IExpression[] { new Variable("x"), new Number(2), new Number(4) }, 3);
            var expected = Helpers.HasVariable(exp, new Variable("x"));

            Assert.Equal(expected, true);
        }

        [Fact]
        public void HasVarDiffTest2()
        {
            var exp = new GCD(new IExpression[] { new Variable("y"), new Number(2), new Number(4) }, 3);
            var expected = Helpers.HasVariable(exp, new Variable("x"));

            Assert.Equal(expected, false);
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
            Assert.Equal("log(9, 3)", exp.ToString());
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
            Assert.Equal("root(x, 3)", exp.ToString());
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

            var exp = parser.Parse(tokens);
            Assert.Equal("deriv(sin(x))", exp.ToString());
        }

        [Fact]
        public void ParseDerivSecondParamIsNotVar()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Derivative, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ArgumentException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseAssign()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("x := 3", exp.ToString());
        }

        [Fact]
        public void ParseAssignWithOneParam()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign)
            };

            Assert.Throws<ParserException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void ParseAssignFirstParamIsNotVar()
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
        public void DefineParserTest()
        {
            var tokens = new List<IToken>
            {
                new VariableToken("aaa"),
                new OperationToken(Operations.Assign),
                new NumberToken(1)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("aaa := 1", exp.ToString());
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

            Assert.Throws<ParameterTypeMismatchException>(() => parser.Parse(tokens));
        }

        [Fact]
        public void AssignUserFuncTest()
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
            Assert.Equal("func(x) := sin(x)", exp.ToString());
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
            Assert.Equal("1 + func(x)", exp.ToString());
        }

        [Fact]
        public void DefineParseFailTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParameterTypeMismatchException>(() => parser.Parse(tokens));
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
            Assert.Equal("undef(f(x))", exp.ToString());
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
        public void UndefineParseFailTest()
        {
            var tokens = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new FunctionToken(Functions.Undefine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParameterTypeMismatchException>(() => parser.Parse(tokens));
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
            Assert.Equal("cos(x) + sin(x)", exp.ToString());
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
            Assert.Equal("gcd(12, 16)", exp.ToString());
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
            Assert.Equal("gcd(12, 16, 8)", exp.ToString());
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
            Assert.Equal("lcm(12, 16)", exp.ToString());
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

            var exp = parser.Parse(tokens);
            Assert.Equal("simplify(x)", exp.ToString());
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
            Assert.Equal("fact(4)", exp.ToString());
        }

        [Fact]
        public void SumToTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sum, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("sum(i, 20)", exp.ToString());
        }

        [Fact]
        public void SumFromToTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sum, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("sum(i, 2, 20)", exp.ToString());
        }

        [Fact]
        public void SumFromToIncTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sum, 4),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("sum(i, 2, 20, 2)", exp.ToString());
        }

        [Fact]
        public void SumFromToIncVarTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Sum, 5),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("k"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("k"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("sum(k, 2, 20, 2, k)", exp.ToString());
        }

        [Fact]
        public void ProductToTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Product, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("product(i, 20)", exp.ToString());
        }

        [Fact]
        public void ProductFromToTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Product, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("product(i, 2, 20)", exp.ToString());
        }

        [Fact]
        public void ProductFromToIncTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Product, 4),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("product(i, 2, 20, 2)", exp.ToString());
        }

        [Fact]
        public void ProductFromToIncVarTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Product, 5),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("k"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("k"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse(tokens);
            Assert.Equal("product(k, 2, 20, 2, k)", exp.ToString());
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
            Assert.Equal("{2, 3, 4}", exp.ToString());
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
            Assert.Equal("{{2, 3}, {4, 7}}", exp.ToString());
        }

        [Fact]
        public void MatrixAndNotVectorTest()
        {
            var tokens = new List<IToken>
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParameterTypeMismatchException>(() => parser.Parse(tokens));
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

            Assert.Equal("for(2, x := 0, x < 10, x := x + 1)", exp.ToString());
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

            Assert.Equal("while(x := x + 1, (1 == 1))", exp.ToString());
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

            Assert.Equal("if((x == 0) && (y != 0), 2, 8)", exp.ToString());
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

            Assert.Equal("(x == 0) && (y != 0)", exp.ToString());
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

            Assert.Equal("(x == 0) || (y != 0)", exp.ToString());
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

            Assert.Equal("x == 0", exp.ToString());
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

            Assert.Equal("x != 0", exp.ToString());
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

            Assert.Equal("x < 0", exp.ToString());
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

            Assert.Equal("x <= 0", exp.ToString());
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

            Assert.Equal("x > 0", exp.ToString());
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

            Assert.Equal("x >= 0", exp.ToString());
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

            Assert.Equal("x++", exp.ToString());
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

            Assert.Equal("for(2, x := 0, x < 10, x++)", exp.ToString());
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

            Assert.Equal("x--", exp.ToString());
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

            Assert.Equal("x += 2", exp.ToString());
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

            Assert.Equal("x *= 2", exp.ToString());
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

            Assert.Equal("x -= 2", exp.ToString());
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

            Assert.Equal("x /= 2", exp.ToString());
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

            Assert.Equal("True and False", exp.ToString());
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

            Assert.Equal("(3 > 4) and (1 < 3)", exp.ToString());
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
            var exp = new Implication(new Or(new Variable("a"), new Variable("b")), new Not(new Variable("c")));
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
            var expected = new Sub(new Variable("x"), new ComplexNumber(new Complex(0, 2)));

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
            var expected = new Add(new Variable("x"), new ComplexNumber(new Complex(3, -2)));

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

            var exp = parser.Parse(tokens);
            var expected = new Del(new Add(new Add(new Mul(new Number(2), new Variable("x")), new Mul(new Number(3), new Variable("y"))), new Mul(new Number(4), new Variable("z"))));

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
        public void DivTest()
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
        public void PowTest()
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

    }

}
