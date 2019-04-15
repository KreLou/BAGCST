using Microsoft.Extensions.DependencyInjection;
using System;
using TestingClient.Testing;
using TestingClient.Testing.Configuration;
using TestingClient.Testing.Configuration.WaitingMethod;
using TestingClient.Testing.Performance.Contacts;
using TestingClient.Testing.Performance.News;
using TestingClient.Testing.Performance.Timetable;
using TestingClient.Testing.Performance.Postgroup;
using TestingClient.Testing.Reporting;

namespace TestingClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to BAGCST-Backend Tester.");
            Console.WriteLine("Please choose your test: Contacts/News/Timetable/Postgroup (or press enter to do all) ");
            string answer = Console.ReadLine();

            TestConditions testConditions = new TestConditions { Iterations = 100, WaitingMethod = new RandomWaiting { MinDelay = 100, MaxDelay = 200 } };

            //TODO Implement Tester-Switch, depends on UserInput


            ContactsPerformanceTesting contactsTesting = new ContactsPerformanceTesting { TestConditions = testConditions };
            NewsPerformanceTesting newsTesting = new NewsPerformanceTesting { TestConditions = testConditions };
            TimetablePerformanceTesting timetableTesting = new TimetablePerformanceTesting { TestConditions = testConditions };
            PostgroupPerformanceTesting postgroupTesting = new PostgroupPerformanceTesting { TestConditions = testConditions };

            switch (answer)
            {
                case "Contacts":
                    contactsTesting.PerfomTest().Wait();
                    TestReport contactsReport = contactsTesting.generateTestReport();
                    contactsTesting.ExportToFileSystem();
                    Console.WriteLine(contactsReport.ToString()); break;

                case "News":
                    newsTesting.PerfomTest().Wait();
                    TestReport newsReport = newsTesting.generateTestReport();
                    Console.WriteLine(newsReport.ToString()); break;

                case "Timetable":
                    timetableTesting.PerfomTest().Wait();
                    TestReport timetableReport = timetableTesting.generateTestReport();
                    Console.WriteLine(timetableReport.ToString()); break;

                case "Postgroup":
                    postgroupTesting.PerfomTest().Wait();
                    TestReport postgroupReport = postgroupTesting.generateTestReport();
                    Console.WriteLine(postgroupReport.ToString()); break;

                default:
                    contactsTesting.PerfomTest().Wait();
                    TestReport allContactsReport = contactsTesting.generateTestReport();
                    Console.WriteLine(allContactsReport.ToString());

                    newsTesting.PerfomTest().Wait();
                    TestReport allNewsReport = newsTesting.generateTestReport();
                    Console.WriteLine(allNewsReport.ToString());

                    timetableTesting.PerfomTest().Wait();
                    TestReport allTimetableReport = timetableTesting.generateTestReport();
                    Console.WriteLine(allTimetableReport.ToString());

                    postgroupTesting.PerfomTest().Wait();
                    TestReport allPostgroupReport = postgroupTesting.generateTestReport();
                    Console.WriteLine(allPostgroupReport.ToString());
                    break;      
            }

            Console.ReadLine();
            
        }
    }
}
