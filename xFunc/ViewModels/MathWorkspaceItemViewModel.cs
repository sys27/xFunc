// Copyright 2012-2020 Dmytro Kyshchenko
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
using xFunc.Maths.Results;
using xFunc.Presenters;

namespace xFunc.ViewModels
{

    public class MathWorkspaceItemViewModel
    {

        private int index;
        private MathWorkspaceItem item;

        public MathWorkspaceItemViewModel(int index, MathWorkspaceItem item)
        {
            this.index = index;
            this.item = item;
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public MathWorkspaceItem Item
        {
            get
            {
                return item;
            }
        }

        public string StringExpression
        {
            get
            {
                return item.StringExpression;
            }
        }

        public IResult Result
        {
            get
            {
                return item.Result;
            }
        }

        public string Answer
        {
            get
            {
                return item.Answer;
            }
        }

    }

}