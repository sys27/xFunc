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

namespace xFunc.Maths.Expressions.Trigonometric
{

    /// <summary>
    /// The base class for trigonomeric functions. This is an <c>abstract</c> class.
    /// </summary>
    public abstract class TrigonometryMathExpression : UnaryMathExpression
    {

        /// <summary>
        /// The angle measurement.
        /// </summary>
        protected AngleMeasurement angleMeasurement;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrigonometryMathExpression"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        protected TrigonometryMathExpression(IMathExpression firstMathExpression)
            : this(firstMathExpression, AngleMeasurement.Degree) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrigonometryMathExpression"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        protected TrigonometryMathExpression(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement)
            : base(firstMathExpression)
        {
            this.angleMeasurement = angleMeasurement;
        }

        // todo: protected
        public abstract double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions);
        public abstract double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions);
        public abstract double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions);

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <returns>The result of calculation.</returns>
        public override double Calculate()
        {
            if (angleMeasurement == AngleMeasurement.Degree)
                return CalculateDergee(null, null);
            if (angleMeasurement == AngleMeasurement.Radian)
                return CalculateRadian(null, null);
            if (angleMeasurement == AngleMeasurement.Gradian)
                return CalculateGradian(null, null);

            return double.NaN;
        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result of calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            if (angleMeasurement == AngleMeasurement.Degree)
                return CalculateDergee(parameters, null);
            if (angleMeasurement == AngleMeasurement.Radian)
                return CalculateRadian(parameters, null);
            if (angleMeasurement == AngleMeasurement.Gradian)
                return CalculateGradian(parameters, null);

            return double.NaN;
        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="functions">The functions.</param>
        /// <returns>The result of calculation.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (angleMeasurement == AngleMeasurement.Degree)
                return CalculateDergee(parameters, functions);
            if (angleMeasurement == AngleMeasurement.Radian)
                return CalculateRadian(parameters, functions);
            if (angleMeasurement == AngleMeasurement.Gradian)
                return CalculateGradian(parameters, functions);

            return double.NaN;
        }

        /// <summary>
        /// Gets or sets the angle measurement.
        /// </summary>
        /// <value>The angle measurement.</value>
        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return angleMeasurement;
            }
            set
            {
                angleMeasurement = value;
            }
        }

    }

}
