// Copyright 2012 Dmitry Kischenko
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
using xFunc.Views;
using xFunc.Logics;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Presenters
{

    public class MainPresenter
    {

        private IMainView view;

        private MathWorkspace mathWorkspace;
        private LogicWorkspace logicWorkspace;

        public MainPresenter(IMainView view)
        {
            this.view = view;
            this.mathWorkspace = new MathWorkspace();
            this.logicWorkspace = new LogicWorkspace();
        }

        public void AddMathExpression(string strExp)
        {
            mathWorkspace.Add(strExp);

            view.MathExpressions = mathWorkspace.Expressions;
        }

        public void AddLogicExpression(string strExp)
        {
            logicWorkspace.Add(strExp);

            view.LogicExpressions = logicWorkspace.Expressions;
        }

        public MathWorkspace MathWorkspace
        {
            get
            {
                return mathWorkspace;
            }
        }

        public LogicWorkspace LogicWorkspace
        {
            get
            {
                return logicWorkspace;
            }
        }

        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return mathWorkspace.Parser.AngleMeasurement;
            }
            set
            {
                mathWorkspace.Parser.AngleMeasurement = value;
            }
        }

    }

}
