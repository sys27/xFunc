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
using System.Collections.Immutable;
using System.Diagnostics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Expressions.Units.Converters;

namespace xFunc.Maths.Expressions.Units
{
    /// <summary>
    /// Represents the convert function.
    /// </summary>
    public class Convert : IExpression
    {
        private readonly IConverter converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Convert"/> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="unit">The target unit.</param>
        public Convert(IConverter converter, IExpression value, IExpression unit)
            : this(converter, ImmutableArray.Create(value, unit))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Convert"/> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <param name="arguments">The list of arguments.</param>
        internal Convert(IConverter converter, ImmutableArray<IExpression> arguments)
        {
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));

            Debug.Assert(arguments != null, $"{nameof(arguments)} shouldn't be null.");
            Debug.Assert(arguments.Length == 2, "{nameof(arguments)} should contain 2 parameters.");

            Value = arguments[0] ?? throw new ArgumentNullException(nameof(arguments));
            Unit = arguments[1] ?? throw new ArgumentNullException(nameof(arguments));
        }

        /// <summary>
        /// Deconstructs <see cref="Convert"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="unit">The target unit.</param>
        public void Deconstruct(out IExpression value, out IExpression unit)
        {
            value = Value;
            unit = Unit;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || GetType() != obj.GetType())
                return false;

            var (value, unit) = (Convert)obj;

            return Value.Equals(value) && Unit.Equals(unit);
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public object Execute() => Execute(null);

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters)
        {
            var valueResult = Value.Execute(parameters);
            var unitResult = Unit.Execute(parameters);

            return unitResult switch
            {
                string unit => converter.Convert(valueResult, unit),
                _ => throw new ResultIsNotSupportedException(this, valueResult, unitResult),
            };
        }

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
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="left">The left argument of new expression.</param>
        /// <param name="right">The right argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public IExpression Clone(IExpression? left = null, StringExpression? right = null)
            => new Convert(converter, left ?? Value, right ?? Unit);

        /// <summary>
        /// Gets the value.
        /// </summary>
        public IExpression Value { get; }

        /// <summary>
        /// Gets the target unit.
        /// </summary>
        public IExpression Unit { get; }
    }
}