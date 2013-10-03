using System;

namespace xFunc.Maths.Expressions
{

    public class ExpressionParameters
    {

        private AngleMeasurement angleMeasuremnt;
        private MathParameterCollection parameters;
        private MathFunctionCollection functions;

        public ExpressionParameters(AngleMeasurement angleMeasuremnt)
            : this(angleMeasuremnt, null, null)
        {
        }

        public ExpressionParameters(MathParameterCollection parameters)
            : this(AngleMeasurement.Degree, parameters, null)
        {
        }

        public ExpressionParameters(MathFunctionCollection functions)
            : this(AngleMeasurement.Degree, null, functions)
        {
        }

        public ExpressionParameters(MathParameterCollection parameters, MathFunctionCollection functions)
            : this(AngleMeasurement.Degree, parameters, functions)
        {
        }

        public ExpressionParameters(AngleMeasurement angleMeasuremnt, MathParameterCollection parameters, MathFunctionCollection functions)
        {
            this.angleMeasuremnt = angleMeasuremnt;
            this.parameters = parameters;
            this.functions = functions;
        }

        public static implicit operator ExpressionParameters(AngleMeasurement angleMeasurement)
        {
            return new ExpressionParameters(angleMeasurement);
        }

        public static implicit operator ExpressionParameters(MathParameterCollection parameters)
        {
            return new ExpressionParameters(parameters);
        }

        public static implicit operator ExpressionParameters(MathFunctionCollection functions)
        {
            return new ExpressionParameters(functions);
        }

        public AngleMeasurement Angleeasurement
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

        public MathParameterCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        public MathFunctionCollection Functions
        {
            get
            {
                return functions;
            }
        }

    }

}
