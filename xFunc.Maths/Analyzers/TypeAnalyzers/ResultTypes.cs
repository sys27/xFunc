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

using System;

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{
    /// <summary>
    /// Represents results of expressions.
    /// </summary>
    [Flags]
    public enum ResultTypes
    {
        /// <summary>
        /// The expression doesn't return anything.
        /// </summary>
        None = 0,

        /// <summary>
        /// The expression returns undefined result.
        /// </summary>
        Undefined = 1,

        /// <summary>
        /// The expression returns a number.
        /// </summary>
        Number = 1 << 1,

        /// <summary>
        /// The expression returns a complex number.
        /// </summary>
        ComplexNumber = 1 << 2,

        /// <summary>
        /// The expression returns a boolean (true or false).
        /// </summary>
        Boolean = 1 << 3,

        /// <summary>
        /// The expression returns a vector.
        /// </summary>
        Vector = 1 << 4,

        /// <summary>
        /// The expression returns a matrix.
        /// </summary>
        Matrix = 1 << 5,

        /// <summary>
        /// The expression returns other expression.
        /// </summary>
        Expression = 1 << 6,

        /// <summary>
        /// The expression returns an angle.
        /// </summary>
        AngleNumber = 1 << 7,

        /// <summary>
        /// The expression returns a string.
        /// </summary>
#pragma warning disable CA1720
        String = 1 << 8,
#pragma warning restore CA1720

        /// <summary>
        /// The expression returns a power number.
        /// </summary>
        PowerNumber = 1 << 9,

        /// <summary>
        /// The expression returns a number or a complex number.
        /// </summary>
        NumberOrComplex = Number | ComplexNumber,

        /// <summary>
        /// The expression returns a number or a angle number.
        /// </summary>
        NumberOrAngle = Number | AngleNumber,

        /// <summary>
        /// The expression returns a number or a angle number or a complex number.
        /// </summary>
        NumberOrAngleOrComplex = NumberOrAngle | ComplexNumber,

        /// <summary>
        /// The expression returns a number or a angle number or a complex number or a vector.
        /// </summary>
        NumberOrAngleOrComplexOrVector = NumberOrAngleOrComplex | Vector,

        /// <summary>
        /// The expression returns a number or a vector or a matrix.
        /// </summary>
        NumberOrVectorOrMatrix = Number | Vector | Matrix,
    }
}