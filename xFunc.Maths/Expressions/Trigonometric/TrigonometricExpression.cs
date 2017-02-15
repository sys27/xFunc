// Copyright 2012-2017 Dmitry Kischenko
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
using System.Numerics;

namespace xFunc.Maths.Expressions.Trigonometric
{

    /// <summary>
    /// The base class for trigonomeric functions. This is an <c>abstract</c> class.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.UnaryExpression" />
    public abstract class TrigonometricExpression : CachedUnaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TrigonometricExpression"/> class.
        /// </summary>
        protected TrigonometricExpression() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrigonometricExpression"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        protected TrigonometricExpression(IExpression expression) : base(expression) { }

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>
        /// The result type of current expression.
        /// </returns>
        protected override ExpressionResultType GetResultType()
        {
            return m_argument.ResultType;
        }

        /// <summary>
        /// Calculates this mathematical expression (using degree).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected abstract double ExecuteDergee(ExpressionParameters parameters);
        /// <summary>
        /// Calculates this mathematical expression (using radian).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected abstract double ExecuteRadian(ExpressionParameters parameters);
        /// <summary>
        /// Calculates this mathematical expression (using gradian).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected abstract double ExecuteGradian(ExpressionParameters parameters);
        /// <summary>
        /// Calculates the this mathematical expression (complex number).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        protected abstract Complex ExecuteComplex(ExpressionParameters parameters);

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var resultType = this.ResultType;
            if (resultType == ExpressionResultType.ComplexNumber)
                return ExecuteComplex(parameters);

            if (parameters == null || parameters.AngleMeasurement == AngleMeasurement.Degree)
                return ExecuteDergee(parameters);
            if (parameters.AngleMeasurement == AngleMeasurement.Radian)
                return ExecuteRadian(parameters);
            if (parameters.AngleMeasurement == AngleMeasurement.Gradian)
                return ExecuteGradian(parameters);

            return double.NaN;
        }

        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>
        /// The type of the argument.
        /// </value>
        public override ExpressionResultType ArgumentType { get; } = ExpressionResultType.Number | ExpressionResultType.ComplexNumber;

    }

}
