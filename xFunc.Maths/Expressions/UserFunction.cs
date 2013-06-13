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
        private int countOfParams;

        public UserFunction()
            : this(null, null, -1)
        {

        }

        public UserFunction(string function, int countOfParams) : this(function, null, countOfParams) { }

        public UserFunction(string function, IMathExpression[] args, int countOfParams)
        {
            this.function = function;
            this.arguments = args;
            this.countOfParams = countOfParams;
        }

        public override bool Equals(object obj)
        {
            var exp = obj as UserFunction;
            if (exp != null && this.function == exp.function && this.countOfParams == exp.countOfParams)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return arguments == null ? function.GetHashCode() : function.GetHashCode() ^ countOfParams.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(function);
            builder.Append('(');
            if (arguments != null)
            {
                for (int i = 0; i < arguments.Length; i++)
                {
                    builder.Append(arguments[i]);
                    builder.Append(',');
                }
                if (arguments.Length > 0)
                    builder.Remove(builder.Length - 1, 1);
            }
            builder.Append(')');

            return builder.ToString();
        }

        public double Calculate()
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var func = functions.Keys.First(uf => uf.Equals(this));
            var newParameters = new MathParameterCollection(parameters);
            for (int i = 0; i < arguments.Length; i++)
            {
                var arg = func.Arguments[i] as Variable;
                newParameters[arg.Name] = this.arguments[i].Calculate(parameters, functions);
            }

            return functions[this].Calculate(newParameters, functions);
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
            return new UserFunction(function, arguments, countOfParams);
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
            set
            {
                arguments = value;
            }
        }

        public int CountOfParams
        {
            get
            {
                return countOfParams;
            }
        }

    }

}
