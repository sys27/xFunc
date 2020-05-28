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
            var first = span[0];
            var second = span.Length >= 2 ? span[1] : default;
            var third = span.Length >= 3 ? span[2] : default;

            var (token, size) = (first, second, third) switch
            {
                ('<', '-', '>') => (Equality, 3),
                ('<', '−', '>') => (Equality, 3),
                ('<', '=', '>') => (Equality, 3),

                (':', '=', _) => (Assign, 2),
                ('+', '=', _) => (AddAssign, 2),
                ('-', '=', _) => (SubAssign, 2),
                ('−', '=', _) => (SubAssign, 2),
                ('*', '=', _) => (MulAssign, 2),
                ('×', '=', _) => (MulAssign, 2),
                ('/', '=', _) => (DivAssign, 2),
                ('&', '&', _) => (ConditionalAnd, 2),
                ('|', '|', _) => (ConditionalOr, 2),
                ('=', '=', _) => (Equal, 2),
                ('!', '=', _) => (NotEqual, 2),
                ('<', '=', _) => (LessOrEqual, 2),
                ('>', '=', _) => (GreaterOrEqual, 2),
                ('+', '+', _) => (Increment, 2),
                ('-', '-', _) => (Decrement, 2),
                ('−', '−', _) => (Decrement, 2),
                ('-', '>', _) => (Implication, 2),
                ('−', '>', _) => (Implication, 2),
                ('=', '>', _) => (Implication, 2),

                ('+', _, _) => (Plus, 1),
                ('-', _, _) => (Minus, 1),
                ('−', _, _) => (Minus, 1),
                ('*', _, _) => (Multiplication, 1),
                ('×', _, _) => (Multiplication, 1),
                ('/', _, _) => (Division, 1),
                ('^', _, _) => (Exponentiation, 1),
                ('!', _, _) => (Factorial, 1),
                ('%', _, _) => (Modulo, 1),
                ('<', _, _) => (LessThan, 1),
                ('>', _, _) => (GreaterThan, 1),
                ('~', _, _) => (Not, 1),
                ('&', _, _) => (And, 1),
                ('|', _, _) => (Or, 1),

                _ => (null, 0),
            };

            function = function[size..];

            return token;
        }
    }
}