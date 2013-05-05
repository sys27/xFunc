using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Exceptions;
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
            parser = new MathParser(lexer);
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
                new FunctionToken(Functions.Log),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(9),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("log(9, 3)", false);
            Assert.AreEqual("log(9, 3)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseLogWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Log),
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
                new FunctionToken(Functions.Root),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("root(x, 3)", false);
            Assert.AreEqual("root(x, 3)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseRootWithOneParam()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Root),
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
                new FunctionToken(Functions.Derivative),
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
                new FunctionToken(Functions.Derivative),
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

            var exp = parser.Parse("x := 3", false);
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
                new FunctionToken(Functions.Sine),
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

            var exp = parser.Parse("aaa := 1", false);
            Assert.AreEqual("aaa := 1", exp.ToString());
        }

        [TestMethod]
        public void AssignUserFuncTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new UserFunctionToken("func"),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new OperationToken(Operations.Assign),
                new FunctionToken(Functions.Sine),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("func(x) := sin(x)", false);
            Assert.AreEqual("func(x) := sin(x)", exp.ToString());
        }

        [TestMethod]
        public void UserFunc()
        {
            lexer.Tokens = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.Addition),
                new UserFunctionToken("func"),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("1 + func(x)", false);
            Assert.AreEqual("1 + func(x)", exp.ToString());
        }

        [TestMethod]
        public void ParserTest()
        {
            lexer.Tokens = new List<IToken>()
            {
                new FunctionToken(Functions.Cosine),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new OperationToken(Operations.Addition),
                new FunctionToken(Functions.Sine),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            var exp = parser.Parse("cos(x) + sin(x)", false);
            Assert.AreEqual("cos(x) + sin(x)", exp.ToString());
        }

    }

}
