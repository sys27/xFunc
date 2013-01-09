using System;

namespace xFunc.Library.Maths.Expressions
{

    public class NumberMathExpression : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private double number;

        public NumberMathExpression(double number)
        {
            this.number = number;
        }

        public override bool Equals(object obj)
        {
            NumberMathExpression num = obj as NumberMathExpression;
            if (num == null)
                return false;

            return number == num.Number;
        }

        public override string ToString()
        {
            return number.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return number;
        }

        public IMathExpression Derivative()
        {
            return new NumberMathExpression(0);
        }

        public IMathExpression Derivative(VariableMathExpression variable)
        {
            return new NumberMathExpression(0);
        }

        public IMathExpression Clone()
        {
            return new NumberMathExpression(number);
        }

        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
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
