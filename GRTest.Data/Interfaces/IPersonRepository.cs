using System.Collections.Generic;
using GRTest.Data.Models;

namespace GRTest.Data.Interfaces
{
    public interface IPersonRepository
    {
        void CreatePerson(Person person);

        IEnumerable<Person> GetPeople();
    }
}
