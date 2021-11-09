// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths.Tokenization;

/// <summary>
/// The lexer for mathematical expressions.
/// </summary>
internal ref partial struct Lexer
{
    private bool CreateSymbol()
    {
        var symbol = function[0] switch
        {
            '(' => OpenParenthesisSymbol,
            ')' => CloseParenthesisSymbol,
            '{' => OpenBraceSymbol,
            '}' => CloseBraceSymbol,
            ',' => CommaSymbol,
            '∠' => AngleSymbol,
            '°' => DegreeSymbol,
            ':' => ColonSymbol,
            '?' => QuestionMarkSymbol,
            _ => Empty,
        };

        if (symbol == Empty)
            return false;

        function = function[1..];

        Current = Token.Create(symbol);
        return true;
    }
}