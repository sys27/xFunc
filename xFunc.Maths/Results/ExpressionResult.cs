using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the result in the expression form.
    /// </summary>
    public class ExpressionResult : IResult
    {

        private IExpression exp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionResult"/> class.
        /// </summary>
        /// <param name="exp">The expression.</param>
        public ExpressionResult(IExpression exp)
        {
            this.exp = exp;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return exp.ToString();
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public IExpression Result
        {
            get
            {
                return exp;
            }
        }

    }

}
