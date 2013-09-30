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
        private const int currentRelease = 112799;

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
        internal string CheckUpdates()
        {
            string url = null;

            try
            {
                var request = WebRequest.Create(checkUri);
                var response = request.GetResponse();
                var responseUri = response.ResponseUri.ToString();

                var releaseNumber = responseUri.Substring(responseUri.LastIndexOf('/') + 1);
                var release = int.Parse(releaseNumber);

                if (release > currentRelease)
                    url = responseUri;
            }
            catch (SecurityException)
            {
            }
            catch (FormatException)
            {
            }

            return url;
        }

    }

}
