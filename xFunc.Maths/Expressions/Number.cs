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
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Number operation.
    /// </summary>
    public class Number : IExpression
    {

        private IExpression parent;
        private double number;

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="number">A number.</param>
        public Number(double number)
        {
            this.number = number;
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Number"/> to a double value.
        /// </summary>
        /// <param name="number">The value to convert to a double.</param>
        /// <returns>An object that contains the value of the <paramref name="number"/> parameter.</returns>
        public static implicit operator double(Number number)
        {
            return number?.Value ?? double.NaN;
        }

        /// <summary>
        /// Defines an implicit conversion of double to <see cref="Number"/>.
        /// </summary>
        /// <param name="number">The value to convert to <see cref="Number"/>.</param>
        /// <returns>An object that contains the value of the <paramref name="number"/> parameter.</returns>
        public static implicit operator Number(double number)
        {
            return new Number(number);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var num = obj as Number;
            if (num == null)
                return false;

            return number.Equals(num.number) || Math.Abs(number - num.number) < 1E-14;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return number.GetHashCode() ^ 9643;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (number < 0)
            {
                var sub = parent as Sub;
                if (sub != null && sub.Right == this)
                    return $"({number.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
            }

            return number.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a number. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the execution.</returns>
        public object Execute()
        {
            return number;
        }

        /// <summary>
        /// Returns a number.
        /// </summary>
        /// <param name="parameters">A collection of variables.</param>
        /// <returns>A result of the execution.</returns>
        /// <seealso cref="ExpressionParameters"/>
        public object Execute(ExpressionParameters parameters)
        {
            return number;
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance of the <see cref="Number"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public IExpression Clone()
        {
            return new Number(number);
        }

        /// <summary>
        /// Gets or Sets a number.
        /// </summary>
        public double Value
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IExpression Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters. 
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinParameters { get; } = 0;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public int MaxParameters { get; } = -1;

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int ParametersCount { get; } = 0;

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public ExpressionResultType ResultType { get; } = ExpressionResultType.Number;

    }

}
