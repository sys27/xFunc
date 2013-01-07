using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
{

    public class CosecantMathExpression : TrigonometryMathExpression
    {

        public CosecantMathExpression()
            : base(null)
        {

        }

        public CosecantMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("csc({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return 1 / Math.Sin(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return 1 / Math.Sin(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return 1 / Math.Sin(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            UnaryMinusMathExpression unary = new UnaryMinusMathExpression(firstMathExpression.Derivative(variable));
            CotangentMathExpression cot = new CotangentMathExpression(firstMathExpression);
            CosecantMathExpression csc = new CosecantMathExpression(firstMathExpression);
            MultiplicationMathExpression mul1 = new MultiplicationMathExpression(cot, csc);
            MultiplicationMathExpression mul2 = new MultiplicationMathExpression(unary, mul1);

            return mul2;
        }

    }

}
