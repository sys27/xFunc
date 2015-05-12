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
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
using xFunc.Maths.Expressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public class Parser
    {

        private ILexer lexer;
        private ISimplifier simplifier;
        private IExpressionFactory factory;

        private bool saveLastExpression = true;
        private string lastFunc = string.Empty;
        private IExpression mathExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with default implementations of <see cref="ILexer"/>, <see cref="ISimplifier"/> and <see cref="IExpressionFactory"/>.
        /// </summary>
        public Parser()
            : this(new Lexer(), new Simplifier(), new ExpressionFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser" /> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="factory">The factory.</param>
        public Parser(ILexer lexer, ISimplifier simplifier, IExpressionFactory factory)
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
        public static bool HasVar(IExpression expression, Variable arg)
        {
            if (expression is BinaryExpression)
            {
                var bin = expression as BinaryExpression;
                if (HasVar(bin.Left, arg))
                    return true;

                return HasVar(bin.Right, arg);
            }
            if (expression is UnaryExpression)
            {
                var un = expression as UnaryExpression;

                return HasVar(un.Argument, arg);
            }
            if (expression is DifferentParametersExpression)
            {
                var paramExp = expression as DifferentParametersExpression;

                return paramExp.Arguments.Any(e => HasVar(e, arg));
            }

            return expression is Variable && expression.Equals(arg);
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The parsed expression.</returns>
        public IExpression Parse(string function)
        {
#if NET40_OR_GREATER || PORTABLE
            if (string.IsNullOrWhiteSpace(function))
#elif NET20_OR_GREATER
            if (StringExtension.IsNullOrWhiteSpace(function))
#endif
                throw new ArgumentNullException("function");

            if (!saveLastExpression || function != lastFunc)
            {
                IEnumerable<IToken> tokens = lexer.Tokenize(function);
                IEnumerable<IToken> rpn = ConvertToReversePolishNotation(tokens);
                IEnumerable<IExpression> expressions = ConvertTokensToExpressions(rpn);

                var stack = new Stack<IExpression>();
                foreach (var expression in expressions)
                {
                    if (expression is Number || expression is Variable)
                    {
                        stack.Push(expression);
                    }
                    else if (expression is BinaryExpression)
                    {
                        if ((expression is Log || expression is Root) && stack.Count < 2)
                            throw new ParserException(Resource.InvalidNumberOfVariables);

                        var binExp = expression as BinaryExpression;
                        binExp.Right = stack.Pop();
                        binExp.Left = stack.Pop();

                        stack.Push(binExp);
                    }
                    else if (expression is UnaryExpression)
                    {
                        var unaryMathExp = expression as UnaryExpression;
                        unaryMathExp.Argument = stack.Pop();

                        stack.Push(unaryMathExp);
                    }
                    else if (expression is DifferentParametersExpression)
                    {
                        var func = expression as DifferentParametersExpression;

                        IExpression[] arg = new IExpression[func.ParametersCount];
                        for (int i = func.ParametersCount - 1; i >= 0; i--)
                            arg[i] = stack.Pop();

                        if (func is Derivative && func.ParametersCount == 2 && !(arg[1] is Variable))
                            throw new ParserException(Resource.InvalidExpression);

                        func.Arguments = arg;

                        stack.Push(func);
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
                            throw new ParserException(Resource.InvalidNumberOfVariables);

                        var assign = expression as Define;
                        assign.Value = stack.Pop();
                        assign.Key = stack.Pop();

                        stack.Push(assign);
                    }
                    else if (expression is Undefine)
                    {
                        if (stack.Count < 1)
                            throw new ParserException(Resource.InvalidNumberOfVariables);

                        var undef = expression as Undefine;
                        undef.Key = stack.Pop();

                        stack.Push(undef);
                    }
                    else
                    {
                        throw new ParserException(Resource.UnexpectedError);
                    }
                }

                if (stack.Count > 1)
                    throw new ParserException(Resource.ErrorWhileParsingTree);

                if (saveLastExpression)
                {
                    lastFunc = function;
                    mathExpression = stack.Pop();
                }
                else
                {
                    return stack.Pop();
                }
            }

            return mathExpression;
        }

        private IEnumerable<IExpression> ConvertTokensToExpressions(IEnumerable<IToken> tokens)
        {
            var preOutput = new List<IExpression>();

            foreach (var token in tokens)
            {
                var exp = factory.Create(token);
                if (exp == null)
                    throw new ParserException(Resource.ErrorWhileParsingTree);

                if (token is FunctionToken)
                {
                    var t = token as FunctionToken;

                    if (t.CountOfParams < exp.MinParameters)
                        throw new ParserException(Resource.LessParams);
                    if (exp.MaxParameters != -1 && t.CountOfParams > exp.MaxParameters)
                        throw new ParserException(Resource.MoreParams);

                    var diff = exp as DifferentParametersExpression;
                    if (diff != null)
                        diff.ParametersCount = t.CountOfParams;
                }

                preOutput.Add(exp);
            }

            return preOutput;
        }

        private IEnumerable<IToken> ConvertToReversePolishNotation(IEnumerable<IToken> tokens)
        {
            var output = new List<IToken>();
            var stack = new Stack<IToken>();

            var openBracketToken = new SymbolToken(Symbols.OpenBracket);
            var openBraceToken = new SymbolToken(Symbols.OpenBrace);
            foreach (var token in tokens)
            {
                IToken stackToken;
                if (token is SymbolToken)
                {
                    var t = token as SymbolToken;
                    if (t.Symbol == Symbols.OpenBracket || t.Symbol == Symbols.OpenBrace)
                    {
                        stack.Push(token);
                    }
                    else if (t.Symbol == Symbols.CloseBracket || t.Symbol == Symbols.CloseBrace)
                    {
                        stackToken = stack.Pop();
                        while (!stackToken.Equals(openBracketToken) && !stackToken.Equals(openBraceToken))
                        {
                            output.Add(stackToken);
                            stackToken = stack.Pop();
                        }
                    }
                    else if (t.Symbol == Symbols.Comma)
                    {
                        stackToken = stack.Pop();

                        while (!stackToken.Equals(openBracketToken) && !stackToken.Equals(openBraceToken))
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
                        if (stackToken.Equals(openBracketToken) || stackToken.Equals(openBraceToken))
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

        /// <summary>
        /// Gets or sets a value indicating whether saving of last expression.
        /// </summary>
        /// <value>
        ///   If <c>true</c> the parser saves last expression.
        /// </value>
        public bool SaveLastExpression
        {
            get
            {
                return saveLastExpression;
            }
            set
            {
                saveLastExpression = value;
            }
        }

    }

}
