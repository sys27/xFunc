// Copyright 2012-2020 Dmytro Kyshchenko
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
using System.Runtime.CompilerServices;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    public partial class Lexer
    {
        private IToken CreateOperatorToken(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            var endIndex = 0;
            while (endIndex < span.Length && IsOperatorSymbol(span[endIndex]))
                endIndex++;

            if (endIndex <= 0 || span.Length < endIndex)
                return null;

            var operatorMatch = span[..endIndex];
            IToken token;

            if (operatorMatch.Equals("+", StringComparison.Ordinal))
                token = OperatorToken.Plus;
            else if (operatorMatch.Equals("-", StringComparison.Ordinal) ||
                     operatorMatch.Equals("−", StringComparison.Ordinal))
                token = OperatorToken.Minus;
            else if (operatorMatch.Equals("*", StringComparison.Ordinal) ||
                     operatorMatch.Equals("×", StringComparison.Ordinal))
                token = OperatorToken.Multiplication;
            else if (operatorMatch.Equals("/", StringComparison.Ordinal))
                token = OperatorToken.Division;
            else if (operatorMatch.Equals("^", StringComparison.Ordinal))
                token = OperatorToken.Exponentiation;
            else if (operatorMatch.Equals("!", StringComparison.Ordinal))
                token = OperatorToken.Factorial;
            else if (operatorMatch.Equals("%", StringComparison.Ordinal))
                token = OperatorToken.Modulo;
            else if (operatorMatch.Equals(":=", StringComparison.Ordinal))
                token = OperatorToken.Assign;
            else if (operatorMatch.Equals("+=", StringComparison.Ordinal))
                token = OperatorToken.AddAssign;
            else if (operatorMatch.Equals("-=", StringComparison.Ordinal) ||
                     operatorMatch.Equals("−=", StringComparison.Ordinal))
                token = OperatorToken.SubAssign;
            else if (operatorMatch.Equals("*=", StringComparison.Ordinal) ||
                     operatorMatch.Equals("×=", StringComparison.Ordinal))
                token = OperatorToken.MulAssign;
            else if (operatorMatch.Equals("/=", StringComparison.Ordinal))
                token = OperatorToken.DivAssign;
            else if (operatorMatch.Equals("&&", StringComparison.Ordinal))
                token = OperatorToken.ConditionalAnd;
            else if (operatorMatch.Equals("||", StringComparison.Ordinal))
                token = OperatorToken.ConditionalOr;
            else if (operatorMatch.Equals("==", StringComparison.Ordinal))
                token = OperatorToken.Equal;
            else if (operatorMatch.Equals("!=", StringComparison.Ordinal))
                token = OperatorToken.NotEqual;
            else if (operatorMatch.Equals("<=", StringComparison.Ordinal))
                token = OperatorToken.LessOrEqual;
            else if (operatorMatch.Equals("<", StringComparison.Ordinal))
                token = OperatorToken.LessThan;
            else if (operatorMatch.Equals(">=", StringComparison.Ordinal))
                token = OperatorToken.GreaterOrEqual;
            else if (operatorMatch.Equals(">", StringComparison.Ordinal))
                token = OperatorToken.GreaterThan;
            else if (operatorMatch.Equals("++", StringComparison.Ordinal))
                token = OperatorToken.Increment;
            else if (operatorMatch.Equals("--", StringComparison.Ordinal) ||
                     operatorMatch.Equals("−−", StringComparison.Ordinal))
                token = OperatorToken.Decrement;
            else if (operatorMatch.Equals("~", StringComparison.Ordinal))
                token = OperatorToken.Not;
            else if (operatorMatch.Equals("&", StringComparison.Ordinal))
                token = OperatorToken.And;
            else if (operatorMatch.Equals("|", StringComparison.Ordinal))
                token = OperatorToken.Or;
            else if (operatorMatch.Equals("->", StringComparison.Ordinal) ||
                     operatorMatch.Equals("−>", StringComparison.Ordinal) ||
                     operatorMatch.Equals("=>", StringComparison.Ordinal))
                token = OperatorToken.Implication;
            else if (operatorMatch.Equals("<->", StringComparison.Ordinal) ||
                     operatorMatch.Equals("<−>", StringComparison.Ordinal) ||
                     operatorMatch.Equals("<=>", StringComparison.Ordinal))
                token = OperatorToken.Equality;
            else
                return null;

            function = function[endIndex..];

            return token;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsOperatorSymbol(char symbol) =>
            !char.IsLetterOrDigit(symbol) &&
            !char.IsWhiteSpace(symbol) &&
            !IsRestrictedSymbol(symbol);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsRestrictedSymbol(char symbol)
            => symbol == '(' ||
               symbol == ')' ||
               symbol == '{' ||
               symbol == '}' ||
               symbol == ',' ||
               symbol == '°' ||
               symbol == '∠';
    }
}