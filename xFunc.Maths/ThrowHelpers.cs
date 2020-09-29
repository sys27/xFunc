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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using xFunc.Maths.Expressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization;

namespace xFunc.Maths
{
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
    }
#pragma warning restore SA1602
#pragma warning restore SA1600
}