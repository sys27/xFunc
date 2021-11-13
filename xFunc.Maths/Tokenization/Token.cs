// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace xFunc.Maths.Tokenization;

/// <summary>
/// Represents a token.
/// </summary>
internal readonly struct Token
{
    private Token(TokenKind kind, double numberValue, string? stringValue)
    {
        Kind = kind;
        NumberValue = numberValue;
        StringValue = stringValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> struct.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// The token.
    /// </returns>
    public static Token Id(string id)
    {
        Debug.Assert(!string.IsNullOrEmpty(id), "String should not be empty.");

        return new Token(TokenKind.Id, default, id);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> struct.
    /// </summary>
    /// <param name="stringValue">The string value.</param>
    /// <returns>
    /// The token.
    /// </returns>
    public static Token String(string stringValue)
    {
        Debug.Assert(stringValue != null, "String should not be empty.");

        return new Token(TokenKind.String, default, stringValue);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> struct.
    /// </summary>
    /// <param name="numberValue">The number value.</param>
    /// <returns>
    /// The token.
    /// </returns>
    public static Token Number(double numberValue)
    {
        return new Token(TokenKind.Number, numberValue, default);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> struct.
    /// </summary>
    /// <param name="kind">The token kind.</param>
    /// <returns>
    /// The token.
    /// </returns>
    public static Token Create(TokenKind kind)
    {
        Debug.Assert(kind != TokenKind.Id && kind != TokenKind.Number, "Use other ctor for Id/Number tokens.");

        return new Token(kind, default, default);
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override string ToString()
    {
        if (IsId() || Is(TokenKind.String))
            return $"String: {StringValue}";

        if (IsNumber())
            return $"Number: {NumberValue}";

        return $"Kind: {Kind}";
    }

    /// <summary>
    /// Determines whether the specified token is not empty.
    /// </summary>
    /// <param name="token">The specified token.</param>
    /// <returns>
    /// <c>true</c> if the specified token is not empty token; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator true(Token token) => token.IsNotEmpty();

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
    public bool Is(TokenKind kind) => Kind == kind;

    /// <summary>
    /// Determines whether the current token is empty token.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current token is empty token; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty() => Is(TokenKind.Empty);

    /// <summary>
    /// Determines whether the current token is not empty token.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current token is not empty token; otherwise, <c>false</c>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotEmpty() => !Is(TokenKind.Empty);

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
    /// Gets a token kind.
    /// </summary>
    public TokenKind Kind { get; }

    /// <summary>
    /// Gets a string value.
    /// </summary>
    public string? StringValue { get; }

    /// <summary>
    /// Gets a number value.
    /// </summary>
    public double NumberValue { get; }

    /// <summary>
    /// Gets an empty token.
    /// </summary>
    public static Token Empty => Create(TokenKind.Empty);
}