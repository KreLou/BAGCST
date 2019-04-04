using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestingClient.Testing
{
    /// <summary>
    /// This Class handle the HTTP-Requst and put all information in ReponseInformation
    /// </summary>
    class HttpRequest
    {
        private Stopwatch _stopwatch;

        public string RequestURL { get; private set; }


        public RequestResponseInformation ResponseInformation { get; private set; }


        public HttpRequest(string url)
        {
            this.RequestURL = url;
            _stopwatch = new Stopwatch();
        }


        /// <summary>
        /// Start the Tast
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            using (HttpClient client = new HttpClient())
            {
                _stopwatch.Start();

                var getTask = client.GetAsync(RequestURL);
                HttpResponseMessage responseMessage = await getTask;

                _stopwatch.Stop();

                fillResponseInformation(responseMessage);

            }
        }

        /// <summary>
        /// Store the information in the ResponseInformation
        /// </summary>
        /// <param name="responseMessage"></param>
        private void fillResponseInformation(HttpResponseMessage responseMessage)
        {
            this.ResponseInformation = new RequestResponseInformation
            {
                ElapsedMiliseconds = _stopwatch.ElapsedMilliseconds,
                HttpContent = responseMessage.Content,
                StatusCode = responseMessage.StatusCode,
                HttpHeader = responseMessage.Headers
            };
        }
    }
}
