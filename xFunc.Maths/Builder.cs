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

namespace xFunc.Maths
{

    public class Builder : IExpression
    {

        private IExpression current;

        public Builder(IExpression initial)
        {
            this.current = initial;
        }

        public Builder Create(IExpression initial)
        {
            return new Builder(initial);
        }

        public Builder Add(IExpression summand)
        {
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
            current = new Sqrt(current);

            return this;
        }

        public Builder Root(IExpression degree)
        {
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

        public Builder Expression(Func<IExpression, IExpression> customExpression)
        {
            current = customExpression(current);

            return this;
        }

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
