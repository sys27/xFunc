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
            return MathExtentions.Acsc(firstMathExpression.Calculate(parameters)) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return MathExtentions.Acsc(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            return MathExtentions.Acsc(firstMathExpression.Calculate(parameters)) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            AbsoluteMathExpression abs = new AbsoluteMathExpression(firstMathExpression);
            ExponentiationMathExpression sqr = new ExponentiationMathExpression(firstMathExpression, new NumberMathExpression(2));
            SubtractionMathExpression sub = new SubtractionMathExpression(sqr, new NumberMathExpression(1));
            SqrtMathExpression sqrt = new SqrtMathExpression(sub);
            MultiplicationMathExpression mul = new MultiplicationMathExpression(abs, sqrt);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), mul);
            UnaryMinusMathExpression unary = new UnaryMinusMathExpression(div);

            return unary;
        }

    }

}
