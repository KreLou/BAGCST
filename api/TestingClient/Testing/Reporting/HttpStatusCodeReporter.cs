using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace TestingClient.Testing.Reporting
{
    class HttpStatusCodeReporter
    {
        private List<KeyValuePair<HttpStatusCode, long>> statusCodeList;

        public HttpStatusCodeReporter(HttpStatusCode[] codes)
        {
            statusCodeList = new List<KeyValuePair<HttpStatusCode, long>>();
            
            var query = codes.GroupBy(x => x, (code, items) => new {
                Key = code,
                Count = items.Count()
            });
            foreach (var result in query)
            {
                statusCodeList.Add(new KeyValuePair<HttpStatusCode, long>(result.Key, result.Count));
            }
        }

        public override string ToString()
        {
            string returnString = "";

            var last = statusCodeList.Last();

            foreach(KeyValuePair<HttpStatusCode, long> kvp in statusCodeList)
            {
                returnString += formatString(kvp.Key, kvp.Value);

                if (!kvp.Equals(last)) returnString += "\n";
            }

            return returnString;
        }

        private string formatString(HttpStatusCode key, long value)
        {
            return formatString(key.ToString(), value.ToString());
        }

        private string formatString(string element1, string element2)
        {
            return String.Format("{0, -10} {1,5}", element1, element2);
        }
    }
}
