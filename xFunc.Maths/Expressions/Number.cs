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

    public class Number : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private double number;

        public Number(double number)
        {
            this.number = number;
        }

        public static implicit operator double(Number number)
        {
            return number.Value;
        }

        public static implicit operator Number(double number)
        {
            return new Number(number);
        }

        public override bool Equals(object obj)
        {
            Number num = obj as Number;
            if (num == null)
                return false;

            return number == num.Value;
        }

        public override string ToString()
        {
            return number.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public double Calculate()
        {
            return number;
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return number;
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return number;
        }

        public IMathExpression Differentiate()
        {
            return new Number(0);
        }

        public IMathExpression Differentiate(Variable variable)
        {
            return new Number(0);
        }

        public IMathExpression Clone()
        {
            return new Number(number);
        }

        public double Value
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
