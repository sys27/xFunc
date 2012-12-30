using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using xFunc.App.Presenters;
using xFunc.App.Views;

namespace xFunc.App
{

    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            MainView mainView = new MainView();
            MainPresenter mainPresenter = new MainPresenter(mainView);
            
            mainView.Show();
        }

    }

}
