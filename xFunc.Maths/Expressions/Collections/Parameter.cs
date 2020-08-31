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
using System.Globalization;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Collections
{
    /// <summary>
    /// Item of <see cref="ParameterCollection"/>.
    /// </summary>
    public class Parameter : IComparable<Parameter>, IEquatable<Parameter>
    {
        private object value = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="key">The name of parameter.</param>
        /// <param name="value">The value of parameter.</param>
        public Parameter(string key, object value)
            : this(key, value, ParameterType.Normal)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter" /> class.
        /// </summary>
        /// <param name="key">The name of parameter.</param>
        /// <param name="value">The value of parameter.</param>
        /// <param name="type">The type of parameter.</param>
        public Parameter(string key, int value, ParameterType type)
            : this(key, (object)value, type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter" /> class.
        /// </summary>
        /// <param name="key">The name of parameter.</param>
        /// <param name="value">The value of parameter.</param>
        /// <param name="type">The type of parameter.</param>
        public Parameter(string key, double value, ParameterType type)
            : this(key, (object)value, type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter" /> class.
        /// </summary>
        /// <param name="key">The name of parameter.</param>
        /// <param name="value">The value of parameter.</param>
        /// <param name="type">The type of parameter.</param>
        public Parameter(string key, object value, ParameterType type)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            Key = key;
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Creates a constant.
        /// </summary>
        /// <param name="key">The name of parameter.</param>
        /// <param name="value">The value of parameter.</param>
        /// <returns>A constant.</returns>
        public static Parameter CreateConstant(string key, object value)
            => new Parameter(key, value, ParameterType.Constant);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            var param = obj as Parameter;
            if (param == null)
                return false;

            return Equals(param);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
            => HashCode.Combine(Key);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
            => string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2})", Key, value, Type);

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Less than zero - This object is less than the other parameter.
        /// Zero - This object is equal to other.
        /// Greater than zero - This object is greater than other.
        /// </returns>
        public int CompareTo(Parameter? other)
            => string.Compare(Key, other?.Key, StringComparison.Ordinal);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to the other parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(Parameter? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Key == other.Key && value.Equals(other.value);
        }

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Parameter? left, Parameter? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Parameter? left, Parameter? right)
            => !(left == right);

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator >(Parameter? left, Parameter? right)
        {
            if (left is null || right is null)
                return false;

            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator <(Parameter? left, Parameter? right)
        {
            if (left is null || right is null)
                return false;

            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator >=(Parameter? left, Parameter? right)
            => !(left < right);

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator <=(Parameter? left, Parameter? right)
            => !(left > right);

        /// <summary>
        /// Gets the name of parameter.
        /// </summary>
        /// <value>
        /// The name of parameter.
        /// </value>
        public string Key { get; }

        /// <summary>
        /// Gets or sets the value of parameter.
        /// </summary>
        /// <value>
        /// The value of parameter.
        /// </value>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (Type != ParameterType.Normal)
                    throw new ParameterIsReadOnlyException(Resource.ReadOnlyError, Key);

                if (IsNumber(value))
                    value = Convert.ToDouble(value, CultureInfo.InvariantCulture);

                this.value = value;
            }
        }

        private static bool IsNumber(object value) =>
            value is sbyte ||
            value is byte ||
            value is short ||
            value is ushort ||
            value is int ||
            value is uint ||
            value is long ||
            value is ulong ||
            value is float ||
            value is double ||
            value is decimal;

        /// <summary>
        /// Gets the type of parameter.
        /// </summary>
        /// <value>
        /// The type of parameter.
        /// </value>
        public ParameterType Type { get; }
    }
}