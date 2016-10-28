using System;
using System.Collections.Generic;
using System.IO;
using GRTest.Data.Interfaces;
using GRTest.Data.Models;

namespace GRTest.Services
{
    public class PersonImportService
    {

        private readonly IPersonRepository _personRepository;
        

        public PersonImportService(IPersonRepository repository)
        {
            this._personRepository = repository;
        }

        

        public void ImportPeople(string data)
        {
            //var people = ParsePeople(data);


        }
    }
}
