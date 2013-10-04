using System;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Combines all parameters of expressions.
    /// </summary>
    public class ExpressionParameters
    {

        private AngleMeasurement angleMeasuremnt;
        private MathParameterCollection parameters;
        private MathFunctionCollection functions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt)
            : this(angleMeasuremnt, new MathParameterCollection(), new MathFunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="parameters">The collection of variables' values.</param>
        public ExpressionParameters(MathParameterCollection parameters)
            : this(AngleMeasurement.Degree, parameters, new MathFunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(MathFunctionCollection functions)
            : this(AngleMeasurement.Degree, new MathParameterCollection(), functions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        /// <param name="parameters">The collection of variables' values.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt, MathParameterCollection parameters)
            : this(angleMeasuremnt, parameters, new MathFunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt, MathFunctionCollection functions)
            : this(angleMeasuremnt, new MathParameterCollection(), functions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="parameters">The collection of variables' values.</param>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(MathParameterCollection parameters, MathFunctionCollection functions)
            : this(AngleMeasurement.Degree, parameters, functions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        /// <param name="parameters">The collection of variables' values.</param>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt, MathParameterCollection parameters, MathFunctionCollection functions)
        {
            this.angleMeasuremnt = angleMeasuremnt;
            this.parameters = parameters;
            this.functions = functions;
        }

        /// <summary>
        /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="AngleMeasurement"/>.
        /// </summary>
        /// <param name="angleMeasurement">The angle measurement.</param>
        /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
        public static implicit operator ExpressionParameters(AngleMeasurement angleMeasurement)
        {
            return new ExpressionParameters(angleMeasurement);
        }

        /// <summary>
        /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="MathParameterCollection"/>.
        /// </summary>
        /// <param name="parameters">The collection of variables' values.</param>
        /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
        public static implicit operator ExpressionParameters(MathParameterCollection parameters)
        {
            return new ExpressionParameters(parameters);
        }

        /// <summary>
        /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="MathFunctionCollection"/>.
        /// </summary>
        /// <param name="functions">The collection of user functions.</param>
        /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
        public static implicit operator ExpressionParameters(MathFunctionCollection functions)
        {
            return new ExpressionParameters(functions);
        }

        /// <summary>
        /// Gets or sets the angle measurement.
        /// </summary>
        /// <value>
        /// The angle measurement.
        /// </value>
        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return angleMeasuremnt;
            }
            set
            {
                angleMeasuremnt = value;
            }
        }

        /// <summary>
        /// Gets the collection of variables' values.
        /// </summary>
        /// <value>
        /// The collection of variables' values.
        /// </value>
        public MathParameterCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Gets the collection of user functions.
        /// </summary>
        /// <value>
        /// The collection of user functions.
        /// </value>
        public MathFunctionCollection Functions
        {
            get
            {
                return functions;
            }
        }

    }

}
