using System;
using TestingClient.Testing.Configuration;
using TestingClient.Testing.Performance.Contacts;

namespace TestingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BAGCST-Backend Tester.");
            Console.WriteLine("Please choose your test:");

            TestConditions testConditions = new TestConditions { Iterations = 100, WaitingMethod = new RandomWaiting { MinDelay = 100, MaxDelay = 400 } };

            //TODO Implement Tester-Switch, depends on UserInput


            ContactsPerformanceTesting contactsTesting = new ContactsPerformanceTesting { TestConditions = testConditions};

            contactsTesting.PerfomTest().Wait();

            Console.WriteLine($"{contactsTesting.TestResult.ToString()}");

            Console.ReadLine();
        }
    }
}
