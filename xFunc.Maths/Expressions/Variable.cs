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

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents variables in expressions.
    /// </summary>
    public class Variable : IExpression, IEquatable<Variable>
    {
        /// <summary>
        /// The 'x' variable.
        /// </summary>
        public static readonly Variable X = new Variable("x");

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="name">A name of variable.</param>
        public Variable(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        /// <summary>
        /// Defines an implicit conversion of a Variable object to a string object.
        /// </summary>
        /// <param name="variable">The value to convert.</param>
        /// <returns>An object that contains the converted value.</returns>
        public static implicit operator string(Variable? variable)
            => variable?.Name ?? throw new ArgumentNullException(nameof(variable));

        /// <summary>
        /// Defines an implicit conversion of a string object to a Variable object.
        /// </summary>
        /// <param name="variable">The value to convert.</param>
        /// <returns>An object that contains the converted value.</returns>
        public static implicit operator Variable(string variable)
            => new Variable(variable);

        /// <summary>
        /// Deconstructs <see cref="Variable"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="variable">The name of variable.</param>
        public void Deconstruct(out string variable) => variable = Name;

        /// <inheritdoc />
        public bool Equals(Variable? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Name == other.Name;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (typeof(Variable) != obj.GetType())
                return false;

            return Equals((Variable)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
            => HashCode.Combine(Name);

        /// <inheritdoc />
        public string ToString(IFormatter formatter)
            => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString()
            => ToString(new CommonFormatter());

        /// <inheritdoc />
        /// <exception cref="NotSupportedException">Always.</exception>
        public object Execute() => throw new NotSupportedException();

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters)
        {
            if (parameters is null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.Variables[Name];
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
        /// Gets a name of this variable.
        /// </summary>
        public string Name { get; }
    }
}