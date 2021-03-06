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

using System.Collections.Immutable;
using System.Numerics;
using xFunc.Maths.Expressions.Angles;

namespace xFunc.Maths.Expressions.Trigonometric
{
    /// <summary>
    /// The base class for trigonometric functions. This is an <c>abstract</c> class.
    /// </summary>
    /// <seealso cref="UnaryExpression" />
    public abstract class TrigonometricExpression : UnaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrigonometricExpression"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        protected TrigonometricExpression(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrigonometricExpression"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        protected TrigonometricExpression(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Calculates this mathematical expression (using radian).
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        protected abstract NumberValue ExecuteInternal(AngleValue angleValue);

        /// <summary>
        /// Calculates the this mathematical expression (complex number).
        /// </summary>
        /// <param name="complex">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        protected abstract Complex ExecuteComplex(Complex complex);

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters? parameters)
        {
            var result = Argument.Execute(parameters);

            return result switch
            {
                NumberValue number => ExecuteInternal(AngleValue.Degree(number).ToRadian()),
                AngleValue angle => ExecuteInternal(angle.ToRadian()),
                Complex complex => ExecuteComplex(complex),
                _ => throw new ResultIsNotSupportedException(this, result),
            };
        }
    }
}