using System;
using System.Linq;
using System.Web.Http;
using GRTest.API.Models;
using GRTest.Data.InMemory;
using GRTest.Services;
using GRTest.Services.Interfaces;

namespace GRTest.API.Controllers
{
    public class RecordsController : ApiController
    {
        //normally I'd use an IoC container / DI tool here, but for simplicity's sake, I'm hard-coding it
        private readonly IPersonService _personService = new PersonService(new InMemoryPersonRepository());

        [HttpPost]
        [Route("records")]
        public IHttpActionResult Add(AddRecordRequestModel record)
        {
            //adding some validation here to enfore the addition of only one at a time            
            if(record.data.Contains(Constants.PERSON_SEPARATOR))
                throw new InvalidOperationException("Only one record can be added at a time");
            var person = _personService.ImportPeople(record.data).ElementAt(0);

            return Ok(person);
        }

        [HttpGet]
        [Route("records/gender")]
        public IHttpActionResult GetByGender()
        {
            return Ok(_personService.GetPeople().OrderBy(c => c.Gender));
        }

        [HttpGet]
        [Route("records/birthdate")]
        public IHttpActionResult GetByBirthDate()
        {
            return Ok(_personService.GetPeople().OrderBy(c => c.DateOfBirth));
        }

        [HttpGet]
        [Route("records/name")]
        public IHttpActionResult GetByName()
        {
            return Ok(_personService.GetPeople().OrderBy(c => c.LastName).ThenBy(c => c.FirstName));
        }
    }
}
