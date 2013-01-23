// Copyright 2012 Dmitry Kischenko
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
using System.Linq;
using xFunc.Logics.Exceptions;
using xFunc.Logics.Expressions;
using xFunc.Logics.Resources;

namespace xFunc.Logics
{

    public class LogicParser
    {

        private LogicLexer lexer;

        private string lastFunc = string.Empty;
        private ILogicExpression logicExpression;

        public LogicParser()
        {
            lexer = new LogicLexer();
        }

        public ILogicExpression Parse(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException("function");

            if (function != lastFunc)
            {
                IEnumerable<LogicToken> tokens = lexer.Tokenization(function);
                IEnumerable<LogicToken> rpn = ConvertToReversePolishNotation(tokens);
                IEnumerable<ILogicExpression> expressions = ConvertTokensToExpression(rpn);

                Stack<ILogicExpression> stack = new Stack<ILogicExpression>();
                foreach (var expression in expressions)
                {
                    if (expression is VariableLogicExpression || expression is ConstLogicExpression)
                    {
                        stack.Push(expression);
                    }
                    else if (expression is UnaryLogicExpression)
                    {
                        UnaryLogicExpression unaryLogicExp = (UnaryLogicExpression)expression;
                        unaryLogicExp.FirstMathExpression = stack.Pop();

                        stack.Push(unaryLogicExp);
                    }
                    else if (expression is BinaryLogicExpression)
                    {
                        BinaryLogicExpression binLoginExp = (BinaryLogicExpression)expression;
                        binLoginExp.SecondOperand = stack.Pop();
                        binLoginExp.FirstOperand = stack.Pop();

                        stack.Push(binLoginExp);
                    }
                    else if (expression is AssignLogicExpression)
                    {
                        AssignLogicExpression assign = (AssignLogicExpression)expression;
                        assign.Value = stack.Pop();

                        if (!(stack.Peek() is VariableLogicExpression))
                            throw new LogicParserException(Resource.InvalidExpression);

                        assign.Variable = (VariableLogicExpression)stack.Pop();

                        stack.Push(assign);
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
            HashSet<char> c = new HashSet<char>(from t in lexer.Tokenization(function).AsParallel()
                                                where t.Type == LogicTokenType.Variable
                                                orderby t.Variable
                                                select t.Variable);

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
                UnaryLogicExpression un = (UnaryLogicExpression)expression;
                ConvertToColletion(un.FirstMathExpression, collection);
            }
            else if (expression is BinaryLogicExpression)
            {
                BinaryLogicExpression bin = (BinaryLogicExpression)expression;
                ConvertToColletion(bin.FirstOperand, collection);
                ConvertToColletion(bin.SecondOperand, collection);
            }
            else if (expression is VariableLogicExpression)
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
                else if (token.Type == LogicTokenType.Not ||
                         token.Type == LogicTokenType.And ||
                         token.Type == LogicTokenType.Or ||
                         token.Type == LogicTokenType.Implication ||
                         token.Type == LogicTokenType.Equality ||
                         token.Type == LogicTokenType.NOr ||
                         token.Type == LogicTokenType.NAnd ||
                         token.Type == LogicTokenType.XOr ||
                         token.Type == LogicTokenType.Assign ||
                         token.Type == LogicTokenType.TruthTable ||
                         token.Type == LogicTokenType.True ||
                         token.Type == LogicTokenType.False)
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Type >= token.Type)
                    {
                        if (stackToken.Type == LogicTokenType.OpenBracket)
                            break;
                        output.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
                else if (token.Type == LogicTokenType.Variable)
                {
                    output.Add(token);
                }
                else
                {
                    throw new LogicParserException(Resource.NotSupportedToken);
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
                        preOutput.Add(new NotLogicExpression());
                        break;
                    case LogicTokenType.And:
                        preOutput.Add(new AndLogicExpression());
                        break;
                    case LogicTokenType.Or:
                        preOutput.Add(new OrLogicExpression());
                        break;
                    case LogicTokenType.Implication:
                        preOutput.Add(new ImplicationLogicExpression());
                        break;
                    case LogicTokenType.Equality:
                        preOutput.Add(new EqualityLogicExpression());
                        break;
                    case LogicTokenType.NAnd:
                        preOutput.Add(new NAndLogicExpression());
                        break;
                    case LogicTokenType.NOr:
                        preOutput.Add(new NOrLogicExpression());
                        break;
                    case LogicTokenType.XOr:
                        preOutput.Add(new XOrLogicExpression());
                        break;
                    case LogicTokenType.Assign:
                        preOutput.Add(new AssignLogicExpression());
                        break;
                    case LogicTokenType.TruthTable:
                        preOutput.Add(new TruthTableExpression());
                        break;
                    case LogicTokenType.True:
                        preOutput.Add(new ConstLogicExpression(true));
                        break;
                    case LogicTokenType.False:
                        preOutput.Add(new ConstLogicExpression(false));
                        break;
                    case LogicTokenType.Variable:
                        preOutput.Add(new VariableLogicExpression(token.Variable));
                        break;
                    default:
                        throw new LogicParserException(Resource.NotSupportedToken);
                }
            }

            return preOutput;
        }

    }

}
