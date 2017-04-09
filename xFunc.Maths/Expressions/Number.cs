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
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Number operation.
    /// </summary>
    public class Number : IExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Expressions.Number"/> class.
        /// </summary>
        /// <param name="number">A number.</param>
        public Number(double number)
        {
            this.Value = number;
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Expressions.Number"/> to a double value.
        /// </summary>
        /// <param name="number">The value to convert to a double.</param>
        /// <returns>An object that contains the value of the <paramref name="number"/> parameter.</returns>
        public static implicit operator double(Number number)
        {
            return number?.Value ?? double.NaN;
        }

        /// <summary>
        /// Defines an implicit conversion of double to <see cref="Expressions.Number"/>.
        /// </summary>
        /// <param name="number">The value to convert to <see cref="Expressions.Number"/>.</param>
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

            return Value.Equals(num.Value) || Math.Abs(Value - num.Value) < 1E-14;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ 9643;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter)
        {
            return this.Analyze(formatter);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToString(new CommonFormatter());
        }

        /// <summary>
        /// Returns a number. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the execution.</returns>
        public object Execute()
        {
            return Value;
        }

        /// <summary>
        /// Returns a number.
        /// </summary>
        /// <param name="parameters">A collection of variables.</param>
        /// <returns>A result of the execution.</returns>
        /// <seealso cref="ExpressionParameters"/>
        public object Execute(ExpressionParameters parameters)
        {
            return Value;
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
        /// Clones this instance of the <see cref="Expressions.Number"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public IExpression Clone()
        {
            return new Number(Value);
        }

        /// <summary>
        /// Gets or Sets a number.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IExpression Parent { get; set; }

        /// <summary>
        /// Gets the minimum count of parameters. 
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinParameters => 0;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public int MaxParameters => -1;

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int ParametersCount => 0;

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        /// <remarks>
        /// Usage of this property can affect performance. Don't use this property each time if you need to check result type of current expression. Just store/cache value only once and use it everywhere.
        /// </remarks>
        public ResultType ResultType => ResultType.Number;

    }

}
