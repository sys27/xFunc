// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths.Tokenization;

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
                ('=', '>', _) => (LambdaOperator, 2),
            ('<', '<', _) => (LeftShiftOperator, 2),
            ('>', '>', _) => (RightShiftOperator, 2),
            ('/', '/', _) => (RationalOperator, 2),

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