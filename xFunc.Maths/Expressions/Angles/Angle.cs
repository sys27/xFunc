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

namespace xFunc.Maths.Expressions.Angles
{
    /// <summary>
    /// Represents an angle number.
    /// </summary>
    public class Angle : IExpression, IEquatable<Angle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> class.
        /// </summary>
        /// <param name="value">An angle.</param>
        public Angle(AngleValue value) => Value = value;

        /// <summary>
        /// Deconstructs <see cref="Angle"/> to <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="value">The angle.</param>
        public void Deconstruct(out AngleValue value) => value = Value;

        /// <inheritdoc />
        public bool Equals(Angle? other)
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

            if (typeof(Angle) != obj.GetType())
                return false;

            return Equals((Angle)obj);
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
        /// Gets an angle.
        /// </summary>
        public AngleValue Value { get; }
    }
}