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
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters)) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters)) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            AbsoluteMathExpression abs = new AbsoluteMathExpression(firstMathExpression);
            ExponentiationMathExpression sqr = new ExponentiationMathExpression(firstMathExpression, new NumberMathExpression(2));
            SubtractionMathExpression sub = new SubtractionMathExpression(sqr, new NumberMathExpression(1));
            SqrtMathExpression sqrt = new SqrtMathExpression(sub);
            MultiplicationMathExpression mul = new MultiplicationMathExpression(abs, sqrt);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), mul);

            return div;
        }

    }

}
