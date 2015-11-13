// Copyright 2012-2015 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths
{

    public class Builder : IExpression
    {

        private IExpression current;

        public Builder()
        {

        }

        public Builder(IExpression initial)
        {
            Init(initial);
        }

        public Builder(double number)
        {
            Init(number);
        }

        public Builder(string variable)
        {
            Init(variable);
        }

        public override string ToString()
        {
            return current.ToString();
        }

        public static Builder Create(IExpression initial)
        {
            return new Builder(initial);
        }

        public static Builder Create(double number)
        {
            return new Builder(number);
        }

        public static Builder Create(string variable)
        {
            return new Builder(variable);
        }

        public void Init(IExpression initial)
        {
            this.current = initial;
        }

        public void Init(double number)
        {
            Init((IExpression)new Number(number));
        }

        public void Init(string variable)
        {
            Init((IExpression)new Variable(variable));
        }

        private void CheckCurrentExpression()
        {
            // todo: ???
            if (current == null)
                throw new ArgumentNullException(nameof(current));
        }

        public Builder Expression(Func<IExpression, IExpression> customExpression)
        {
            CheckCurrentExpression();

            current = customExpression(current);

            return this;
        }

        public Builder Add(IExpression summand)
        {
            CheckCurrentExpression();

            current = new Add(current, summand);

            return this;
        }

        public Builder Add(double summand)
        {
            return Add((IExpression)new Number(summand));
        }

        public Builder Add(string summand)
        {
            return Add((IExpression)new Variable(summand));
        }

        public Builder Sub(IExpression subtrahend)
        {
            CheckCurrentExpression();

            current = new Sub(current, subtrahend);

            return this;
        }

        public Builder Sub(double subtrahend)
        {
            return Sub((IExpression)new Number(subtrahend));
        }

        public Builder Sub(string subtrahend)
        {
            return Sub((IExpression)new Variable(subtrahend));
        }

        public Builder Mul(IExpression factor)
        {
            CheckCurrentExpression();

            current = new Mul(current, factor);

            return this;
        }

        public Builder Mul(double factor)
        {
            return Mul((IExpression)new Number(factor));
        }

        public Builder Mul(string factor)
        {
            return Mul((IExpression)new Variable(factor));
        }

        public Builder Div(IExpression denominator)
        {
            CheckCurrentExpression();

            current = new Div(current, denominator);

            return this;
        }

        public Builder Div(double denominator)
        {
            return Div((IExpression)new Number(denominator));
        }

        public Builder Div(string denominator)
        {
            return Div((IExpression)new Variable(denominator));
        }

        public Builder Pow(IExpression exponent)
        {
            CheckCurrentExpression();

            current = new Pow(current, exponent);

            return this;
        }

        public Builder Pow(double exponent)
        {
            return Pow((IExpression)new Number(exponent));
        }

        public Builder Pow(string exponent)
        {
            return Pow((IExpression)new Variable(exponent));
        }

        public Builder Sqrt()
        {
            CheckCurrentExpression();

            current = new Sqrt(current);

            return this;
        }

        public Builder Root(IExpression degree)
        {
            CheckCurrentExpression();

            current = new Root(current, degree);

            return this;
        }

        public Builder Root(double degree)
        {
            return Root((IExpression)new Number(degree));
        }

        public Builder Root(string degree)
        {
            return Root((IExpression)new Variable(degree));
        }

        #region Trigonometric

        public Builder Sin(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Sin(current);

            return this;
        }

        public Builder Sin(double number)
        {
            return Sin((IExpression)new Number(number));
        }

        public Builder Sin(string variable)
        {
            return Sin((IExpression)new Variable(variable));
        }

        public Builder Cos(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Cos(current);

            return this;
        }

        public Builder Cos(double number)
        {
            return Cos((IExpression)new Number(number));
        }

        public Builder Cos(string variable)
        {
            return Cos((IExpression)new Variable(variable));
        }

        public Builder Tan(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Tan(current);

            return this;
        }

        public Builder Tan(double number)
        {
            return Tan((IExpression)new Number(number));
        }

        public Builder Tan(string variable)
        {
            return Tan((IExpression)new Variable(variable));
        }

        public Builder Cot(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Cot(current);

            return this;
        }

        public Builder Cot(double number)
        {
            return Cot((IExpression)new Number(number));
        }

        public Builder Cot(string variable)
        {
            return Cot((IExpression)new Variable(variable));
        }

        public Builder Sec(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Sec(current);

            return this;
        }

        public Builder Sec(double number)
        {
            return Sec((IExpression)new Number(number));
        }

        public Builder Sec(string variable)
        {
            return Sec((IExpression)new Variable(variable));
        }

        public Builder Csc(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Csc(current);

            return this;
        }

        public Builder Csc(double number)
        {
            return Csc((IExpression)new Number(number));
        }

        public Builder Csc(string variable)
        {
            return Csc((IExpression)new Variable(variable));
        }

        #endregion Trigonometric

        #region Hyperbolic

        public Builder Arcsin(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Arcsin(current);

            return this;
        }

        public Builder Arcsin(double number)
        {
            return Arcsin((IExpression)new Number(number));
        }

        public Builder Arcsin(string variable)
        {
            return Arcsin((IExpression)new Variable(variable));
        }

        public Builder Arccos(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Arccos(current);

            return this;
        }

        public Builder Arccos(double number)
        {
            return Arccos((IExpression)new Number(number));
        }

        public Builder Arccos(string variable)
        {
            return Arccos((IExpression)new Variable(variable));
        }

        public Builder Arctan(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Arctan(current);

            return this;
        }

        public Builder Arctan(double number)
        {
            return Arctan((IExpression)new Number(number));
        }

        public Builder Arctan(string variable)
        {
            return Arctan((IExpression)new Variable(variable));
        }

        public Builder Arccot(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Arccot(current);

            return this;
        }

        public Builder Arccot(double number)
        {
            return Arccot((IExpression)new Number(number));
        }

        public Builder Arccot(string variable)
        {
            return Arccot((IExpression)new Variable(variable));
        }

        public Builder Arcsec(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Arcsec(current);

            return this;
        }

        public Builder Arcsec(double number)
        {
            return Arcsec((IExpression)new Number(number));
        }

        public Builder Arcsec(string variable)
        {
            return Arcsec((IExpression)new Variable(variable));
        }

        public Builder Arccsc(IExpression expression)
        {
            CheckCurrentExpression();

            current = new Arccsc(current);

            return this;
        }

        public Builder Arccsc(double number)
        {
            return Arccsc((IExpression)new Number(number));
        }

        public Builder Arccsc(string variable)
        {
            return Csc((IExpression)new Variable(variable));
        }

        #endregion Hyperbolic

        #region IExpression

        public object Calculate()
        {
            return current.Calculate();
        }

        public object Calculate(ExpressionParameters parameters)
        {
            return current.Calculate(parameters);
        }

        public IExpression Clone()
        {
            return current.Clone();
        }

        public IExpression Parent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MinParameters
        {
            get
            {
                return current.MinParameters;
            }
        }

        public int MaxParameters
        {
            get
            {
                return current.MaxParameters;
            }
        }

        public int ParametersCount
        {
            get
            {
                return current.ParametersCount;
            }
        }

        public ExpressionResultType ResultType
        {
            get
            {
                return current.ResultType;
            }
        }

        #endregion

    }

}
