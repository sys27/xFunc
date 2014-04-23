// Copyright 2012-2014 Dmitry Kischenko
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

    /// <summary>
    /// Checks updates of this program.
    /// </summary>
    internal class Updater
    {

        private const string checkUri = "http://xfunc.codeplex.com/releases/";
        private const int currentRelease = 121291;
        private string updateUri;
        private bool hasUpdates;

        /// <summary>
        /// Initializes a new instance of the <see cref="Updater"/> class.
        /// </summary>
        internal Updater()
        {
        }

        /// <summary>
        /// Checks the updates.
        /// </summary>
        /// <returns>The url to download a new release.</returns>
        internal bool CheckUpdates()
        {
            try
            {
                var request = WebRequest.Create(checkUri);
                var response = request.GetResponse();
                var responseUri = response.ResponseUri.ToString();

                var releaseNumber = responseUri.Substring(responseUri.LastIndexOf('/') + 1);
                var release = int.Parse(releaseNumber);

                if (release > currentRelease)
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
