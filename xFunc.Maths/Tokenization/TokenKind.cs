// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Tokenization;

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

    /// <summary>
    /// String.
    /// </summary>
    String,

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
    /// The 'assign' token.
    /// </summary>
    AssignKeyword,

    /// <summary>
    /// The 'unassign' token.
    /// </summary>
    UnassignKeyword,

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
    /// The '=>, ->' token.
    /// </summary>
    LambdaOperator,

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