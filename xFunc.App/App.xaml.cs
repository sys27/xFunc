using System;
using System.Windows;
using xFunc.App.View;
using xFunc.App.ViewModel;

namespace xFunc.App
{

    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            MainView mainView = new MainView();
            OperationsView mathOperationsView = new OperationsView();
            MainViewModel mainViewModel = new MainViewModel { View = mainView };
            mainView.DataContext = mainViewModel;
            mathOperationsView.DataContext = mainViewModel;

            this.MainWindow = mainView;
            this.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            mainView.Show();
        }

    }

}
