// Copyright 2012-2013 Dmitry Kischenko
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
    /// Defines methods to calculate and to differentiate mathematical expressions.
    /// </summary>
    public interface IMathExpression
    {

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        double Calculate();
        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>A result of the calculation.</returns>
        /// <seealso cref="ExpressionParameters"/>
        double Calculate(ExpressionParameters parameters);
        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>Returns a derivative of the expression.</returns>
        IMathExpression Differentiate();
        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>Returns a derivative of the expression of several variables.</returns>
        /// <seealso cref="Variable"/>
        IMathExpression Differentiate(Variable variable);

        /// <summary>
        /// Clones this instance of the <see cref="IMathExpression"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        IMathExpression Clone();

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        IMathExpression Parent { get; set; }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        int MinCountOfParams { get; }
        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        int MaxCountOfParams { get; }
        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        int CountOfParams { get; }

    }

}
