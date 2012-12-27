using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Expressions.Maths
{

    public class CotangentMathExpression : TrigonometryMathExpression
    {

        public CotangentMathExpression() : base(null) { }

        public CotangentMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("cot({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return MathExtentions.Cot(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            var x = firstMathExpression.Calculate(parameters);

            return MathExtentions.Cot(x);
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return MathExtentions.Cot(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            SineMathExpression sine = new SineMathExpression(firstMathExpression);
            ExponentiationMathExpression involution = new ExponentiationMathExpression(sine, new NumberMathExpression(2));
            DivisionMathExpression division = new DivisionMathExpression(firstMathExpression.Derivative(variable), involution);
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(division);

            return unMinus;
        }

    }

}
