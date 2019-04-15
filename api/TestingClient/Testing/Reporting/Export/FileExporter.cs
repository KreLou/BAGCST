using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestingClient.Testing.Reporting.Export
{
    class FileExporter
    {
        private readonly DirectoryExporter directoryExporter;

        private readonly string filename;

        public string FullPath { get { return Path.Combine(directoryExporter.FullPath, filename); } }
        public FileExporter(DirectoryExporter directory, string filename)
        {
            this.directoryExporter = directory;
            this.filename = filename;

            if (!File.Exists(FullPath))
            {
                using (StreamWriter sw = new StreamWriter(FullPath))
                {
                    sw.Write(String.Empty);
                }

                Console.WriteLine("File Created: " + FullPath);
            }
        }

        protected void WriteLine(string line)
        {
            using (StreamWriter sw = new StreamWriter(FullPath, true))
            {
                sw.WriteLine(line );
            }
        }
    }
}
