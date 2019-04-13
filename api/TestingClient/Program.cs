using Microsoft.Extensions.DependencyInjection;
using System;
using TestingClient.Testing;
using TestingClient.Testing.Configuration;
using TestingClient.Testing.Configuration.WaitingMethod;
using TestingClient.Testing.Performance.Contacts;
using TestingClient.Testing.Reporting;

namespace TestingClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to BAGCST-Backend Tester.");
            Console.WriteLine("Please choose your test:");

            TestConditions testConditions = new TestConditions { Iterations = 100, WaitingMethod = new RandomWaiting { MinDelay = 100, MaxDelay = 200 } };

            //TODO Implement Tester-Switch, depends on UserInput


            ContactsPerformanceTesting contactsTesting = new ContactsPerformanceTesting { TestConditions = testConditions};

            contactsTesting.PerfomTest().Wait();
            TestReport report = contactsTesting.generateTestReport();

            Console.WriteLine(report.ToString());

            Console.ReadLine();
        }
    }
}
