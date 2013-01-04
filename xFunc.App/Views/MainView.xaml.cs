using Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using xFunc.App.Presenters;

namespace xFunc.App.Views
{

    public partial class MainView : RibbonWindow, IMainView
    {

        private MainPresenter presenter;

        public MainView()
        {
            this.presenter = new MainPresenter(this);

            InitializeComponent();

            expressionBox.Focus();
        }

        public string Expression
        {
            get
            {
                return expressionBox.Text;
            }
        }

    }

}
