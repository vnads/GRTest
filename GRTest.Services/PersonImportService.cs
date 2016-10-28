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

        public void ImportPeople(string peopleData)
        {
            var delimiter = new char();

            var people = peopleData.Split(PERSON_SEPARATOR);

            var exceptions = new List<Exception>(); //store here to throw later

            foreach (var person in people)
            {
                try
                {
                    //we haven't declare the delimiter yet
                    //for performance reasons, we'll run it on the first person so that it has a smaller set of data to read
                    if (delimiter == default(char)) 
                        delimiter = new DelimitorSelector().GetDelimiter(person);

                    var personFields = person.Split(delimiter);
                    //we're going to assume the correct order is always provided
                    _personRepository.CreatePerson(new Person
                    {

                    });
                }
                catch (Exception ex)
                {
                    exceptions.Add(new InvalidDataException($"Invalid Person Record: {person}"));
                }
            }
        }
    }
}
