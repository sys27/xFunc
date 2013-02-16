using System;

namespace xFunc.Maths.Expressions.Bitwise
{

    public class Not : UnaryMathExpression
    {

        public Not()
            : base(null)
        {

        }

        public Not(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("not({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return ~(int)firstMathExpression.Calculate(parameters);
        }

        public override IMathExpression Clone()
        {
            return new Not(firstMathExpression.Clone());
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
