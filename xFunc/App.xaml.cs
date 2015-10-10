// Copyright 2012-2015 Dmitry Kischenko
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
            AppDomain.CurrentDomain.UnhandledException += (obj, args) => MiniDump.CreateMiniDump();
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
