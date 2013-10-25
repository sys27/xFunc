using System;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Undefice operation.
    /// </summary>
    public class Undefine : IMathExpression
    {

        private IMathExpression key;

        /// <summary>
        /// Initializes a new instance of the <see cref="Undefine"/> class.
        /// </summary>
        internal Undefine() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Undefine"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public Undefine(IMathExpression key)
        {
            this.Key = key;
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
            if (this == obj)
                return true;

            var undef = obj as Undefine;
            if (undef == null)
                return false;

            return key.Equals(undef.key);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return key.GetHashCode() ^ 2143;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return string.Format("undef({0})", key);
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public double Calculate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        public double Calculate(ExpressionParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (key is Variable)
            {
                var e = key as Variable;

                parameters.Parameters.Remove(e.Name);
            }
            else if (key is UserFunction)
            {
                var e = key as UserFunction;

                parameters.Functions.Remove(e);
            }

            return double.NaN;
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>
        /// Throws exception.
        /// </returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Throws exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="NotSupportedException">Always.</exception>
        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Undefine(key.Clone());
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinCountOfParams
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
        public int MaxCountOfParams
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int CountOfParams
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="NotSupportedException"><paramref name="value"/> is not a <see cref="Variable"/> or a <see cref="UserFunction"/>.</exception>
        public IMathExpression Key
        {
            get
            {
                return key;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (!(value is Variable || value is UserFunction))
                {
                    throw new NotSupportedException();
                }

                key = value;
            }
        }

    }

}
