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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// Represents a token.
    /// </summary>
    internal readonly struct Token
    {
        private readonly TokenKind kind;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string? stringValue;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly double numberValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> struct.
        /// </summary>
        /// <param name="kind">The token kind.</param>
        public Token(TokenKind kind)
        {
            Debug.Assert(kind != TokenKind.Id && kind != TokenKind.Number, "Use other ctor for Id/Number tokens.");

            stringValue = default;
            numberValue = default;
            this.kind = kind;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> struct.
        /// </summary>
        /// <param name="stringValue">The string value of token.</param>
        public Token(string stringValue)
        {
            Debug.Assert(!string.IsNullOrEmpty(stringValue), "String should not be empty.");

            this.stringValue = stringValue;
            numberValue = default;
            kind = TokenKind.Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> struct.
        /// </summary>
        /// <param name="numberValue">The number value of token.</param>
        public Token(double numberValue)
        {
            stringValue = default;
            this.numberValue = numberValue;
            kind = TokenKind.Number;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            if (IsId())
                return $"String: {stringValue}";

            if (IsNumber())
                return $"Number: {numberValue}";

            return $"Kind: {kind}";
        }

        /// <summary>
        /// Determines whether the specified token is not empty.
        /// </summary>
        /// <param name="token">The specified token.</param>
        /// <returns>
        /// <c>true</c> if the specified token is not empty token; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Token token) => !token.IsEmpty();

        /// <summary>
        /// Determines whether the specified token is empty.
        /// </summary>
        /// <param name="token">The specified token.</param>
        /// <returns>
        /// <c>true</c> if the specified token is empty token; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public static bool operator false(Token token) => token.IsEmpty();

        /// <summary>
        /// Returns the non-empty token.
        /// </summary>
        /// <remarks>
        /// We use the '||' operator as 'syntax sugar' to chain methods in parser,
        /// previously it was done by '??' operator, but it cannot be overloaded.
        ///
        /// Always returns <paramref name="right"/>. It works because '||' is short-circuiting
        /// operator. It means that it will try to convert <paramref name="left"/> to boolean,
        /// if it is <c>true</c> (non-empty token), then current operator won't be executed.
        /// We can freely ignore <paramref name="left"/> parameter,
        /// because it always should empty token and simply return <paramref name="right"/>.
        /// </remarks>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The non-empty token.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Token operator |(Token left, Token right)
        {
            Debug.Assert(left.IsEmpty(), "The left should be empty.");

            return right;
        }

        /// <summary>
        /// Determines whether the current token has specified token kind.
        /// </summary>
        /// <param name="kind">The specified token kind.</param>
        /// <returns>
        /// <c>true</c> if the current token has specified token kind; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Is(TokenKind kind) => this.kind == kind;

        /// <summary>
        /// Determines whether the current token is empty token.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current token is empty token; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty() => Is(TokenKind.Empty);

        /// <summary>
        /// Determines whether the current token is <c>Id</c> token.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current token is <c>Id</c> token; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsId() => Is(TokenKind.Id);

        /// <summary>
        /// Determines whether the current token is <c>Number</c> token.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current token is <c>Number</c> token; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNumber() => Is(TokenKind.Number);

        /// <summary>
        /// Gets a string value.
        /// </summary>
        public string? StringValue => stringValue;

        /// <summary>
        /// Gets a number value.
        /// </summary>
        public double NumberValue => numberValue;

        /// <summary>
        /// Gets an empty token.
        /// </summary>
        public static Token Empty => new Token(TokenKind.Empty);
    }
}