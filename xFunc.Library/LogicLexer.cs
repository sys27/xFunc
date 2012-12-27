using System;
using System.Collections.Generic;
using xFunc.Library.Exceptions;
using xFunc.Library.Resources;

namespace xFunc.Library
{

    public class LogicLexer
    {

        public IEnumerable<Token> LogicTokenization(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentException(Resource.NotSpecifiedFuncation, "function");

            function = function.ToLower().Replace(" ", "");
            List<Token> tokens = new List<Token>();

            for (int i = 0; i < function.Length; i++)
            {
                char letter = function[i];
                Token token = new Token();
                if (letter == '(')
                {
                    token.Type = TokenType.OpenBracket;
                }
                else if (letter == ')')
                {
                    token.Type = TokenType.CloseBracket;
                }
                else if (letter == '!')
                {
                    token.Type = TokenType.Not;
                }
                else if (letter == '&')
                {
                    token.Type = TokenType.And;
                }
                else if (letter == '|')
                {
                    token.Type = TokenType.Or;
                }
                else if (letter == '-' || letter == '=')
                {
                    if (i + 1 <= function.Length && function[i + 1] == '>')
                    {
                        token.Type = TokenType.Implication;
                        i++;
                    }
                    else
                    {
                        throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                    }
                }
                else if (letter == '<')
                {
                    if (i + 1 <= function.Length)
                    {
                        if (function[i + 1] == '=' || function[i + 1] == '-')
                        {
                            if (i + 2 <= function.Length && function[i + 2] == '>')
                            {
                                token.Type = TokenType.Equality;
                                i += 2;
                            }
                        }
                        else
                        {
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                        }
                    }
                    else
                    {
                        throw new LexerException(Resource.InvalidExpression);
                    }
                }
                else if (letter == '^')
                {
                    token.Type = TokenType.XOr;
                }
                else if (char.IsLetter(letter))
                {
                    string sub = function.Substring(i);
                    if (sub.StartsWith("true"))
                    {
                        token.Type = TokenType.True;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("false"))
                    {
                        token.Type = TokenType.False;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("not"))
                    {
                        token.Type = TokenType.Not;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("or"))
                    {
                        token.Type = TokenType.Or;
                        tokens.Add(token);
                        i++;
                        continue;
                    }
                    if (sub.StartsWith("and"))
                    {
                        token.Type = TokenType.And;
                        tokens.Add(token);
                        i += 2;
                        continue;
                    }
                    if (sub.StartsWith("impl"))
                    {
                        token.Type = TokenType.Implication;
                        tokens.Add(token);
                        i += 3;
                        continue;
                    }
                    if (sub.StartsWith("eq"))
                    {
                        token.Type = TokenType.Equality;
                        tokens.Add(token);
                        i++;
                        continue;
                    }
                    if (sub.StartsWith("nor"))
                    {
                        token.Type = TokenType.NOr;
                        tokens.Add(token);
                        i += 2;
                        continue;
                    }
                    if (sub.StartsWith("nand"))
                    {
                        token.Type = TokenType.NAnd;
                        tokens.Add(token);
                        i += 3;
                        continue;
                    }
                    if (sub.StartsWith("xor"))
                    {
                        token.Type = TokenType.XOr;
                        tokens.Add(token);
                        i += 2;
                        continue;
                    }
                    if (sub.StartsWith("table"))
                    {
                        token.Type = TokenType.TruthTable;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }

                    if (letter == 't')
                    {
                        token.Type = TokenType.True;
                        tokens.Add(token);

                        continue;
                    }
                    if (letter == 'f')
                    {
                        token.Type = TokenType.False;
                        tokens.Add(token);

                        continue;
                    }

                    token.Type = TokenType.Variable;
                    token.Variable = letter;
                }
                else
                {
                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                tokens.Add(token);
            }

            return tokens;
        }

    }

}
