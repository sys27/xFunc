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

namespace xFunc.Maths.Expressions.Programming
{

    public class Or : BinaryExpression
    {

        internal Or() { }

        public Or(IExpression left, IExpression right)
            : base(left, right) { }

        public override object Calculate(ExpressionParameters parameters)
        {
            return left.Calculate(parameters).AsBool() || right.Calculate(parameters).AsBool();
        }

        public override IExpression Clone()
        {
            return new And(left.Clone(), right.Clone());
        }

        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
