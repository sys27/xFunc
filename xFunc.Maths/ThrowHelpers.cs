// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace xFunc.Maths;

/// <summary>
/// Contains helpers for throw exceptions.
/// </summary>
internal static class ThrowHelpers
{
    /// <summary>
    /// Throws an <see cref="TokenizeException"/>.
    /// </summary>
    /// <param name="symbol">The unsupported symbol.</param>
    [DoesNotReturn]
    internal static void NotSupportedSymbol(char symbol)
        => throw new TokenizeException(symbol);

    /// <summary>
    /// Throws a <see cref="ParseException"/>.
    /// </summary>
    /// <param name="tokenKind">The operator kind.</param>
    /// <returns>Does not return.</returns>
    [DoesNotReturn]
    internal static IExpression MissingSecondOperand(TokenKind tokenKind)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.SecondOperandParseException, tokenKind));

    /// <summary>
    /// Throws a <see cref="ParseException"/> when the '(' symbol is missing.
    /// </summary>
    /// <param name="tokenKind">The token kind.</param>
    [DoesNotReturn]
    internal static void MissingOpenParenthesis(TokenKind tokenKind)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.FunctionOpenParenthesisParseException, tokenKind));

    /// <summary>
    /// Throws a <see cref="ParseException"/> when the ')' symbol is missing.
    /// </summary>
    /// <param name="tokenKind">The token kind.</param>
    [DoesNotReturn]
    internal static void MissingCloseParenthesis(TokenKind tokenKind)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.FunctionCloseParenthesisParseException, tokenKind));

    /// <summary>
    /// Throws a <see cref="ParseException"/> when the ',' symbol is missing.
    /// </summary>
    /// <param name="previousExpression">The previous expression.</param>
    [DoesNotReturn]
    internal static void MissingComma(IExpression previousExpression)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CommaParseException, previousExpression));

    /// <summary>
    /// Throws a <see cref="ParseException"/>.
    /// </summary>
    /// <returns>Does not return.</returns>
    [DoesNotReturn]
    internal static IExpression MissingExpression()
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.MissingExpParseException));

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/>.
    /// </summary>
    /// <param name="arg">The name of argument.</param>
    [DoesNotReturn]
    internal static void ArgNull(ExceptionArgument arg)
        => throw new ArgumentNullException(arg.ToString());
}

#pragma warning disable SA1600
#pragma warning disable SA1602
internal enum ExceptionArgument
{
    exp,
    context,
    differentiator,
    simplifier,
    typeAnalyzer,
    converter,
}
#pragma warning restore SA1602
#pragma warning restore SA1600