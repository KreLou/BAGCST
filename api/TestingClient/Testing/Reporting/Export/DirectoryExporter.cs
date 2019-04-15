using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestingClient.Testing.Performance.Abstract;

namespace TestingClient.Testing.Reporting.Export
{
    class DirectoryExporter
    {
        /// <summary>
        /// Parentfolder is in User/Documents/BAGCST-TestingClient
        /// </summary>
        private string ParentFolder { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BAGCST-TestingClient"); } }
        public string DirectoryName { get; private set; }

        public string FullPath { get { return Path.Combine(ParentFolder, DirectoryName); } }

        private readonly PerformanceTesting perfomanceTest;

        public DirectoryExporter(PerformanceTesting test)
        {
            perfomanceTest = test;

            setDirctoryName();

            checkifCreatedAndCreate();
        }

        private void setDirctoryName()
        {
            this.DirectoryName = $"{perfomanceTest.StartTime.ToString("yyyy-MM-dd hhmmss")} - {perfomanceTest.TestName}";
        }

        private void checkifCreatedAndCreate()
        {
            if (!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(FullPath);
                Console.WriteLine("Directory created: " + FullPath);
            }
        }
    }
}
