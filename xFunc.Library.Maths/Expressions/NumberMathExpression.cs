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
