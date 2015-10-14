// Copyright 2012-2015 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokens;
using Xunit;

namespace xFunc.Test
{

    public class MathParserTest
    {

        private Parser parser;
        private MathLexerMock lexer;

        public MathParserTest()
        {
            lexer = new MathLexerMock();
            var simplifier = new Simplifier();
            parser = new Parser(lexer, simplifier, new ExpressionFactory());
        }

        [Fact]
        public void HasVarTest1()
        {
            var exp = new Sin(new Mul(new Number(2), new Variable("x")));
            bool expected = Parser.HasVar(exp, new Variable("x"));

            Assert.Equal(expected, true);
        }

        [Fact]
        public void HasVarTest2()
        {
            var exp = new Sin(new Mul(new Number(2), new Number(3)));
            bool expected = Parser.HasVar(exp, new Variable("x"));

            Assert.Equal(expected, false);
        }

        [Fact]
        public void HasVarDiffTest1()
        {
            var exp = new GCD(new IExpression[] { new Variable("x"), new Number(2), new Number(4) }, 3);
            var expected = Parser.HasVar(exp, new Variable("x"));

            Assert.Equal(expected, true);
        }

        [Fact]
        public void HasVarDiffTest2()
        {
            var exp = new GCD(new IExpression[] { new Variable("y"), new Number(2), new Number(4) }, 3);
            var expected = Parser.HasVar(exp, new Variable("x"));

            Assert.Equal(expected, false);
        }

        [Fact]
        public void ParseNullStr()
        {
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Fact]
        public void ParseLog()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Log, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(9),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("log(9, 3)");
            Assert.Equal("log(9, 3)", exp.ToString());
        }

