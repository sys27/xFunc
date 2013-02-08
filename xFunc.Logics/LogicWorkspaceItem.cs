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
using xFunc.Logics.Expressions;

namespace xFunc.Logics
{

    public class LogicWorkspaceItem
    {

        private string strExp;
        private ILogicExpression exp;
        private string answer;

        public LogicWorkspaceItem()
        {

        }

        public LogicWorkspaceItem(string strExp, ILogicExpression exp)
            : this(strExp, exp, null)
        {

        }

        public LogicWorkspaceItem(string strExp, ILogicExpression exp, string answer)
        {
            this.strExp = strExp;
            this.exp = exp;
            this.answer = answer;
        }

        public string StringExpression
        {
            get
            {
                return strExp;
            }
        }

        public ILogicExpression Expression
        {
            get
            {
                return exp;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }
            internal set
            {
                answer = value;
            }
        }

    }

}
