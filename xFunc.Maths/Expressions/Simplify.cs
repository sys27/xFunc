using System;

namespace xFunc.Maths.Expressions
{
    
    public class Simplify  : IMathExpression
    {

        private IMathExpression firstMathExpression;

        public Simplify() { }

        public Simplify(IMathExpression firstMathExpression)
        {
            this.firstMathExpression = firstMathExpression;
        }

        public override bool Equals(object obj)
        {
            var simp = obj as Simplify;
            if (simp != null && firstMathExpression.Equals(simp.firstMathExpression))
                return true;

            return false;
        }

        public override string ToString()
        {
            return string.Format("simplify({0})", firstMathExpression.ToString());
        }

        public double Calculate()
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Clone()
        {
            return new Simplify(firstMathExpression.Clone());
        }

        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
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
            }
        }

    }

}
