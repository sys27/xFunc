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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class UserFunction : IMathExpression
    {

        private string function;
        private IMathExpression[] arguments;

        public UserFunction()
        {

        }

        public UserFunction(string function, IMathExpression[] args)
        {
            this.function = function;
            this.arguments = args;
        }

        public double Calculate()
        {
            throw new NotImplementedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            throw new NotImplementedException();
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate()
        {
            throw new NotImplementedException();
        }

        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotImplementedException();
        }

        public IMathExpression Clone()
        {
            return new UserFunction(function, arguments);
        }

        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public string Function
        {
            get
            {
                return function;
            }
        }

        public IMathExpression[] Arguments
        {
            get
            {
                return arguments;
            }
        }

    }

}
