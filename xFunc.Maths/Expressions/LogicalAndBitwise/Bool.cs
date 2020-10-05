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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions.LogicalAndBitwise
{
    /// <summary>
    /// Represents the boolean constant.
    /// </summary>
    public class Bool : IExpression, IEquatable<Bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bool"/> class.
        /// </summary>
        /// <param name="value">The value of this constant.</param>
        private Bool(bool value) => Value = value;

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
        /// Performs an implicit conversion from <see cref="Bool"/> to <see cref="bool"/>.
        /// </summary>
        /// <param name="boolean">The boolean.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator bool(Bool? boolean)
            => boolean?.Value ?? throw new ArgumentNullException(nameof(boolean));

        /// <summary>
        /// Performs an implicit conversion from <see cref="bool"/> to <see cref="Bool"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Bool(bool value)
            => new Bool(value);

        /// <inheritdoc />
        public bool Equals(Bool? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (typeof(Bool) != obj.GetType())
                return false;

            return Equals((Bool)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Value);

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Gets the value of this expression.
        /// </summary>
#pragma warning disable SA1623
        public bool Value { get; }
#pragma warning restore SA1623

        /// <summary>
        /// The <c>true</c> constant.
        /// </summary>
        public static readonly Bool True = new Bool(true);

        /// <summary>
        /// The <c>false</c> constant.
        /// </summary>
        public static readonly Bool False = new Bool(false);
    }
}