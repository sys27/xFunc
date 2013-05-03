// Copyright 2012-2013 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The abstract base class that represents the unary operation.
    /// </summary>
    public abstract class UnaryMathExpression : IMathExpression
    {

        protected IMathExpression parentMathExpression;
        protected IMathExpression firstMathExpression;

        public UnaryMathExpression(IMathExpression firstMathExpression)
        {
            FirstMathExpression = firstMathExpression;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            UnaryMathExpression exp = obj as UnaryMathExpression;
            return firstMathExpression.Equals(exp.FirstMathExpression);
        }

        protected string ToString(string format)
        {
            return string.Format(format, firstMathExpression);
        }

        public abstract double Calculate();

        public abstract double Calculate(MathParameterCollection parameters);

        public abstract double Calculate(MathParameterCollection parameters, MathFunctionCollection functions);

        public abstract IMathExpression Clone();

        public IMathExpression Differentiate()
        {
            return Differentiate(new Variable("x"));
        }

        public IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                return _Differentiation(variable);
            }

            return new Number(0);
        }

        protected abstract IMathExpression _Differentiation(Variable variable);

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
