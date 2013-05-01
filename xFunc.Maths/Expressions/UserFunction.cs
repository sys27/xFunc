using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class UserFunction : IMathExpression
    {

        private string function;
        private IMathExpression[] args;

        public UserFunction()
        {

        }
        
        public UserFunction(string function, IMathExpression[] args)
        {
            this.function = function;
            this.args = args;
        }

        public double Calculate()
        {
            throw new NotImplementedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            throw new NotImplementedException();
        }

        public IMathExpression Differentiate()
        {
            throw new NotImplementedException();
        }

        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotImplementedException();
        }

        public IMathExpression Clone()
        {
            return new UserFunction(function, args);
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

    }

}
