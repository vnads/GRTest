using GRTest.Data.Interfaces;

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
            var people = new PersonParsingService().ParsePeopleData(data);


        }
    }
}
