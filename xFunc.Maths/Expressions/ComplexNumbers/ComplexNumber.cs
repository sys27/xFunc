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
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions.ComplexNumbers
{
    /// <summary>
    /// Represent complex number expression.
    /// </summary>
    /// <seealso cref="IExpression" />
    public class ComplexNumber : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumber"/> class.
        /// </summary>
        /// <param name="real">The real part.</param>
        /// <param name="imaginary">The imaginary part.</param>
        public ComplexNumber(double real, double imaginary)
            : this(new Complex(real, imaginary))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumber"/> class.
        /// </summary>
        /// <param name="complex">The complex number.</param>
        public ComplexNumber(Complex complex) => Value = complex;

        /// <summary>
        /// Performs an implicit conversion from <see cref="ComplexNumber"/> to <see cref="Complex"/>.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Complex(ComplexNumber? number)
            => number?.Value ?? throw new ArgumentNullException(nameof(number));

        /// <summary>
        /// Performs an implicit conversion from <see cref="Complex"/> to <see cref="ComplexNumber"/>.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ComplexNumber(Complex number)
            => new ComplexNumber(number);

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is ComplexNumber num)
                return Value.Equals(num.Value);

            return false;
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public object Execute() => Value;

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters) => Value;

        /// <inheritdoc />
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this);
        }

        /// <inheritdoc />
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this, context);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public Complex Value { get; }
    }
}