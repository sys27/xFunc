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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Matrices
{
    
    public class Invert : UnaryExpression
    {

        internal Invert() { }

        public Invert(IExpression argument)
            : base(argument)
        {

        }

        public override object Calculate(ExpressionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public override IExpression Clone()
        {
            return new Invert(this.argument.Clone());
        }

        protected override IExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

        public override IExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                if (value != null)
                {
                    if (!(value is Vector || value is Matrix))
                        throw new NotSupportedException();

                    value.Parent = this;
                }

                argument = value;
            }
        }

    }

}
