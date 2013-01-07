using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
{

    public class ArcsecMathExpression : TrigonometryMathExpression
    {

        public ArcsecMathExpression()
            : base(null)
        {

        }

        public ArcsecMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("arcsec({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = 1 / firstMathExpression.Calculate(parameters);

            return Math.Acos(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Acos(1 / firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = 1 / firstMathExpression.Calculate(parameters);

            return Math.Acos(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            throw new NotImplementedException();
        }

    }

}
