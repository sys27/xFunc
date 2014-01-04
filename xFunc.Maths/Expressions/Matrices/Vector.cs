// Copyright 2012-2013 Dmitry Kischenko
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
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Represents a vector.
    /// </summary>
    public class Vector : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        internal Vector()
            : base(null, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="System.ArgumentException"></exception>
        public Vector(IExpression[] args)
            : base(args, args.Length)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length < 1)
                throw new ArgumentException();
        }

        public Vector(int size)
            : base(new IExpression[size], size)
        {

        }

        /// <summary>
        /// Gets or sets the <see cref="IExpression"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IExpression"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The element of vector.</returns>
        public IExpression this[int index]
        {
            get
            {
                return arguments[index];
            }
            set
            {
                arguments[index] = value;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var vector = (Vector)obj;

            return this.countOfParams == vector.countOfParams &&
                   this.arguments.SequenceEqual(vector.arguments);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(3121, 8369);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append('{');
            foreach (var item in arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override object Calculate(ExpressionParameters parameters)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override IExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Vector(CloneArguments());
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public override IExpression[] Arguments
        {
            get
            {
                return base.Arguments;
            }
            set
            {
                if (value != null && value.Length == 0)
                    throw new ArgumentException();

                base.Arguments = value;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinCountOfParams
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public override int MaxCountOfParams
        {
            get
            {
                return -1;
            }
        }

    }

}
