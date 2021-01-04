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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents a number.
    /// </summary>
    public class Number : IExpression, IEquatable<Number>
    {
        /// <summary>
        /// 0.
        /// </summary>
        public static readonly Number Zero = new Number(0);

        /// <summary>
        /// 1.
        /// </summary>
        public static readonly Number One = new Number(1);

        /// <summary>
        /// 2.
        /// </summary>
        public static readonly Number Two = new Number(2);

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="number">A number.</param>
        public Number(double number)
            : this(new NumberValue(number))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> class.
        /// </summary>
        /// <param name="number">A number.</param>
        public Number(NumberValue number) => Value = number;

        /// <summary>
        /// Deconstructs <see cref="Number"/> to <see cref="double"/>.
        /// </summary>
        /// <param name="number">The number.</param>
        public void Deconstruct(out NumberValue number) => number = Value;

        /// <inheritdoc />
        public bool Equals(Number? other)
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

            if (typeof(Number) != obj.GetType())
                return false;

            return Equals((Number)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
            => HashCode.Combine(Value);

        /// <inheritdoc />
        public string ToString(IFormatter formatter)
            => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString()
            => ToString(new CommonFormatter());

        /// <summary>
        /// Returns a number. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the execution.</returns>
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
        /// Gets a number.
        /// </summary>
        public NumberValue Value { get; }
    }
}