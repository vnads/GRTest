using System.Collections.Generic;
using GRTest.Data.Models;

namespace GRTest.Data.Interfaces
{
    public interface IPersonRepository
    {
        void AddPeople(IEnumerable<Person> people);

        IEnumerable<Person> GetPeople();
    }
}
