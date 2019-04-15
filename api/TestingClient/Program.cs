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
using TestingClient.Testing.Performance.Abstract;
using TestingClient.Testing.Reporting.Export;

namespace TestingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            Console.WriteLine("Welcome to BAGCST-Backend Tester.");

            do
            {
                Console.WriteLine("Please choose your test: Contacts/News/Timetable/Postgroup (or press enter to do all) ");
                string answer = Console.ReadLine().ToLower().Trim(); //change to lower chase and remove spaces bevor and end

                TestConditions testConditions = new TestConditions { Iterations = 100, WaitingMethod = new RandomWaiting { MinDelay = 100, MaxDelay = 200 } };

                //TODO Implement Tester-Switch, depends on UserInput



                switch (answer)
                {
                    case "contacts":
                        handleContactsTesting(testConditions); break;

                    case "news":
                        handleNewsTesting(testConditions); break;

                    case "timetable":
                        handleTimetableTesting(testConditions); break;

                    case "postgroup":
                        handlePostgroupTesting(testConditions); break;

                    default:
                        handleContactsTesting(testConditions);
                        handleTimetableTesting(testConditions);
                        handlePostgroupTesting(testConditions);
                        handleNewsTesting(testConditions);
                        break;
                }

                //Set the run to the UserInput
                run = UserInputIsTrue("Would you like to tun an another test?");
            } while (run);

            Console.WriteLine("Thanks for you usage. Please click any key to close the windows.");
            Console.ReadKey(true);
            
        }

        private static void handlePostgroupTesting(TestConditions testConditions)
        {
            runTest(new PostgroupPerformanceTesting { TestConditions = testConditions });
        }

        private static void handleTimetableTesting(TestConditions testConditions)
        {
            runTest(new TimetablePerformanceTesting { TestConditions = testConditions });
        }

        private static void handleNewsTesting(TestConditions testConditions)
        {
            runTest(new NewsPerformanceTesting { TestConditions = testConditions });
        }

        private static void handleContactsTesting(TestConditions testConditions)
        {
            runTest(new ContactsPerformanceTesting {TestConditions = testConditions });
        }

        private static void runTest(PerformanceTesting test)
        {
            test.PerfomTest().Wait();
            TestReport report = test.generateTestReport();
            if (UserInputIsTrue("Want to show the Testresults?"))
            {
                Console.WriteLine(report.ToString());
            }

            if (UserInputIsTrue("Want to Export to Filesystem?"))
            {
                test.ExportToFileSystem();
            }
            
        }

        private static bool UserInputIsTrue(string text)
        {
            Console.WriteLine(text + " [y|n]");
            bool notValid = true;
            while (notValid)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char letter = keyInfo.KeyChar;

                if (letter == 'y' || letter == 'Y') { return true; }

                if (letter == 'n' || letter == 'N') { return false; }

                Console.WriteLine($"Invalid input '{keyInfo.ToString()}', please insert 'y' or 'n'");
            }
            return false;
            
        }
    }
}
