using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
{

    public class ArccscMathExpression : TrigonometryMathExpression
    {

        public ArccscMathExpression() : base(null) { }

        public ArccscMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arccsc({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = 1 / firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Asin(1 / firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = 1 / firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            throw new NotImplementedException();
        }

    }

}
