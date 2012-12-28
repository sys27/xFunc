using System;

namespace xFunc.Library.Maths.Expressions
{

    public abstract class UnaryMathExpression : IMathExpression
    {

        protected IMathExpression parentMathExpression;
        protected IMathExpression firstMathExpression;

        public UnaryMathExpression(IMathExpression firstMathExpression)
        {
            this.FirstMathExpression = firstMathExpression;
        }

        protected string ToString(string format)
        {
            return string.Format(format, firstMathExpression.ToString());
        }

        public abstract double Calculate(MathParameterCollection parameters);

        public IMathExpression Derivative()
        {
            return Derivative(new VariableMathExpression('x'));
        }

        protected abstract IMathExpression _Derivative(VariableMathExpression variable);

        public IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HaveVar(firstMathExpression, variable))
            {
                return _Derivative(variable);
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public IMathExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
                if (firstMathExpression != null)
                    firstMathExpression.Parent = this;
            }
        }

        public IMathExpression Parent
        {
            get
            {
                return parentMathExpression;
            }
            set
            {
                parentMathExpression = value;
            }
        }

    }

}
