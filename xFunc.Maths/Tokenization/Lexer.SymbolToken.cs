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

using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths.Tokenization
{
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

            Current = new Token(symbol);
            return true;
        }
    }
}