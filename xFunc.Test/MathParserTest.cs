using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokens;

namespace xFunc.Test
{

    [TestClass]
    public class MathParserTest
    {

        private Parser parser;
        private MathLexerMock lexer;

        [TestInitialize]
        public void TestInit()
        {
            lexer = new MathLexerMock();
            var simplifier = new Simplifier();
            parser = new Parser(lexer, simplifier, new ExpressionFactory());
        }

        [TestMethod]
        public void HasVarTest1()
        {
            var exp = new Sin(new Mul(new Number(2), new Variable("x")));
            bool expected = Parser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, true);
        }

        [TestMethod]
        public void HasVarTest2()
        {
            var exp = new Sin(new Mul(new Number(2), new Number(3)));
            bool expected = Parser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, false);
        }

        [TestMethod]
        public void HasVarDiffTest1()
        {
            var exp = new GCD(new IExpression[] { new Variable("x"), new Number(2), new Number(4) }, 3);
            var expected = Parser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, true);
        }

        [TestMethod]
        public void HasVarDiffTest2()
        {
            var exp = new GCD(new IExpression[] { new Variable("y"), new Number(2), new Number(4) }, 3);
            var expected = Parser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseNullStr()
        {
            parser.Parse(null);
        }

        [TestMethod]
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
            Assert.AreEqual("log(9, 3)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ParseLogWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Log, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("log(9)");
        }

        [TestMethod]
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
            Assert.AreEqual("root(x, 3)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ParseRootWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Root, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("root(x)");
        }

        [TestMethod]
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
            Assert.AreEqual("deriv(sin(x))", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
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

            var exp = parser.Parse("deriv(x, 3)");
        }

        [TestMethod]
        public void ParseAssign()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            var exp = parser.Parse("x := 3");
            Assert.AreEqual("x := 3", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void ParseAssignWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign)
            };

            var exp = parser.Parse("x := ");
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ParseAssignFirstParamIsNotVar()
        {
            lexer.Tokens = new List<IToken>()
            {
                new NumberToken(5),
                new OperationToken(Operations.Assign),
                new NumberToken(3)
            };

            var exp = parser.Parse("5 := 3");
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
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

            var exp = parser.Parse("sin(x) 2");
        }

        [TestMethod]
        public void StringVarParserTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("aaa"),
                new OperationToken(Operations.Assign),
                new NumberToken(1)
            };

            var exp = parser.Parse("aaa := 1");
            Assert.AreEqual("aaa := 1", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("func(x) := sin(x)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("1 + func(x)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("undef(f(x))", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void UndefWithoutParamsTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Undefine, 0),
                new SymbolToken(Symbols.OpenBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("undef()");
        }

        [TestMethod]
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
            Assert.AreEqual("cos(x) + sin(x)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("gcd(12, 16)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("gcd(12, 16, 8)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("lcm(12, 16)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("simplify(x)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("fact(4)", exp.ToString());
        }

        [TestMethod]
        public void SaveLastExpFalseTest()
        {
            var parser = new Parser() { SaveLastExpression = false };
            var e1 = parser.Parse("e");
            var e2 = parser.Parse("e");

            Assert.AreNotSame(e1, e2);
        }

        [TestMethod]
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
            Assert.AreEqual("sum(i, 20)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("sum(i, 2, 20)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("sum(i, 2, 20, 2)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("sum(k, 2, 20, 2, k)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("product(i, 20)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("product(i, 2, 20)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("product(i, 2, 20, 2)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("product(k, 2, 20, 2, k)", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("{2, 3, 4}", exp.ToString());
        }

        [TestMethod]
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
            Assert.AreEqual("{{2, 3}, {4, 7}}", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MatrixIsInvalidException))]
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

            parser.Parse("matrix{2, 3}");
        }

        [TestMethod]
        [ExpectedException(typeof(MatrixIsInvalidException))]
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

            var exp = parser.Parse("matrix{vector{2, 3}, vector{4, 7, 2}}");
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
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

            var exp = parser.Parse("sin(x, 3)");
        }

        [TestMethod]
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

            Assert.AreEqual("for(2, x := 0, x < 10, x := x + 1)", exp.ToString());
        }

        [TestMethod]
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

            Assert.AreEqual("while(x := x + 1, (1 == 1))", exp.ToString());
        }

        [TestMethod]
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

            Assert.AreEqual("if((x == 0) && (y != 0), 2, 8)", exp.ToString());
        }

        [TestMethod]
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

            Assert.AreEqual("(x == 0) && (y != 0)", exp.ToString());
        }

        [TestMethod]
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

            Assert.AreEqual("(x == 0) || (y != 0)", exp.ToString());
        }

        [TestMethod]
        public void EqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Equal),
                new NumberToken(0),
            };

            var exp = parser.Parse("x == 0");

            Assert.AreEqual("x == 0", exp.ToString());
        }

        [TestMethod]
        public void NotEqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.NotEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse("x != 0");

            Assert.AreEqual("x != 0", exp.ToString());
        }

        [TestMethod]
        public void LessThenTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessThan),
                new NumberToken(0),
            };

            var exp = parser.Parse("x < 0");

            Assert.AreEqual("x < 0", exp.ToString());
        }

        [TestMethod]
        public void LessOrEqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessOrEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse("x <= 0");

            Assert.AreEqual("x <= 0", exp.ToString());
        }

        [TestMethod]
        public void GreaterThenTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterThan),
                new NumberToken(0),
            };

            var exp = parser.Parse("x > 0");

            Assert.AreEqual("x > 0", exp.ToString());
        }

        [TestMethod]
        public void GreaterOrEqualTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterOrEqual),
                new NumberToken(0),
            };

            var exp = parser.Parse("x >= 0");

            Assert.AreEqual("x >= 0", exp.ToString());
        }

        [TestMethod]
        public void IncTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Increment)
            };

            var exp = parser.Parse("x++");

            Assert.AreEqual("x++", exp.ToString());
        }

        [TestMethod]
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

            Assert.AreEqual("for(2, x := 0, x < 10, x++)", exp.ToString());
        }

        [TestMethod]
        public void DecTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Decrement)
            };

            var exp = parser.Parse("x--");

            Assert.AreEqual("x--", exp.ToString());
        }

        [TestMethod]
        public void AddAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.AddAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x += 2");

            Assert.AreEqual("x += 2", exp.ToString());
        }

        [TestMethod]
        public void MulAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.MulAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x *= 2");

            Assert.AreEqual("x *= 2", exp.ToString());
        }

        [TestMethod]
        public void SubAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.SubAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x -= 2");

            Assert.AreEqual("x -= 2", exp.ToString());
        }

        [TestMethod]
        public void DivAssing()
        {
            lexer.Tokens = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.DivAssign),
                new NumberToken(2)
            };

            var exp = parser.Parse("x /= 2");

            Assert.AreEqual("x /= 2", exp.ToString());
        }

        [TestMethod]
        public void BoolConstTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.And),
                new BooleanToken(false)
            };

            var exp = parser.Parse("true & false");

            Assert.AreEqual("True and False", exp.ToString());
        }

    }

}
