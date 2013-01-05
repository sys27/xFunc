using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.App.Views;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.App.Presenters
{

    public class MainPresenter
    {

        private IMainView view;

        private MathWorkspace mathWorkspace;

        public MainPresenter(IMainView view)
        {
            this.view = view;
            this.mathWorkspace = new MathWorkspace();
        }

        public void SetAngleMeasurement(AngleMeasurement angle)
        {
            mathWorkspace.Parser.AngleMeasurement = angle;
        }

    }

}
