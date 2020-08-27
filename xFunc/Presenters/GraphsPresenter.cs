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

using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Properties;
using xFunc.ViewModels;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class GraphsPresenter
    {

        private readonly IGraphsView view;

        private readonly Processor processor;
        private readonly int countOfGraphs;
        private readonly List<GraphItemViewModel> listOfGraphs;

        public GraphsPresenter(IGraphsView view, Processor processor)
        {
            this.view = view;

            this.processor = processor;
            countOfGraphs = Settings.Default.MaxCountOfExpressions >= 20 ? 20 : Settings.Default.MaxCountOfExpressions;
            listOfGraphs = new List<GraphItemViewModel>(countOfGraphs);
        }

        public void Add(string strExp)
        {
            var exp = processor.Parse(strExp);

            listOfGraphs.Add(new GraphItemViewModel(exp, true, null, Settings.Default.DefaultChartColor));

            while (listOfGraphs.Count > countOfGraphs)
                listOfGraphs.RemoveAt(0);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(GraphItemViewModel exp)
        {
            listOfGraphs.Remove(exp);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(int index)
        {
            listOfGraphs.RemoveAt(index);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Clear()
        {
            listOfGraphs.Clear();

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public int CountOfGraphs => listOfGraphs.Count;

    }

}