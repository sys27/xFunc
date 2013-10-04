using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokens;

namespace xFunc.Test
{

    [TestClass]
    public class MathParserTest
    {

        private MathParser parser;
        private MathLexerMock lexer;

        [TestInitialize]
        public void TestInit()
        {
            lexer = new MathLexerMock();
            var simplifier = new MathSimplifier();
            var differentiator = new MathDifferentiator(simplifier);
            parser = new MathParser(lexer, simplifier, new MathExpressionFactory());
        }

        [TestMethod]
        public void HasVarTest1()
        {
            IMathExpression exp = new Sin(new Mul(new Number(2), new Variable("x")));
            bool expected = MathParser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, true);
        }

        [TestMethod]
        public void HasVarTest2()
        {
            IMathExpression exp = new Sin(new Mul(new Number(2), new Number(3)));
            bool expected = MathParser.HasVar(exp, new Variable("x"));

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

            var exp = parser.Parse("log(3, 9)");
            Assert.AreEqual("log(3, 9)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
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
        [ExpectedException(typeof(MathParserException))]
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
        [ExpectedException(typeof(MathParserException))]
        public void ParseDerivWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Derivative, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("deriv(x)");
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
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
        [ExpectedException(typeof(MathParserException))]
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
        [ExpectedException(typeof(MathParserException))]
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
        [ExpectedException(typeof(MathParserException))]
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

    }

}
