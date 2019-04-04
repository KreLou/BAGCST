using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TestingClient.Testing
{
    class RequestResponseInformation
    {
        public HttpStatusCode StatusCode { get; set; }
        public long ElapsedMiliseconds { get; set; }

        public HttpContent HttpContent { get; set; }

        public HttpResponseHeaders HttpHeader { get; set; }
    }
}
