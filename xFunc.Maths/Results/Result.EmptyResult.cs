// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Represents the empty result.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class EmptyResult : Result
    {
        /// <inheritdoc />
        public override string ToString()
            => string.Empty;
    }
}