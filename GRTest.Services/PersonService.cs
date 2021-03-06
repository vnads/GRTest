﻿using System.Collections.Generic;
using GRTest.Data.Interfaces;
using GRTest.Data.Models;
using GRTest.Services.Interfaces;

namespace GRTest.Services
{
    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _personRepository;
        

        public PersonService(IPersonRepository repository)
        {
            this._personRepository = repository;
        }

        

        public IEnumerable<Person> ImportPeople(string data)
        {
            var people = new PersonParsingService().ParsePeopleData(data);

            this._personRepository.AddPeople(people);

            return people;
        }

        public IEnumerable<Person> GetPeople()
        {
            return this._personRepository.GetPeople();
        }
    }
}
