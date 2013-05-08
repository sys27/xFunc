using System;

namespace xFunc.Maths.Expressions
{

    public class Undefine : IMathExpression
    {

        private IMathExpression key;

        public Undefine()
            : this(null)
        {

        }

        public Undefine(Variable key)
        {
            this.Key = key;
        }

        public double Calculate()
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (key is Variable)
            {
                var e = key as Variable;

                parameters.Remove(e.Character);
            }
            else
            {
                throw new NotSupportedException();
            }

            return double.NaN;
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            if (functions == null)
                throw new ArgumentNullException("functions");

            if (key is Variable)
            {
                var e = key as Variable;

                parameters.Remove(e.Character);
            }
            else if (key is UserFunction)
            {
                var e = key as UserFunction;

                functions.Remove(e);
            }

            return double.NaN;
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
            return new Undefine((Variable)key.Clone());
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

        public IMathExpression Key
        {
            get
            {
                return key;
            }
            set
            {
                if (value != null && !(value is Variable || value is UserFunction))
                {
                    throw new NotSupportedException();
                }

                key = value;
            }
        }

    }

}
