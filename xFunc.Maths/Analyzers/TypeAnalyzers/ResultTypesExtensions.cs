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

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{
    /// <summary>
    /// Extensions method for the <see cref="ResultTypes"/> enum.
    /// </summary>
    internal static class ResultTypesExtensions
    {
        /// <summary>
        /// Helper method for throw the <see cref="ParameterTypeMismatchException"/> exception.
        /// </summary>
        /// <param name="expected">The expected result type.</param>
        /// <param name="actual">The actual result type.</param>
        /// <returns>Always throws <see cref="ParameterTypeMismatchException"/>.</returns>
        [DoesNotReturn]
        internal static ResultTypes ThrowFor(this ResultTypes expected, ResultTypes actual) =>
            throw new ParameterTypeMismatchException(expected, actual);

        /// <summary>
        /// Helper method for throw the <see cref="BinaryParameterTypeMismatchException"/> exception.
        /// </summary>
        /// <param name="expected">The expected result type.</param>
        /// <param name="actual">The actual result type.</param>
        /// <returns>Always throws <see cref="BinaryParameterTypeMismatchException"/>.</returns>
        [DoesNotReturn]
        internal static ResultTypes ThrowForLeft(this ResultTypes expected, ResultTypes actual) =>
            throw new BinaryParameterTypeMismatchException(expected, actual, BinaryParameterType.Left);

        /// <summary>
        /// Helper method for throw the <see cref="BinaryParameterTypeMismatchException"/> exception.
        /// </summary>
        /// <param name="expected">The expected result type.</param>
        /// <param name="actual">The actual result type.</param>
        /// <returns>Always throws <see cref="BinaryParameterTypeMismatchException"/>.</returns>
        [DoesNotReturn]
        internal static ResultTypes ThrowForRight(this ResultTypes expected, ResultTypes actual) =>
            throw new BinaryParameterTypeMismatchException(expected, actual, BinaryParameterType.Right);
    }
}