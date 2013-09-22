using System;

namespace xFunc.Logics.Expressions
{

    /// <summary>
    /// Represents the Undefine operation.
    /// </summary>
    public class Undefine : ILogicExpression
    {

        private Variable variable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Undefine"/> class.
        /// </summary>
        internal Undefine()
            : this(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Undefine"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        public Undefine(Variable variable)
        {
            this.variable = variable;
        }

        /// <summary>
        /// Calculates this logical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        /// <seealso cref="LogicParameterCollection" />
        /// <exception cref="System.ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        public bool Calculate(LogicParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            parameters.Remove(variable.Name);

            return false;
        }

        /// <summary>
        /// Gets or sets the variable.
        /// </summary>
        /// <value>The variable.</value>
        public Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

    }

}
