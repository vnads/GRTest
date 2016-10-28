using System;
using System.Collections.Generic;
using System.IO;
using GRTest.Data.Interfaces;
using GRTest.Data.Models;
using GRTest.Services.Parsing;

namespace GRTest.Services
{
    public class PersonImportService
    {
        private const char PERSON_SEPARATOR = '\n'; //ASSUMPTION: \n = new person


        private readonly IPersonRepository _personRepository;
        

        public PersonImportService(IPersonRepository repository)
        {
            this._personRepository = repository;
        }

        public IEnumerable<Person> ImportPeople(string peopleData)
        {
            var delimiter = new DelimitorSelector().GetDelimiter(peopleData);

            var people = peopleData.Split(PERSON_SEPARATOR);

            var exceptions = new List<Exception>(); //store here to throw later, so one bad record doesn't spoil everything

            foreach (var person in people)
            {
                try
                {
                    var personFields = person.Split(delimiter);

                    if(personFields.Length != 5)
                        throw new Exception(); //handled in catch

                    //we're going to assume the correct order is always provided
                    _personRepository.CreatePerson(new Person
                    {
                        LastName = personFields[0],
                        FirstName = personFields[1],
                        Gender = (Gender)Enum.Parse(typeof(Gender), personFields[2]),
                        FavoriteColor = personFields[3],
                        DateOfBirth = DateTime.Parse(personFields[4])
                    });
                }
                catch (Exception)
                {
                    exceptions.Add(new InvalidDataException($"Invalid Person Record: {person}"));
                }
            }

            if(exceptions.Count > 0)
                throw new AggregateException($"{exceptions.Count} error(s) occured. See inner exceptions for details.", exceptions);

            return _personRepository.GetPeople();
        }
    }
}