        [Fact]
        public void ParseLogWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Log, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse("log(9)"));
        }

        [Fact]
        public void ParseRoot()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Root, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("root(x, 3)");
            Assert.Equal("root(x, 3)", exp.ToString());
        }

        [Fact]
        public void ParseRootWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Root, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse("root(x)"));
        }

        [Fact]
        public void ParseDerivWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Derivative, 1),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("deriv(sin(x))");
            Assert.Equal("deriv(sin(x))", exp.ToString());
        }

        [Fact]
        public void ParseDerivSecondParamIsNotVar()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Derivative, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ArgumentException>(() => parser.Parse("deriv(x, 3)"));
        }

        [Fact]
        public void ParseAssign()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            var exp = parser.Parse("x := 3");
            Assert.Equal("x := 3", exp.ToString());
        }

        [Fact]
        public void ParseAssignWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign)
            };

            Assert.Throws<ParserException>(() => parser.Parse("x := "));
        }

        [Fact]
        public void ParseAssignFirstParamIsNotVar()
        {
            lexer.Tokens = new List<IToken>()
            {
                new NumberToken(5),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            Assert.Throws<NotSupportedException>(() => parser.Parse("5 := 3"));
        }

        [Fact]
        public void ErrorWhileParsingTree()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new NumberToken(2)
            };

            Assert.Throws<ParserException>(() => parser.Parse("sin(x) 2"));
        }

        [Fact]
        public void StringVarParserTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("aaa"),
                new OperationToken(Operations.Assign),
                new NumberToken(1)
            };

            var exp = parser.Parse("aaa := 1");
            Assert.Equal("aaa := 1", exp.ToString());
        }

        [Fact]
        public void AssignUserFuncTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("func(x) := sin(x)");
            Assert.Equal("func(x) := sin(x)", exp.ToString());
        }

        [Fact]
        public void UserFunc()
        {
            lexer.Tokens = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.Addition),
                new UserFunctionToken("func", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("1 + func(x)");
            Assert.Equal("1 + func(x)", exp.ToString());
        }

        [Fact]
        public void UndefParseTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Undefine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new UserFunctionToken("f", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("undef(f(x))");
            Assert.Equal("undef(f(x))", exp.ToString());
        }

        [Fact]
        public void UndefWithoutParamsTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Undefine, 0),
                new SymbolToken(Symbols.OpenBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse("undef()"));
        }

        [Fact]
        public void ParserTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("cos(x) + sin(x)");
            Assert.Equal("cos(x) + sin(x)", exp.ToString());
        }

        [Fact]
        public void GCDTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.GCD, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("gcd(12, 16)");
            Assert.Equal("gcd(12, 16)", exp.ToString());
        }

        [Fact]
        public void GCDOfThreeTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("gcd(12, 16, 8)");
            Assert.Equal("gcd(12, 16, 8)", exp.ToString());
        }

        [Fact]
        public void LCMTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.LCM, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("lcm(12, 16)");
            Assert.Equal("lcm(12, 16)", exp.ToString());
        }

        [Fact]
        public void SimplifyTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Simplify, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("simplify(x)");
            Assert.Equal("simplify(x)", exp.ToString());
        }

        [Fact]
        public void FactorialTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Factorial, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("fact(4)");
            Assert.Equal("fact(4)", exp.ToString());
        }

        [Fact]
        public void SaveLastExpFalseTest()
        {
            var parser = new Parser() { SaveLastExpression = false };
            var e1 = parser.Parse("e");
            var e2 = parser.Parse("e");

            Assert.NotSame(e1, e2);
        }

        [Fact]
        public void SumToTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("sum(i, 20)");
            Assert.Equal("sum(i, 20)", exp.ToString());
        }

        [Fact]
        public void SumFromToTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("sum(i, 2, 20)");
            Assert.Equal("sum(i, 2, 20)", exp.ToString());
        }

        [Fact]
        public void SumFromToIncTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("sum(i, 2, 20, 2)");
            Assert.Equal("sum(i, 2, 20, 2)", exp.ToString());
        }

        [Fact]
        public void SumFromToIncVarTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("sum(k, 2, 20, 2, k)");
            Assert.Equal("sum(k, 2, 20, 2, k)", exp.ToString());
        }

        [Fact]
        public void ProductToTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("product(i, 20)");
            Assert.Equal("product(i, 20)", exp.ToString());
        }

        [Fact]
        public void ProductFromToTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("product(i, 2, 20)");
            Assert.Equal("product(i, 2, 20)", exp.ToString());
        }

        [Fact]
        public void ProductFromToIncTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("product(i, 2, 20, 2)");
            Assert.Equal("product(i, 2, 20, 2)", exp.ToString());
        }

        [Fact]
        public void ProductFromToIncVarTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("product(k, 2, 20, 2, k)");
            Assert.Equal("product(k, 2, 20, 2, k)", exp.ToString());
        }

        [Fact]
        public void VectorTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("vector{2, 3, 4}");
            Assert.Equal("{2, 3, 4}", exp.ToString());
        }

        [Fact]
        public void VectorTwoDimTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("matrix{vector{2, 3}, vector{4, 7}}");
            Assert.Equal("{{2, 3}, {4, 7}}", exp.ToString());
        }

        [Fact]
        public void MatrixAndNotVectorTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<MatrixIsInvalidException>(() => parser.Parse("matrix{2, 3}"));
        }

        [Fact]
        public void MatrixWithDiffVectorSizeTest()
        {
            lexer.Tokens = new List<IToken>()
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

            Assert.Throws<MatrixIsInvalidException>(() => parser.Parse("matrix{vector{2, 3}, vector{4, 7, 2}}"));
        }

        [Fact]
        public void TooMuchParamsTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Sine, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Throws<ParserException>(() => parser.Parse("sin(x, 3)"));
        }

        [Fact]
        public void ForTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("for(2, x := 0, x < 10, x := x + 1)");

            Assert.Equal("for(2, x := 0, x < 10, x := x + 1)", exp.ToString());
        }

        [Fact]
        public void WhileTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("while(x := x + 1, (1 == 1))");

            Assert.Equal("while(x := x + 1, (1 == 1))", exp.ToString());
        }

        [Fact]
        public void IfTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("if(x == 0 && y != 0, 2, 8)");

            Assert.Equal("if((x == 0) && (y != 0), 2, 8)", exp.ToString());
        }

        [Fact]
        public void ConditionalAndTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
                new OperationToken(Operations.ConditionalAnd),
                new VariableToken("y"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0)
            };

            var exp = parser.Parse("x == 0 && y != 0");

            Assert.Equal("(x == 0) && (y != 0)", exp.ToString());
        }

        [Fact]
        public void ConditionalOrTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
                new OperationToken(Operations.ConditionalOr),
                new VariableToken("y"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0)
            };

            var exp = parser.Parse("x == 0 || y != 0");

            Assert.Equal("(x == 0) || (y != 0)", exp.ToString());
        }

        [Fact]
        public void EqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
            };

            var exp = parser.Parse("x == 0");

            Assert.Equal("x == 0", exp.ToString());
        }

        [Fact]
        public void NotEqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse("x != 0");

            Assert.Equal("x != 0", exp.ToString());
        }

        [Fact]
        public void LessThenTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessThan),
                new NumberToken(0),
            };

            var exp = parser.Parse("x < 0");

            Assert.Equal("x < 0", exp.ToString());
        }

        [Fact]
        public void LessOrEqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessOrEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse("x <= 0");

            Assert.Equal("x <= 0", exp.ToString());
        }

        [Fact]
        public void GreaterThenTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterThan),
                new NumberToken(0),
            };

            var exp = parser.Parse("x > 0");

            Assert.Equal("x > 0", exp.ToString());
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterOrEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse("x >= 0");

            Assert.Equal("x >= 0", exp.ToString());
        }

        [Fact]
        public void IncTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Increment)
            };

            var exp = parser.Parse("x++");

            Assert.Equal("x++", exp.ToString());
        }

        [Fact]
        public void IncForTest()
        {
            lexer.Tokens = new List<IToken>()
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

            var exp = parser.Parse("for(2, x := 0, x < 10, x++)");

            Assert.Equal("for(2, x := 0, x < 10, x++)", exp.ToString());
        }

        [Fact]
        public void DecTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Decrement)
            };

            var exp = parser.Parse("x--");

            Assert.Equal("x--", exp.ToString());
        }

        [Fact]
        public void AddAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.AddAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x += 2");

            Assert.Equal("x += 2", exp.ToString());
        }

        [Fact]
        public void MulAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.MulAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x *= 2");

            Assert.Equal("x *= 2", exp.ToString());
        }

        [Fact]
        public void SubAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.SubAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x -= 2");

            Assert.Equal("x -= 2", exp.ToString());
        }

        [Fact]
        public void DivAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.DivAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x /= 2");

            Assert.Equal("x /= 2", exp.ToString());
        }

        [Fact]
        public void BoolConstTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.And),
                new BooleanToken(false)
            };

            var exp = parser.Parse("true & false");

            Assert.Equal("True and False", exp.ToString());
        }

        [Fact]
        public void GetLogicParametersTest()
        {
            string function = "a | b & c & (a | c)";
            lexer.Tokens = new List<IToken>
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

            var actual = parser.GetParameters(function);


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertLogicExpressionToColletionTest()
        {
            var exp = new Implication(new Or(new Variable("a"), new Variable("b")), new Not(new Variable("c")));
            var actual = new List<IExpression>(parser.ConvertExpressionToCollection(exp));

            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void ConvertLogicExpressionToColletionNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => parser.ConvertExpressionToCollection(null));
        }

    }

}
