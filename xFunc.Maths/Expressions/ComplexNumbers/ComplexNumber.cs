// Copyright 2012-2016 Dmitry Kischenko
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
using System.Numerics;

namespace xFunc.Maths.Expressions.ComplexNumbers
{

    public class ComplexNumber : IExpression
    {

        private IExpression parent;
        private Complex complex;

        public ComplexNumber(double real, double imaginary)
            : this(new Complex(real, imaginary))
        {
        }

        public ComplexNumber(Complex complex)
        {
            this.complex = complex;
        }

        public static implicit operator Complex(ComplexNumber number)
        {
            return number.complex;
        }

        public static implicit operator ComplexNumber(Complex number)
        {
            return new ComplexNumber(number);
        }

        public override bool Equals(object obj)
        {
            var num = obj as ComplexNumber;
            if (num == null)
                return false;

            return complex.Equals(num.complex);
        }

        public override int GetHashCode()
        {
            return complex.GetHashCode() ^ 6421;
        }

        public override string ToString()
        {
            return complex.ToString();
        }

        public object Execute()
        {
            return complex;
        }

        public object Execute(ExpressionParameters parameters)
        {
            return complex;
        }

        public IExpression Clone()
        {
            return new ComplexNumber(this.complex);
        }

        public Complex Value
        {
            get
            {
                return complex;
            }
        }

        public IExpression Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }

        public int MinParameters
        {
            get
            {
                return 0;
            }
        }

        public int MaxParameters
        {
            get
            {
                return -1;
            }
        }

        public int ParametersCount
        {
            get
            {
                return 0;
            }
        }

        public ExpressionResultType ResultType
        {
            get
            {
                return ExpressionResultType.ComplexNumber;
            }
        }

    }

}
