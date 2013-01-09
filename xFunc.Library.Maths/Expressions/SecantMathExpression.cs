using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
{

    public class SecantMathExpression : TrigonometryMathExpression
    {

        public SecantMathExpression()
            : base(null)
        {

        }

        public SecantMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("sec({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return 1 / Math.Cos(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return 1 / Math.Cos(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return 1 / Math.Cos(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            TangentMathExpression tan = new TangentMathExpression(firstMathExpression.Clone());
            SecantMathExpression sec = new SecantMathExpression(firstMathExpression.Clone());
            MultiplicationMathExpression mul1 = new MultiplicationMathExpression(tan, sec);
            MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);

            return mul2;
        }

        public override IMathExpression Clone()
        {
            return new SecantMathExpression(firstMathExpression.Clone());
        }

    }

}
