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

        public UserFunction(string function) : this(function, null) { }

        public UserFunction(string function, IMathExpression[] args)
        {
            this.function = function;
            this.arguments = args;
        }

        public override bool Equals(object obj)
        {
            var exp = obj as UserFunction;
            if (exp != null && this.function == exp.Function)
            {
                if (this.arguments != null && exp.Arguments != null && this.arguments.Length == exp.arguments.Length)
                    return true;
                if (this.arguments == null && exp.Arguments == null)
                    return true;

                return false;
            }

            return false;
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
            // todo: ...
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            // todo: ...
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var func = functions.Keys.First(uf => uf.Equals(this));
            var newParameters = new MathParameterCollection(parameters);
            for (int i = 0; i < arguments.Length; i++)
            {
                var arg = func.Arguments[i] as Variable;
                newParameters[arg.Character] = this.arguments[i].Calculate(parameters, functions);
            }

            var result = functions[this].Calculate(newParameters, functions);

            return result;
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
            set
            {
                arguments = value;
            }
        }

    }

}
