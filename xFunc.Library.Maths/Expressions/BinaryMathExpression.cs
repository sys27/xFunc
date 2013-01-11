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
namespace xFunc.Library.Maths.Expressions
{

    public abstract class BinaryMathExpression : IMathExpression
    {

        protected IMathExpression parentMathExpression;
        protected IMathExpression firstMathExpression;
        protected IMathExpression secondMathExpression;

        public BinaryMathExpression(IMathExpression firstOperand, IMathExpression secondOperand)
        {
            this.FirstMathExpression = firstOperand;
            this.SecondMathExpression = secondOperand;
        }

        protected string ToString(string format)
        {
            return string.Format(format, firstMathExpression.ToString(), secondMathExpression.ToString());
        }

        public abstract double Calculate(MathParameterCollection parameters);

        public abstract IMathExpression Clone();

        public IMathExpression Derivative()
        {
            return Derivative(new VariableMathExpression('x'));
        }

        public abstract IMathExpression Derivative(VariableMathExpression variable);

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

        public IMathExpression SecondMathExpression
        {
            get
            {
                return secondMathExpression;
            }
            set
            {
                secondMathExpression = value;
                if (secondMathExpression != null)
                    secondMathExpression.Parent = this;
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
