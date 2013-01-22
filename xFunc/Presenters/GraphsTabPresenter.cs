using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class GraphsTabPresenter
    {

        private IMainView view;

        private MathParser parser;
        private List<IMathExpression> listOfGraphs;

        public GraphsTabPresenter(IMainView view)
        {
            this.view = view;

            this.parser = new MathParser() { AngleMeasurement = AngleMeasurement.Radian };
            this.listOfGraphs = new List<IMathExpression>();
        }

        public void Add(string strExp)
        {
            listOfGraphs.Add(parser.Parse(strExp));

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(IMathExpression exp)
        {
            listOfGraphs.Remove(exp);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

    }

}
