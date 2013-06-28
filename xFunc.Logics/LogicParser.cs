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
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
using xFunc.Logics.Expressions;
using xFunc.Logics.Resources;

namespace xFunc.Logics
{

    public class LogicParser
    {

        private ILexer lexer;

        private string lastFunc = string.Empty;
        private ILogicExpression logicExpression;

        public LogicParser()
            : this(new LogicLexer())
        {

        }

        public LogicParser(ILexer lexer)
        {
            this.lexer = lexer;
        }

        public ILogicExpression Parse(string function)
        {
#if NET40_OR_GREATER || PORTABLE
            if (string.IsNullOrWhiteSpace(function))
#elif NET20 || NET30 || NET35
            if (StringExtention.IsNullOrWhiteSpace(function))
#endif
                throw new ArgumentNullException("function");

            if (function != lastFunc)
            {
                IEnumerable<LogicToken> tokens = lexer.Tokenize(function);
                IEnumerable<LogicToken> rpn = ConvertToReversePolishNotation(tokens);
                IEnumerable<ILogicExpression> expressions = ConvertTokensToExpression(rpn);

                Stack<ILogicExpression> stack = new Stack<ILogicExpression>();
                foreach (var expression in expressions)
                {
                    if (expression is Variable || expression is Const)
                    {
                        stack.Push(expression);
                    }
                    else if (expression is UnaryLogicExpression)
                    {
                        UnaryLogicExpression unaryLogicExp = expression as UnaryLogicExpression;
                        unaryLogicExp.FirstMathExpression = stack.Pop();

                        stack.Push(unaryLogicExp);
                    }
                    else if (expression is BinaryLogicExpression)
                    {
                        BinaryLogicExpression binLoginExp = expression as BinaryLogicExpression;
                        binLoginExp.SecondOperand = stack.Pop();
                        binLoginExp.FirstOperand = stack.Pop();

                        stack.Push(binLoginExp);
                    }
                    else if (expression is Assign)
                    {
                        Assign assign = expression as Assign;
                        assign.Value = stack.Pop();

                        if (!(stack.Peek() is Variable))
                            throw new LogicParserException(Resource.InvalidExpression);

                        assign.Variable = (Variable)stack.Pop();

                        stack.Push(assign);
                    }
                    else if (expression is Undefine)
                    {
                        Undefine undef = expression as Undefine;

                        if (!(stack.Peek() is Variable))
                            throw new LogicParserException(Resource.InvalidExpression);

                        undef.Variable = (Variable)stack.Pop();

                        stack.Push(undef);
                    }
                    else
                    {
                        throw new LogicParserException(Resource.UnexpectedError);
                    }
                }

                lastFunc = function;
                logicExpression = stack.Pop();
            }

            return logicExpression;
        }

        public LogicParameterCollection GetLogicParameters(string function)
        {
#if NET40_OR_GREATER
            HashSet<string> c = new HashSet<string>(from t in lexer.Tokenize(function).AsParallel()
                                                    where t.Type == LogicTokenType.Variable
                                                    orderby t.Variable
                                                    select t.Variable);
#elif NET35 || PORTABLE
            HashSet<string> c = new HashSet<string>(from t in lexer.Tokenize(function)
                                                    where t.Type == LogicTokenType.Variable
                                                    orderby t.Variable
                                                    select t.Variable);
#elif NET20 || NET30
            var tokens = lexer.Tokenize(function);
            List<string> c = new List<string>();

            foreach (var token in tokens)
                if (token.Type == LogicTokenType.Variable && !c.Contains(token.Variable))
                    c.Add(token.Variable);
            c.Sort(StringComparer.InvariantCulture);
#endif

            return new LogicParameterCollection(c);
        }

        public IEnumerable<ILogicExpression> ConvertLogicExpressionToCollection(ILogicExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            List<ILogicExpression> collection = new List<ILogicExpression>();
            ConvertToColletion(expression, collection);

            return collection;
        }

        private void ConvertToColletion(ILogicExpression expression, List<ILogicExpression> collection)
        {
            if (expression is UnaryLogicExpression)
            {
                UnaryLogicExpression un = expression as UnaryLogicExpression;
                ConvertToColletion(un.FirstMathExpression, collection);
            }
            else if (expression is BinaryLogicExpression)
            {
                BinaryLogicExpression bin = expression as BinaryLogicExpression;
                ConvertToColletion(bin.FirstOperand, collection);
                ConvertToColletion(bin.SecondOperand, collection);
            }
            else if (expression is Variable)
            {
                return;
            }

            collection.Add(expression);
        }

        private IEnumerable<LogicToken> ConvertToReversePolishNotation(IEnumerable<LogicToken> tokens)
        {
            List<LogicToken> output = new List<LogicToken>();
            Stack<LogicToken> stack = new Stack<LogicToken>();

            foreach (var token in tokens)
            {
                LogicToken stackToken;
                if (token.Type == LogicTokenType.OpenBracket)
                {
                    stack.Push(token);
                }
                else if (token.Type == LogicTokenType.CloseBracket)
                {
                    stackToken = stack.Pop();
                    while (stackToken.Type != LogicTokenType.OpenBracket)
                    {
                        output.Add(stackToken);
                        stackToken = stack.Pop();
                    }
                }
                else if (token.Type == LogicTokenType.Variable)
                {
                    output.Add(token);
                }
                else
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Type >= token.Type)
                    {
                        if (stackToken.Type == LogicTokenType.OpenBracket)
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

        private IEnumerable<ILogicExpression> ConvertTokensToExpression(IEnumerable<LogicToken> tokens)
        {
            List<ILogicExpression> preOutput = new List<ILogicExpression>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case LogicTokenType.Not:
                        preOutput.Add(new Not());
                        break;
                    case LogicTokenType.And:
                        preOutput.Add(new And());
                        break;
                    case LogicTokenType.Or:
                        preOutput.Add(new Or());
                        break;
                    case LogicTokenType.Implication:
                        preOutput.Add(new Implication());
                        break;
                    case LogicTokenType.Equality:
                        preOutput.Add(new Equality());
                        break;
                    case LogicTokenType.NAnd:
                        preOutput.Add(new NAnd());
                        break;
                    case LogicTokenType.NOr:
                        preOutput.Add(new NOr());
                        break;
                    case LogicTokenType.XOr:
                        preOutput.Add(new XOr());
                        break;
                    case LogicTokenType.Assign:
                        preOutput.Add(new Assign());
                        break;
                    case LogicTokenType.Undefine:
                        preOutput.Add(new Undefine());
                        break;
                    case LogicTokenType.TruthTable:
                        preOutput.Add(new TruthTable());
                        break;
                    case LogicTokenType.True:
                        preOutput.Add(new Const(true));
                        break;
                    case LogicTokenType.False:
                        preOutput.Add(new Const(false));
                        break;
                    case LogicTokenType.Variable:
                        preOutput.Add(new Variable(token.Variable));
                        break;
                    default:
                        throw new LogicParserException(Resource.NotSupportedToken);
                }
            }

            return preOutput;
        }

    }

}
