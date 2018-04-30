// Copyright 2012-2018 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Collections
{

    /// <summary>
    /// Item of <see cref="ParameterCollection"/>.
    /// </summary>
    public class Parameter : IComparable<Parameter>
    {
        private object value;

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
            this.Key = key;
            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Creates a constant.
        /// </summary>
        /// <param name="key">The name of parameter.</param>
        /// <param name="value">The value of parameter.</param>
        /// <returns>A constant.</returns>
        public static Parameter CreateConstant(string key, object value)
        {
            return new Parameter(key, value, ParameterType.Constant);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            var param = obj as Parameter;
            if (param == null)
                return false;

            return Key == param.Key && value.Equals(param.value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 163;

            hash = hash * 41 + Key.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2})", Key, value, Type);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other.</returns>
        public int CompareTo(Parameter other)
        {
            return Key.CompareTo(other.Key);
        }

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
                return this.value;
            }
            set
            {
                if (IsNumber(value))
                    value = Convert.ToDouble(value);

                this.value = value;
            }
        }

        private static bool IsNumber(object value)
        {
            return value is sbyte ||
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
        }

        /// <summary>
        /// Gets or sets the type of parameter.
        /// </summary>
        /// <value>
        /// The type of parameter.
        /// </value>
        public ParameterType Type { get; set; }

    }

}
