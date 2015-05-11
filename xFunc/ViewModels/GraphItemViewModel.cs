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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{

    public class GraphItemViewModel
    {

        private bool isChecked;
        private IExpression exp;
        private DrawingVisual visual;

        public GraphItemViewModel(IExpression exp, bool isChecked, DrawingVisual visual)
        {
            this.exp = exp;
            this.isChecked = isChecked;
            this.visual = visual;
        }

        public override string ToString()
        {
            return exp.ToString();
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
            }
        }

        public IExpression Expression
        {
            get
            {
                return exp;
            }
        }

        public DrawingVisual Visual
        {
            get
            {
                return visual;
            }
            set
            {
                visual = value;
            }
        }

    }

}
