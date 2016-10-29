using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRTest.Data.Models;

namespace GRTest.Services
{
    public class PersonParsingService
    {
        

        public IEnumerable<Person> ParsePeopleData(string data)
        {
            var delimiter = new DelimitorSelector().GetDelimiter(data);

            var peopleData = data.Split(new[] { Constants.PERSON_SEPARATOR}, StringSplitOptions.RemoveEmptyEntries);

            var people = new List<Person>();

            foreach (var person in peopleData)
            {
                try
                {
                    var personFields = person.Split(delimiter);

                    if (personFields.Length != 5)
                        throw new Exception(); //handled in catch

                    //we're going to assume the correct order is always provided
                    people.Add(new Person
                    {
                        LastName = personFields[0].Trim(),
                        FirstName = personFields[1].Trim(),
                        Gender = (Gender)Enum.Parse(typeof(Gender), personFields[2].Trim()),
                        FavoriteColor = personFields[3].Trim(),
                        DateOfBirth = DateTime.Parse(personFields[4].Trim())
                    });
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"Invalid Person Record: {person}");
                }
            }


            return people;
        }
    }
}
