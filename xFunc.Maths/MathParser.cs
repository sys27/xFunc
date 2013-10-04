// Copyright 2012-2013 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public class MathParser
    {

        private ILexer lexer;
        private ISimplifier simplifier;
        private IExpressionFactory factory;

        private string lastFunc = string.Empty;
        private IMathExpression mathExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParser"/> class with default implementations of <see cref="ILexer"/>, <see cref="ISimplifier"/> and <see cref="IExpressionFactory"/>.
        /// </summary>
        public MathParser()
            : this(new MathLexer(), new MathSimplifier(), new MathExpressionFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParser" /> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="factory">The factory.</param>
        public MathParser(ILexer lexer, ISimplifier simplifier, IExpressionFactory factory)
        {
            this.lexer = lexer;
            this.simplifier = simplifier;
            this.factory = factory;
        }

        /// <summary>
        /// Checks the <paramref name="expression"/> parameter has <paramref name="arg"/>.
        /// </summary>
        /// <param name="expression">A expression that is checked.</param>
        /// <param name="arg">A variable that can be contained in the expression.</param>
        /// <returns>true if <paramref name="expression"/> has <paramref name="arg"/>; otherwise, false.</returns>
        public static bool HasVar(IMathExpression expression, Variable arg)
        {
            if (expression is BinaryMathExpression)
            {
                var bin = expression as BinaryMathExpression;
                if (HasVar(bin.Left, arg))
                    return true;

                return HasVar(bin.Right, arg);
            }
            if (expression is UnaryMathExpression)
            {
                var un = expression as UnaryMathExpression;

                return HasVar(un.Argument, arg);
            }
            if (expression is Variable && expression.Equals(arg))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The parsed expression.</returns>
        public IMathExpression Parse(string function)
        {
#if NET40_OR_GREATER || PORTABLE
            if (string.IsNullOrWhiteSpace(function))
#elif NET20_OR_GREATER
            if (StringExtention.IsNullOrWhiteSpace(function))
#endif
                throw new ArgumentNullException("function");

            if (function != lastFunc)
            {
                IEnumerable<IToken> tokens = lexer.Tokenize(function);
                IEnumerable<IToken> rpn = ConvertToReversePolishNotation(tokens);
                IEnumerable<IMathExpression> expressions = ConvertTokensToExpressions(rpn);

                var stack = new Stack<IMathExpression>();
                foreach (var expression in expressions)
                {
                    if (expression is Number || expression is Variable)
                    {
                        stack.Push(expression);
                    }
                    else if (expression is BinaryMathExpression)
                    {
                        if ((expression is Log || expression is Root) && stack.Count < 2)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        var binExp = expression as BinaryMathExpression;
                        binExp.Right = stack.Pop();
                        binExp.Left = stack.Pop();

                        stack.Push(binExp);
                    }
                    else if (expression is UnaryMathExpression)
                    {
                        var unaryMathExp = expression as UnaryMathExpression;
                        unaryMathExp.Argument = stack.Pop();

                        stack.Push(unaryMathExp);
                    }
                    else if (expression is Derivative)
                    {
                        if (stack.Count < 2)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);
                        if (!(stack.Peek() is Variable))
                            throw new MathParserException(Resource.InvalidExpression);

                        var binExp = expression as Derivative;
                        binExp.Variable = (Variable)stack.Pop();
                        binExp.Expression = stack.Pop();

                        stack.Push(binExp);
                    }
                    else if (expression is Simplify)
                    {
                        var simp = expression as Simplify;
                        simp.Expression = stack.Pop();

                        stack.Push(simp);
                    }
                    else if (expression is Define)
                    {
                        if (stack.Count < 2)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        var assign = expression as Define;
                        assign.Value = stack.Pop();

                        var peek = stack.Peek();
                        if (!(peek is Variable || peek is UserFunction))
                            throw new MathParserException(Resource.InvalidExpression);

                        assign.Key = stack.Pop();

                        stack.Push(assign);
                    }
                    else if (expression is Undefine)
                    {
                        if (stack.Count < 1)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        var undef = expression as Undefine;

                        var peek = stack.Peek();
                        if (!(peek is Variable || peek is UserFunction))
                            throw new MathParserException(Resource.InvalidExpression);

                        undef.Key = stack.Pop();

                        stack.Push(undef);
                    }
                    else if (expression is UserFunction)
                    {
                        var func = expression as UserFunction;

                        IMathExpression[] arg = new IMathExpression[func.MaxCountOfParams];
                        for (int i = func.MaxCountOfParams, j = 0; i > 0; i--, j++)
                        {
                            arg[j] = stack.Pop();
                        }
                        func.Arguments = arg;

                        stack.Push(func);
                    }
                    else
                    {
                        throw new MathParserException(Resource.UnexpectedError);
                    }
                }

                if (stack.Count > 1)
                    throw new MathParserException(Resource.ErrorWhileParsingTree);

                lastFunc = function;
                mathExpression = stack.Pop();
            }

            return mathExpression;
        }

        private IEnumerable<IMathExpression> ConvertTokensToExpressions(IEnumerable<IToken> tokens)
        {
            var preOutput = new List<IMathExpression>();

            foreach (var token in tokens)
            {
                var exp = factory.Create(token);

                preOutput.Add(exp);
            }

            return preOutput;
        }

        private IEnumerable<IToken> ConvertToReversePolishNotation(IEnumerable<IToken> tokens)
        {
            var output = new List<IToken>();
            var stack = new Stack<IToken>();

            var openBracketToken = new SymbolToken(Symbols.OpenBracket);
            foreach (var token in tokens)
            {
                IToken stackToken;
                if (token is SymbolToken)
                {
                    var t = token as SymbolToken;
                    if (t.Symbol == Symbols.OpenBracket)
                    {
                        stack.Push(token);
                    }
                    else if (t.Symbol == Symbols.CloseBracket)
                    {
                        stackToken = stack.Pop();
                        while (!stackToken.Equals(openBracketToken))
                        {
                            output.Add(stackToken);
                            stackToken = stack.Pop();
                        }
                    }
                    else if (t.Symbol == Symbols.Comma)
                    {
                        stackToken = stack.Pop();

                        while (!stackToken.Equals(openBracketToken))
                        {
                            output.Add(stackToken);
                            stackToken = stack.Pop();
                        }

                        stack.Push(stackToken);
                    }
                }
                else if (token is NumberToken || token is VariableToken)
                {
                    output.Add(token);
                }
                else
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Priority >= token.Priority)
                    {
                        if (stackToken.Equals(openBracketToken))
                            break;
                        output.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
            }
            if (stack.Count != 0)
            {
                output.AddRange(stack);
            }

            return output;
        }

        /// <summary>
        /// Gets or Sets a implementation of <see cref="ILexer"/>.
        /// </summary>
        public ILexer Lexer
        {
            get
            {
                return lexer;
            }
            set
            {
                lexer = value;
            }
        }

        /// <summary>
        /// Gets or Sets a implementation of <see cref="ISimplifier"/>.
        /// </summary>
        public ISimplifier Simplifier
        {
            get
            {
                return simplifier;
            }
            set
            {
                simplifier = value;
            }
        }

    }

}
