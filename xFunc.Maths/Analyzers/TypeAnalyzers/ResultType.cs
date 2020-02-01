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

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{
    /// <summary>
    /// Represents results of expressions.
    /// </summary>
    [Flags]
    public enum ResultType
    {
        /// <summary>
        /// The expression doesn't return anything.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The expression returns undefined result.
        /// </summary>
        Undefined = 0x1,

        /// <summary>
        /// The expression returns a number.
        /// </summary>
        Number = 0x2,

        /// <summary>
        /// The expression returns a complex number.
        /// </summary>
        ComplexNumber = 0x4,

        /// <summary>
        /// The expression returns a boolean (true or false).
        /// </summary>
        Boolean = 0x8,

        /// <summary>
        /// The expression returns a vector.
        /// </summary>
        Vector = 0x10,

        /// <summary>
        /// The expression returns a matrix.
        /// </summary>
        Matrix = 0x20,

        /// <summary>
        /// The expression returns other expression.
        /// </summary>
        Expression = 0x40
    }
}