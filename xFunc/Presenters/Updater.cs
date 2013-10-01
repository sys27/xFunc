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
