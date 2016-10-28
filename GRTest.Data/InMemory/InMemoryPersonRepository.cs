using System.Collections.Generic;
using GRTest.Data.Interfaces;
using GRTest.Data.Models;

namespace GRTest.Data.InMemory
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        public static IList<Person> DataStore = new List<Person>(); // this is the data store for in-memory "persistence"

        public void CreatePerson(Person person)
        {
            DataStore.Add(person);
        }

        public IEnumerable<Person> GetPeople()
        {
            return DataStore;
        }
    }
}
