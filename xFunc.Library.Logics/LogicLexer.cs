using System;
using System.Collections.Generic;
using xFunc.Library.Logics.Exceptions;
using xFunc.Library.Logics.Resources;

namespace xFunc.Library.Logics
{

    public class LogicLexer
    {

        public IEnumerable<LogicToken> LogicTokenization(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentException(Resource.NotSpecifiedFunction, "function");

            function = function.ToLower().Replace(" ", "");
            List<LogicToken> tokens = new List<LogicToken>();

            for (int i = 0; i < function.Length; i++)
            {
                char letter = function[i];
                LogicToken token = new LogicToken();
                if (letter == '(')
                {
                    token.Type = LogicTokenType.OpenBracket;
                }
                else if (letter == ')')
                {
                    token.Type = LogicTokenType.CloseBracket;
                }
                else if (letter == '!')
                {
                    token.Type = LogicTokenType.Not;
                }
                else if (letter == '&')
                {
                    token.Type = LogicTokenType.And;
                }
                else if (letter == '|')
                {
                    token.Type = LogicTokenType.Or;
                }
                else if (letter == '-' || letter == '=')
                {
                    if (i + 1 <= function.Length && function[i + 1] == '>')
                    {
                        token.Type = LogicTokenType.Implication;
                        i++;
                    }
                    else
                    {
                        throw new LogicLexerException(string.Format(Resource.NotSupportedSymbol, letter));
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
                                token.Type = LogicTokenType.Equality;
                                i += 2;
                            }
                        }
                        else
                        {
                            throw new LogicLexerException(string.Format(Resource.NotSupportedSymbol, letter));
                        }
                    }
                    else
                    {
                        throw new LogicLexerException(Resource.InvalidExpression);
                    }
                }
                else if (letter == '^')
                {
                    token.Type = LogicTokenType.XOr;
                }
                else if (char.IsLetter(letter))
                {
                    string sub = function.Substring(i);
                    if (sub.StartsWith("true"))
                    {
                        token.Type = LogicTokenType.True;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("false"))
                    {
                        token.Type = LogicTokenType.False;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("not"))
                    {
                        token.Type = LogicTokenType.Not;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("or"))
                    {
                        token.Type = LogicTokenType.Or;
                        tokens.Add(token);
                        i++;
                        continue;
                    }
                    if (sub.StartsWith("and"))
                    {
                        token.Type = LogicTokenType.And;
                        tokens.Add(token);
                        i += 2;
                        continue;
                    }
                    if (sub.StartsWith("impl"))
                    {
                        token.Type = LogicTokenType.Implication;
                        tokens.Add(token);
                        i += 3;
                        continue;
                    }
                    if (sub.StartsWith("eq"))
                    {
                        token.Type = LogicTokenType.Equality;
                        tokens.Add(token);
                        i++;
                        continue;
                    }
                    if (sub.StartsWith("nor"))
                    {
                        token.Type = LogicTokenType.NOr;
                        tokens.Add(token);
                        i += 2;
                        continue;
                    }
                    if (sub.StartsWith("nand"))
                    {
                        token.Type = LogicTokenType.NAnd;
                        tokens.Add(token);
                        i += 3;
                        continue;
                    }
                    if (sub.StartsWith("xor"))
                    {
                        token.Type = LogicTokenType.XOr;
                        tokens.Add(token);
                        i += 2;
                        continue;
                    }
                    if (sub.StartsWith("table"))
                    {
                        token.Type = LogicTokenType.TruthTable;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }

                    if (letter == 't')
                    {
                        token.Type = LogicTokenType.True;
                        tokens.Add(token);

                        continue;
                    }
                    if (letter == 'f')
                    {
                        token.Type = LogicTokenType.False;
                        tokens.Add(token);

                        continue;
                    }

                    token.Type = LogicTokenType.Variable;
                    token.Variable = letter;
                }
                else
                {
                    throw new LogicLexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                tokens.Add(token);
            }

            return tokens;
        }

    }

}
