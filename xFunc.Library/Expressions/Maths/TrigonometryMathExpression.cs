using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Expressions.Maths
{

    public abstract class TrigonometryMathExpression : UnaryMathExpression
    {

        protected AngleMeasurement angleMeasurement;

        public TrigonometryMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {
            this.angleMeasurement = Maths.AngleMeasurement.Degree;
        }

        public abstract double CalculateDergee(MathParameterCollection parameters);
        public abstract double CalculateRadian(MathParameterCollection parameters);
        public abstract double CalculateGradian(MathParameterCollection parameters);

        public override double Calculate(MathParameterCollection parameters)
        {
            if (angleMeasurement == Maths.AngleMeasurement.Degree)
                return CalculateDergee(parameters);
            if (angleMeasurement == Maths.AngleMeasurement.Radian)
                return CalculateRadian(parameters);
            if (angleMeasurement == Maths.AngleMeasurement.Gradian)
                return CalculateGradian(parameters);

            return double.NaN;
        }

        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return angleMeasurement;
            }
            set
            {
                angleMeasurement = value;
            }
        }

    }

}
