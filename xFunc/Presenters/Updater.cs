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
using System.Net;
using System.Security;

namespace xFunc.Presenters
{

    internal class Updater
    {

        private const string CheckUri = "http://xfunc.codeplex.com/releases/";
        private const int CurrentRelease = 618568;
        private string updateUri;
        private bool hasUpdates;

        internal bool CheckUpdates()
        {
            try
            {
                var request = WebRequest.Create(CheckUri);
                var response = request.GetResponse();
                var responseUri = response.ResponseUri.ToString();

                var releaseNumber = responseUri.Substring(responseUri.LastIndexOf('/') + 1);
                var release = int.Parse(releaseNumber);

                if (release > CurrentRelease)
                {
                    updateUri = responseUri;
                    hasUpdates = true;
                }
            }
            catch (SecurityException)
            {
            }
            catch (FormatException)
            {
            }

            return hasUpdates;
        }

        internal bool HasUpdates
        {
            get
            {
                return hasUpdates;
            }
        }

        internal string UpdateUrl
        {
            get
            {
                return updateUri;
            }
        }

    }

}
