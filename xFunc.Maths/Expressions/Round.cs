using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the "round" function.
    /// </summary>
    public class Round : DifferentParametersExpression
    {

        internal Round()
            : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
        public Round(IExpression argument) :
            this(new[] { argument }, 1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The expression that represents the number of fractional digits in the return value.</param>
        public Round(IExpression argument, IExpression digits) :
            this(new[] { argument, digits }, 2) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="System.ArgumentNullException">args</exception>
        /// <exception cref="System.ArgumentException"></exception>
        public Round(IExpression[] args, int countOfParams)
            : base(args, countOfParams)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length != countOfParams)
                throw new ArgumentException();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(4211, 10831);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString("round");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            var arg = (double)Argument.Calculate(parameters);
            var digits = Digits != null ? (int)(double)Digits.Calculate(parameters) : 0;

#if PORTABLE
            return Math.Round(arg, digits);
#else
            return Math.Round(arg, digits, MidpointRounding.AwayFromZero);
#endif
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
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
        /// <exception cref="System.NotSupportedException"></exception>
        /// <seealso cref="Variable" />
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
            return new Round(CloneArguments(), countOfParams);
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
        public override int MaxCountOfParams
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// The expression that represents a double-precision floating-point number to be rounded.
        /// </summary>
        /// <value>
        /// The expression that represents a double-precision floating-point number to be rounded.
        /// </value>
        public IExpression Argument
        {
            get
            {
                return arguments[0];
            }
        }

        /// <summary>
        /// The expression that represents the number of fractional digits in the return value.
        /// </summary>
        /// <value>
        /// The expression that represents the number of fractional digits in the return value.
        /// </value>
        public IExpression Digits
        {
            get
            {
                return countOfParams == 2 ? arguments[1] : null;
            }
        }

    }

}
