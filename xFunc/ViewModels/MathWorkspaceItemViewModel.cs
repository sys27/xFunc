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

using xFunc.Maths.Results;
using xFunc.Presenters;

namespace xFunc.ViewModels
{

    public class MathWorkspaceItemViewModel
    {
        public MathWorkspaceItemViewModel(int index, MathWorkspaceItem item)
        {
            this.Index = index;
            this.Item = item;
        }

        public int Index { get; }

        public MathWorkspaceItem Item { get; }

        public string StringExpression => Item.StringExpression;

        public IResult Result => Item.Result;

        public string Answer => Item.Answer;

    }

}