// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace xFunc.Maths.Tokenization;

/// <summary>
/// The lexer for mathematical expressions.
/// </summary>
internal ref partial struct Lexer
{
    private ReadOnlySpan<char> function;

    /// <summary>
    /// Initializes a new instance of the <see cref="Lexer"/> struct.
    /// </summary>
    /// <param name="function">The string that contains the functions and operators.</param>
    /// <exception cref="ArgumentNullException">Throws when the <paramref name="function"/> parameter is null or empty.</exception>
    public Lexer(string function)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(function), Resource.NotSpecifiedFunction);

        Current = default;
        this.function = function.AsSpan();
    }

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <exception cref="TokenizeException">Thrown when input string has the not supported symbol.</exception>
    /// <returns>
    /// <c>true</c> if the enumerator was successfully advanced to the next element;
    /// <c>false</c> if the enumerator has passed the end of the collection.
    /// </returns>
    public bool MoveNext()
    {
        while (HasSymbols())
        {
            var result = CreateNumberToken() ||
                         CreateIdOrKeywordToken() ||
                         CreateOperatorToken() ||
                         CreateSymbol() ||
                         CreateStringToken();

            if (!result)
                throw new TokenizeException(function[0]);

            return true;
        }

        Current = default;
        return false;
    }

    private bool HasSymbols()
    {
        var index = 0;
        while (index < function.Length && char.IsWhiteSpace(function[index]))
            index++;

        if (index > 0)
            function = function[index..];

        return function.Length > 0;
    }

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    public Token Current { get; private set; }
}