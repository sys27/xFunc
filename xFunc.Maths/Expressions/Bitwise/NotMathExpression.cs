using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Bitwise
{

    public class NotMathExpression : UnaryMathExpression
    {

        public NotMathExpression()
            : base(null)
        {

        }

        public NotMathExpression(IMathExpression firstMathExpression)
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
            return new NotMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

    }

}
