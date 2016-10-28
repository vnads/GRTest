using System;
using System.Collections.Generic;
using GRTest.Data.Interfaces;
using GRTest.Data.Models;

namespace GRTest.Data.InMemory
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        public static List<Person> DataStore = new List<Person>(); // this is the data store for in-memory "persistence"

        public void CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPeople()
        {
            throw new NotImplementedException();
        }
    }
}
