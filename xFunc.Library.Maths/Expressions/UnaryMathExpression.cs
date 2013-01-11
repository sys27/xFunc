// Copyright 2012 Dmitry Kischenko
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

        public abstract IMathExpression Clone();

        public IMathExpression Derivative()
        {
            return Derivative(new VariableMathExpression('x'));
        }

        protected abstract IMathExpression _Derivative(VariableMathExpression variable);

        public IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
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
