// Copyright 2012-2021 Dmytro Kyshchenko
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

using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    internal ref partial struct Lexer
    {
        private bool CreateOperatorToken()
        {
            var first = function[0];
            var second = function.Length >= 2 ? function[1] : default;
            var third = function.Length >= 3 ? function[2] : default;

            var (kind, size) = (first, second, third) switch
            {
                ('<', '-', '>') or
                ('<', '−', '>') or
                ('<', '=', '>') => (EqualityOperator, 3),
                ('<', '<', '=') => (LeftShiftAssignOperator, 3),
                ('>', '>', '=') => (RightShiftAssignOperator, 3),

                (':', '=', _) => (AssignOperator, 2),
                ('+', '=', _) => (AddAssignOperator, 2),
                ('-', '=', _) or
                ('−', '=', _) => (SubAssignOperator, 2),
                ('*', '=', _) or
                ('×', '=', _) => (MulAssignOperator, 2),
                ('/', '=', _) => (DivAssignOperator, 2),
                ('&', '&', _) => (ConditionalAndOperator, 2),
                ('|', '|', _) => (ConditionalOrOperator, 2),
                ('=', '=', _) => (EqualOperator, 2),
                ('!', '=', _) => (NotEqualOperator, 2),
                ('<', '=', _) => (LessOrEqualOperator, 2),
                ('>', '=', _) => (GreaterOrEqualOperator, 2),
                ('+', '+', _) => (IncrementOperator, 2),
                ('-', '-', _) or
                ('−', '−', _) => (DecrementOperator, 2),
                ('-', '>', _) or
                ('−', '>', _) or
                ('=', '>', _) => (ImplicationOperator, 2),
                ('<', '<', _) => (LeftShiftOperator, 2),
                ('>', '>', _) => (RightShiftOperator, 2),

                ('+', _, _) => (PlusOperator, 1),
                ('-', _, _) or
                ('−', _, _) => (MinusOperator, 1),
                ('*', _, _) or
                ('×', _, _) => (MultiplicationOperator, 1),
                ('/', _, _) => (DivisionOperator, 1),
                ('^', _, _) => (ExponentiationOperator, 1),
                ('!', _, _) => (FactorialOperator, 1),
                ('%', _, _) => (ModuloOperator, 1),
                ('<', _, _) => (LessThanOperator, 1),
                ('>', _, _) => (GreaterThanOperator, 1),
                ('~', _, _) => (NotOperator, 1),
                ('&', _, _) => (AndOperator, 1),
                ('|', _, _) => (OrOperator, 1),

                _ => (Empty, 0),
            };

            if (kind == Empty)
                return false;

            function = function[size..];

            Current = Token.Create(kind);
            return true;
        }
    }
}