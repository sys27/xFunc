using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Bitwise
{

    public class OrMathExpression : BinaryMathExpression
    {

        public OrMathExpression()
            : base(null, null)
        {

        }

        public OrMathExpression(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} or {1})");
            }

            return ToString("{0} or {1}");
        }
        
        public override double Calculate(MathParameterCollection parameters)
        {
            return (int)firstMathExpression.Calculate(parameters) | (int)secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Clone()
        {
            return new OrMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

    }

}
