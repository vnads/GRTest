using System.Collections.Generic;
using GRTest.Data.Models;

namespace GRTest.Services.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> ImportPeople(string data);

        IEnumerable<Person> GetPeople();
    }
}
