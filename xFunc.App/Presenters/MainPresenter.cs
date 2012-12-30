using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.App.Views;

namespace xFunc.App.Presenters
{

    public class MainPresenter
    {

        private IMainView view;

        public MainPresenter(IMainView view)
        {
            this.view = view;
        }

    }

}
