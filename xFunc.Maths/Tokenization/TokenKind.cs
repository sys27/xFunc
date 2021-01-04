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

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// Represents a token kind.
    /// </summary>
    internal enum TokenKind
    {
        /// <summary>
        /// Empty token.
        /// </summary>
        Empty,

        /// <summary>
        /// Id.
        /// </summary>
        Id,

        /// <summary>
        /// Number.
        /// </summary>
        Number,

        #region Symbols

        /// <summary>
        /// The '(' token.
        /// </summary>
        OpenParenthesisSymbol,

        /// <summary>
        /// The ')' token.
        /// </summary>
        CloseParenthesisSymbol,

        /// <summary>
        /// The '{' token.
        /// </summary>
        OpenBraceSymbol,

        /// <summary>
        /// The '}' token.
        /// </summary>
        CloseBraceSymbol,

        /// <summary>
        /// The ',' token.
        /// </summary>
        CommaSymbol,

        /// <summary>
        /// The '∠' token.
        /// </summary>
        AngleSymbol,

        /// <summary>
        /// The '°' token.
        /// </summary>
        DegreeSymbol,

        /// <summary>
        /// The ':' token.
        /// </summary>
        ColonSymbol,

        /// <summary>
        /// The '?' token.
        /// </summary>
        QuestionMarkSymbol,

        #endregion Symbols

        #region Keywords

        /// <summary>
        /// The 'true' token.
        /// </summary>
        TrueKeyword,

        /// <summary>
        /// The 'false' token.
        /// </summary>
        FalseKeyword,

        /// <summary>
        /// The 'def, define' token.
        /// </summary>
        DefineKeyword,

        /// <summary>
        /// The 'undef, undefine' token.
        /// </summary>
        UndefineKeyword,

        /// <summary>
        /// The 'if' token.
        /// </summary>
        IfKeyword,

        /// <summary>
        /// The 'for' token.
        /// </summary>
        ForKeyword,

        /// <summary>
        /// The 'while' token.
        /// </summary>
        WhileKeyword,

        /// <summary>
        /// The 'nand' token.
        /// </summary>
        NAndKeyword,

        /// <summary>
        /// The 'nor' token.
        /// </summary>
        NOrKeyword,

        /// <summary>
        /// The 'and' token.
        /// </summary>
        AndKeyword,

        /// <summary>
        /// The 'Or' token.
        /// </summary>
        OrKeyword,

        /// <summary>
        /// The 'xor' token.
        /// </summary>
        XOrKeyword,

        /// <summary>
        /// The 'not' token.
        /// </summary>
        NotKeyword,

        /// <summary>
        /// The 'eq' token.
        /// </summary>
        EqKeyword,

        /// <summary>
        /// The 'impl' token.
        /// </summary>
        ImplKeyword,

        /// <summary>
        /// The 'mod' token.
        /// </summary>
        ModKeyword,

        /// <summary>
        /// The 'deg' token.
        /// </summary>
        DegreeKeyword,

        /// <summary>
        /// The 'rad' token.
        /// </summary>
        RadianKeyword,

        /// <summary>
        /// The 'grad' token.
        /// </summary>
        GradianKeyword,

        #endregion Keywords

        #region Operators

        /// <summary>
        /// The '+' token.
        /// </summary>
        PlusOperator,

        /// <summary>
        /// The '-' token.
        /// </summary>
        MinusOperator,

        /// <summary>
        /// The '*' token.
        /// </summary>
        MultiplicationOperator,

        /// <summary>
        /// The '/' token.
        /// </summary>
        DivisionOperator,

        /// <summary>
        /// The '^' token.
        /// </summary>
        ExponentiationOperator,

        /// <summary>
        /// The '!' token.
        /// </summary>
        FactorialOperator,

        /// <summary>
        /// The '%, mod' token.
        /// </summary>
        ModuloOperator,

        /// <summary>
        /// The '&amp;&amp;' token.
        /// </summary>
        ConditionalAndOperator,

        /// <summary>
        /// The '||' token.
        /// </summary>
        ConditionalOrOperator,

        /// <summary>
        /// The '==' token.
        /// </summary>
        EqualOperator,

        /// <summary>
        /// The '!=' token.
        /// </summary>
        NotEqualOperator,

        /// <summary>
        /// The '&lt;' token.
        /// </summary>
        LessThanOperator,

        /// <summary>
        /// The '&lt;=' token.
        /// </summary>
        LessOrEqualOperator,

        /// <summary>
        /// The '&gt;' token.
        /// </summary>
        GreaterThanOperator,

        /// <summary>
        /// The '&gt;=' token.
        /// </summary>
        GreaterOrEqualOperator,

        /// <summary>
        /// The 'The increment (++)' token.
        /// </summary>
        IncrementOperator,

        /// <summary>
        /// The 'The decrement (--)' token.
        /// </summary>
        DecrementOperator,

        /// <summary>
        /// The '+=' token.
        /// </summary>
        AddAssignOperator,

        /// <summary>
        /// The '-=' token.
        /// </summary>
        SubAssignOperator,

        /// <summary>
        /// The '*=' token.
        /// </summary>
        MulAssignOperator,

        /// <summary>
        /// The '/=' token.
        /// </summary>
        DivAssignOperator,

        /// <summary>
        /// The ':=' token.
        /// </summary>
        AssignOperator,

        /// <summary>
        /// The '~, not' token.
        /// </summary>
        NotOperator,

        /// <summary>
        /// The '&amp;, and' token.
        /// </summary>
        AndOperator,

        /// <summary>
        /// The '|, or' token.
        /// </summary>
        OrOperator,

        /// <summary>
        /// The '=>, ->, impl' token.
        /// </summary>
        ImplicationOperator,

        /// <summary>
        /// The '&lt;=>, &lt;->, eq' token.
        /// </summary>
        EqualityOperator,

        /// <summary>
        /// The '&lt;&lt;' token.
        /// </summary>
        LeftShiftOperator,

        /// <summary>
        /// The '&gt;&gt;' token.
        /// </summary>
        RightShiftOperator,

        /// <summary>
        /// The '&lt;&lt;=' token.
        /// </summary>
        LeftShiftAssignOperator,

        /// <summary>
        /// The '&gt;&gt;=' token.
        /// </summary>
        RightShiftAssignOperator,

        #endregion Operators
    }
}