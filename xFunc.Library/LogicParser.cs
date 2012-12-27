using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Library.Exceptions;
using xFunc.Library.Expressions.Logics;
using xFunc.Library.Resources;

namespace xFunc.Library
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
            if (function != lastFunc)
            {
                IEnumerable<Token> tokens = lexer.LogicTokenization(function);
                IEnumerable<Token> rpn = ConvertToReversePolishNotation(tokens);
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
                    else
                    {
                        throw new ParserException(Resource.UnexpectedError);
                    }
                }

                lastFunc = function;
                logicExpression = stack.Pop();
            }

            return logicExpression;
        }

        public LogicParameterCollection GetLogicParameters(string function)
        {
            HashSet<char> c = new HashSet<char>(from t in lexer.LogicTokenization(function).AsParallel()
                                                where t.Type == TokenType.Variable
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

        private IEnumerable<Token> ConvertToReversePolishNotation(IEnumerable<Token> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException("tokens");

            List<Token> output = new List<Token>();
            Stack<Token> stack = new Stack<Token>();

            foreach (var token in tokens)
            {
                Token stackToken;
                if (token.Type == TokenType.OpenBracket)
                {
                    stack.Push(token);
                }
                else if (token.Type == TokenType.CloseBracket)
                {
                    stackToken = stack.Pop();
                    while (stackToken.Type != TokenType.OpenBracket)
                    {
                        output.Add(stackToken);
                        stackToken = stack.Pop();
                    }
                }
                else if (token.Type == TokenType.Not ||
                         token.Type == TokenType.And ||
                         token.Type == TokenType.Or ||
                         token.Type == TokenType.Implication ||
                         token.Type == TokenType.Equality ||
                         token.Type == TokenType.NOr ||
                         token.Type == TokenType.NAnd ||
                         token.Type == TokenType.XOr ||
                         token.Type == TokenType.TruthTable ||
                         token.Type == TokenType.True ||
                         token.Type == TokenType.False)
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Type >= token.Type)
                    {
                        if (stackToken.Type == TokenType.OpenBracket)
                            break;
                        output.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
                else if (token.Type == TokenType.Variable)
                {
                    output.Add(token);
                }
                else
                {
                    throw new ParserException(Resource.NotSupportedToken);
                }
            }
            if (stack.Count != 0)
            {
                output.AddRange(stack);
            }

            return output;
        }

        private IEnumerable<ILogicExpression> ConvertTokensToExpression(IEnumerable<Token> tokens)
        {
            List<ILogicExpression> preOutput = new List<ILogicExpression>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Not:
                        preOutput.Add(new NotLogicExpression());
                        break;
                    case TokenType.And:
                        preOutput.Add(new AndLogicExpression());
                        break;
                    case TokenType.Or:
                        preOutput.Add(new OrLogicExpression());
                        break;
                    case TokenType.Implication:
                        preOutput.Add(new ImplicationLogicExpression());
                        break;
                    case TokenType.Equality:
                        preOutput.Add(new EqualityLogicExpression());
                        break;
                    case TokenType.NAnd:
                        preOutput.Add(new NAndLogicExpression());
                        break;
                    case TokenType.NOr:
                        preOutput.Add(new NOrLogicExpression());
                        break;
                    case TokenType.XOr:
                        preOutput.Add(new XOrLogicExpression());
                        break;
                    case TokenType.TruthTable:
                        preOutput.Add(new TruthTableExpression());
                        break;
                    case TokenType.True:
                        preOutput.Add(new ConstLogicExpression(true));
                        break;
                    case TokenType.False:
                        preOutput.Add(new ConstLogicExpression(false));
                        break;
                    case TokenType.Variable:
                        preOutput.Add(new VariableLogicExpression(token.Variable));
                        break;
                    default:
                        throw new ParserException(Resource.NotSupportedToken);
                }
            }

            return preOutput;
        }

    }

}
