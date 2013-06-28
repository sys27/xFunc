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

namespace xFunc.Logics.Expressions
{

    public abstract class BinaryLogicExpression : ILogicExpression
    {

        protected ILogicExpression firstOperand;
        protected ILogicExpression secondOperand;

        protected BinaryLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }

        protected string ToString(string operand)
        {
            string first;
            string second;

            if (firstOperand is Variable || firstOperand is Const)
                first = firstOperand.ToString();
            else
                first = "(" + firstOperand + ")";

            if (secondOperand is Variable || secondOperand is Const)
                second = secondOperand.ToString();
            else
                second = "(" + secondOperand + ")";

            return first + " " + operand + " " + second;
        }

        public abstract bool Calculate(LogicParameterCollection parameters);

        public ILogicExpression FirstOperand
        {
            get
            {
                return firstOperand;
            }
            set
            {
                firstOperand = value;
            }
        }

        public ILogicExpression SecondOperand
        {
            get
            {
                return secondOperand;
            }
            set
            {
                secondOperand = value;
            }
        }

    }

}
