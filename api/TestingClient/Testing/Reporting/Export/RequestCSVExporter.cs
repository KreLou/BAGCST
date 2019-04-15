using System;
using System.Collections.Generic;
using System.Text;

namespace TestingClient.Testing.Reporting.Export
{
    class RequestCSVExporter: FileExporter
    {
        private readonly DirectoryExporter directoryExporter;
        public RequestCSVExporter(DirectoryExporter directory): base(directory, "request-responses.csv")
        {
            directoryExporter = directory;
        }

        public void export(RequestResponseInformation[] information)
        {

            for(int i = 0; i< information.Length; i++)
            {
                RequestResponseInformation info = information[i];
                string line = $"{i};{info.StatusCode};{info.HttpContent.Headers.ContentLength};{info.ElapsedMiliseconds};{info.HttpHeader.Date}";
                base.WriteLine(line);
            }
        }
    }
}
