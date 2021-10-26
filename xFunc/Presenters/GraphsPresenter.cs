// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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