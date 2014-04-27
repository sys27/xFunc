// Copyright 2012-2014 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Programming
{

    public class For : DifferentParametersExpression
    {

        internal For() : base(null, -1) { }

        public For(IExpression[] arguments, int countOfParams)
            : base(arguments, countOfParams) { }

        public override object Calculate(ExpressionParameters parameters)
        {
            for (Initialization.Calculate(parameters); Condition.Calculate(parameters).AsBool(); Iteration.Calculate(parameters))
                Body.Calculate(parameters);

            return null;
        }

        public override IExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        public override IExpression Clone()
        {
            return new For(CloneArguments(), countOfParams);
        }

        public override int MinCountOfParams
        {
            get
            {
                return 4;
            }
        }

        public override int MaxCountOfParams
        {
            get
            {
                return 4;
            }
        }

        public IExpression Body
        {
            get
            {
                return arguments[0];
            }
        }

        public IExpression Initialization
        {
            get
            {
                return arguments[1];
            }
        }

        public IExpression Condition
        {
            get
            {
                return arguments[2];
            }
        }

        public IExpression Iteration
        {
            get
            {
                return arguments[3];
            }
        }

    }

}
