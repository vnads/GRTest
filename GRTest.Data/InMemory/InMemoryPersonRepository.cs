using System.Collections.Generic;
using GRTest.Data.Interfaces;
using GRTest.Data.Models;

namespace GRTest.Data.InMemory
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private static readonly List<Person> DataStore = new List<Person>(); // this is the data store for in-memory "persistence"

        public void AddPeople(IEnumerable<Person> people)
        {
            DataStore.AddRange(people);
        }

        public IEnumerable<Person> GetPeople()
        {
            return new List<Person>(DataStore);
        }

        public void ClearDataStore()
        {
            DataStore.Clear();
        }
    }
}
