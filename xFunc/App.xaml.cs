// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Windows;
using xFunc.Properties;
using xFunc.Views;

namespace xFunc
{

    public partial class App : Application
    {

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += (obj, args) =>
            {
                if (Settings.Default.SaveDump)
                    MiniDump.CreateMiniDump();
            };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var lang = Settings.Default.Lang;
            if (lang != "Auto")
            {
                var cultureInfo = CultureInfo.GetCultureInfo(lang);
                this.Dispatcher.Thread.CurrentCulture = cultureInfo;
                this.Dispatcher.Thread.CurrentUICulture = cultureInfo;
            }

            MainView mainView = new MainView();
            mainView.Show();
        }

    }

}