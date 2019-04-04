using System;
using System.Collections.Generic;
using System.Text;
using TestingClient.Testing.Performance.Abstract;

namespace TestingClient.Testing.Performance.Contacts
{
    class ContactsPerformanceTesting : PerformanceTesting
    {
        public ContactsPerformanceTesting() : base("http://localhost:55510/api/contacts")
        {

        }
    }
}