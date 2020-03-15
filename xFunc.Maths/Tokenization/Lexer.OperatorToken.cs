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
using static xFunc.Maths.Tokenization.Tokens.OperatorToken;

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

            if (Compare(operatorMatch, "+"))
                token = Plus;
            else if (Compare(operatorMatch, "-") ||
                     Compare(operatorMatch, "−"))
                token = Minus;
            else if (Compare(operatorMatch, "*") ||
                     Compare(operatorMatch, "×"))
                token = Multiplication;
            else if (Compare(operatorMatch, "/"))
                token = Division;
            else if (Compare(operatorMatch, "^"))
                token = Exponentiation;
            else if (Compare(operatorMatch, "!"))
                token = Factorial;
            else if (Compare(operatorMatch, "%"))
                token = Modulo;
            else if (Compare(operatorMatch, ":="))
                token = Assign;
            else if (Compare(operatorMatch, "+="))
                token = AddAssign;
            else if (Compare(operatorMatch, "-=") ||
                     Compare(operatorMatch, "−="))
                token = SubAssign;
            else if (Compare(operatorMatch, "*=") ||
                     Compare(operatorMatch, "×="))
                token = MulAssign;
            else if (Compare(operatorMatch, "/="))
                token = DivAssign;
            else if (Compare(operatorMatch, "&&"))
                token = ConditionalAnd;
            else if (Compare(operatorMatch, "||"))
                token = ConditionalOr;
            else if (Compare(operatorMatch, "=="))
                token = Equal;
            else if (Compare(operatorMatch, "!="))
                token = NotEqual;
            else if (Compare(operatorMatch, "<="))
                token = LessOrEqual;
            else if (Compare(operatorMatch, "<"))
                token = LessThan;
            else if (Compare(operatorMatch, ">="))
                token = GreaterOrEqual;
            else if (Compare(operatorMatch, ">"))
                token = GreaterThan;
            else if (Compare(operatorMatch, "++"))
                token = Increment;
            else if (Compare(operatorMatch, "--") ||
                     Compare(operatorMatch, "−−"))
                token = Decrement;
            else if (Compare(operatorMatch, "~"))
                token = Not;
            else if (Compare(operatorMatch, "&"))
                token = And;
            else if (Compare(operatorMatch, "|"))
                token = Or;
            else if (Compare(operatorMatch, "->") ||
                     Compare(operatorMatch, "−>") ||
                     Compare(operatorMatch, "=>"))
                token = Implication;
            else if (Compare(operatorMatch, "<->") ||
                     Compare(operatorMatch, "<−>") ||
                     Compare(operatorMatch, "<=>"))
                token = Equality;
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