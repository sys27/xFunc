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

    public abstract class UnaryLogicExpression : ILogicExpression
    {

        protected ILogicExpression firstMathExpression;

        public UnaryLogicExpression(ILogicExpression firstMathExpression)
        {
            this.firstMathExpression = firstMathExpression;
        }

        protected string ToString(string operand)
        {
            if (firstMathExpression is VariableLogicExpression)
                return string.Format("{0}{1}", operand, firstMathExpression);

            return string.Format("{0}({1})", operand, firstMathExpression);
        }

        public abstract bool Calculate(LogicParameterCollection parameters);

        public ILogicExpression FirstMathExpression
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
