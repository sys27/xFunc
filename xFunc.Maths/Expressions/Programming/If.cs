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

    public class If : DifferentParametersExpression
    {

        internal If() : base(null, -1) { }

        public If(IExpression condition, IExpression then)
            : base(new[] { condition, then }, 2) { }

        public If(IExpression condition, IExpression then, IExpression @else)
            : base(new[] { condition, then, @else }, 3) { }

        public If(IExpression[] arguments, int countOfParams)
            : base(arguments, countOfParams) { }

        public override object Calculate(ExpressionParameters parameters)
        {
            if (Condition.Calculate(parameters).AsBool())
                return Then.Calculate(parameters);

            var @else = Else;
            if (@else == null)
                // todo: ???
                throw new Exception();

            return @else.Calculate(parameters);
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
            return new If(CloneArguments(), countOfParams);
        }

        public override int MinCountOfParams
        {
            get
            {
                return 2;
            }
        }

        public override int MaxCountOfParams
        {
            get
            {
                return 3;
            }
        }

        public IExpression Condition
        {
            get
            {
                return arguments[0];
            }
        }

        public IExpression Then
        {
            get
            {
                return arguments[1];
            }
        }

        public IExpression Else
        {
            get
            {
                return countOfParams == 3 ? arguments[2] : null;
            }
        }

    }

}
