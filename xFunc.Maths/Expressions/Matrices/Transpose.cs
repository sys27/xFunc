using System;

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Represets the Transpose function.
    /// </summary>
    public class Transpose : UnaryExpression
    {

        internal Transpose() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transpose"/> class.
        /// </summary>
        /// <param name="argument">Matrix or vector.</param>
        public Transpose(IExpression argument)
            : base(argument)
        {

        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="System.NotSupportedException">Argument is not <see cref="Matrix"/> or <see cref="Vector"/>.</exception>
        public override object Calculate(ExpressionParameters parameters)
        {
            if (argument is Vector)
                return ((Vector)argument).Transpose();
            if (argument is Matrix)
                return ((Matrix)argument).Transpose();

            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Transpose(this.argument.Clone());
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
        protected override IExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        /// <exception cref="System.NotSupportedException">Argument is not <see cref="Matrix"/> or <see cref="Vector"/>.</exception>
        public override IExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                if (value != null)
                {
                    if (!(value is Vector || value is Matrix))
                        throw new NotSupportedException();

                    value.Parent = this;
                }

                argument = value;
            }
        }

    }

}
