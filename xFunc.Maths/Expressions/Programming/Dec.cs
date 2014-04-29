using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the decrement operator.
    /// </summary>
    public class Dec : UnaryExpression
    {

        internal Dec() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dec"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        public Dec(IExpression argument)
            : base(argument) { }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("{0}--");
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
            var var = (Variable)argument;
            var newValue = (double)var.Calculate(parameters) - 1;
            parameters.Parameters[var.Name] = newValue;

            return newValue;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="Inc" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Dec(argument.Clone());
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        /// <seealso cref="Variable" />
        protected override IExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public virtual IExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                if (value != null)
                {
                    if (!(value is Variable))
                        throw new NotSupportedException();

                    value.Parent = this;
                }

                argument = value;
            }
        }

    }
}
