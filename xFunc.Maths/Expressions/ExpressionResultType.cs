// Copyright 2012-2015 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents results of expressions.
    /// </summary>
    [Flags]
    public enum ExpressionResultType
    {

        /// <summary>
        /// An expression doesn't return anything.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// An expression returns undefined result.
        /// </summary>
        Undefined = 0x1,
        /// <summary>
        /// An expression returns a number.
        /// </summary>
        Number = 0x2,
        /// <summary>
        /// An expression returns a boolean (true or false).
        /// </summary>
        Boolean = 0x4,
        /// <summary>
        /// An expression returns a matrix or a vector.
        /// </summary>
        Matrix = 0x8,
        /// <summary>
        /// Combines other parameters.
        /// </summary>
        All = Undefined | Number | Boolean | Matrix

    }

    /// <summary>
    /// Extension for the <see cref="ExpressionResultType"/> enumeration.
    /// </summary>
    public static class ExpressionResultTypeExtension
    {

        /// <summary>
        /// Determines whether one or more bit fields are set in the current instance (faster than default implementation).
        /// </summary>
        /// <param name="type">An enumeration.</param>
        /// <param name="flag">An enumeration value.</param>
        /// <returns><c>true</c> if the bit field or bit fields that are set in flag are also set in the current instance; otherwise, <c>false</c>.</returns>
        public static bool HasFlagNI(this ExpressionResultType type, ExpressionResultType flag)
        {
            return (type & flag) == flag;
        }

    }

}
