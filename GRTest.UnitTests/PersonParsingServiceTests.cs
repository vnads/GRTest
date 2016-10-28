using System;
using System.IO;
using System.Linq;
using GRTest.Data.Models;
using GRTest.Services;
using NUnit.Framework;

namespace GRTest.UnitTests
{
    [TestFixture]
    public class PersonParsingServiceTests
    {
       
        [Test]
        public void EmptyString_ThrowsException()
        {
            var service = new PersonParsingService();
            var data = string.Empty;

            Assert.Throws(typeof (InvalidOperationException), () => service.ParsePeopleData(data));
        }


        [Test]
        public void SingleSpace_ThrowsException()
        {
            var service = new PersonParsingService();
            var data = " ";

            var ex = Assert.Throws(typeof(InvalidDataException), () => service.ParsePeopleData(data));
            
            Assert.AreEqual("Invalid Person Record:  ", ex.Message);
            
        }



        [Test]
        public void InvalidRecord_PipeDelimited_ThrowsException()
        {
            var service = new PersonParsingService();
            var data = "LastName | FirstName | Male | Yellow | 10/2/1983\r\nLastName2 | FirstName2 | Blue | 10/24/1983";

            var ex = Assert.Throws(typeof(InvalidDataException), () => service.ParsePeopleData(data));

            
            Assert.AreEqual("Invalid Person Record: LastName2 | FirstName2 | Blue | 10/24/1983", ex.Message);           

        }

        [Test]
        public void Valid_PipeDelimited_ReturnsValidPeople()
        {
            var service = new PersonParsingService();
            var data = "LastName | FirstName | Male | Yellow | 10/2/1983\r\nLastName2|FirstName2|Female|Blue|10/24/1983";

            var people = service.ParsePeopleData(data);

            Assert.AreEqual(2, people.Count());
            Assert.AreEqual("LastName", people.ElementAt(0).LastName);
            Assert.AreEqual("FirstName", people.ElementAt(0).FirstName);
            Assert.AreEqual(Gender.Male, people.ElementAt(0).Gender);
            Assert.AreEqual("Yellow", people.ElementAt(0).FavoriteColor);
            Assert.AreEqual(new DateTime(1983, 10, 2), people.ElementAt(0).DateOfBirth);

            Assert.AreEqual("LastName2", people.ElementAt(1).LastName);
            Assert.AreEqual("FirstName2", people.ElementAt(1).FirstName);
            Assert.AreEqual(Gender.Female, people.ElementAt(1).Gender);
            Assert.AreEqual("Blue", people.ElementAt(1).FavoriteColor);
            Assert.AreEqual(new DateTime(1983, 10, 24), people.ElementAt(1).DateOfBirth);
        }

        [Test]
        public void Valid_CommaDelimited_ReturnsValidPeople()
        {
            var service = new PersonParsingService();
            var data = "LastName, FirstName, Male, Yellow, 10/2/1983\r\nLastName2,FirstName2,Female,Blue,10/24/1983";

            var people = service.ParsePeopleData(data);

            Assert.AreEqual(2, people.Count());
            Assert.AreEqual("LastName", people.ElementAt(0).LastName);
            Assert.AreEqual("FirstName", people.ElementAt(0).FirstName);
            Assert.AreEqual(Gender.Male, people.ElementAt(0).Gender);
            Assert.AreEqual("Yellow", people.ElementAt(0).FavoriteColor);
            Assert.AreEqual(new DateTime(1983, 10, 2), people.ElementAt(0).DateOfBirth);

            Assert.AreEqual("LastName2", people.ElementAt(1).LastName);
            Assert.AreEqual("FirstName2", people.ElementAt(1).FirstName);
            Assert.AreEqual(Gender.Female, people.ElementAt(1).Gender);
            Assert.AreEqual("Blue", people.ElementAt(1).FavoriteColor);
            Assert.AreEqual(new DateTime(1983, 10, 24), people.ElementAt(1).DateOfBirth);
        }

        [Test]
        public void Valid_SpaceDelimited_ReturnsValidPeople()
        {
            var service = new PersonParsingService();
            var data = "LastName FirstName Male Yellow 10/2/1983\r\nLastName2 FirstName2 Female Blue 10/24/1983";

            var people = service.ParsePeopleData(data);

            Assert.AreEqual(2, people.Count());
            Assert.AreEqual("LastName", people.ElementAt(0).LastName);
            Assert.AreEqual("FirstName", people.ElementAt(0).FirstName);
            Assert.AreEqual(Gender.Male, people.ElementAt(0).Gender);
            Assert.AreEqual("Yellow", people.ElementAt(0).FavoriteColor);
            Assert.AreEqual(new DateTime(1983, 10, 2), people.ElementAt(0).DateOfBirth);

            Assert.AreEqual("LastName2", people.ElementAt(1).LastName);
            Assert.AreEqual("FirstName2", people.ElementAt(1).FirstName);
            Assert.AreEqual(Gender.Female, people.ElementAt(1).Gender);
            Assert.AreEqual("Blue", people.ElementAt(1).FavoriteColor);
            Assert.AreEqual(new DateTime(1983, 10, 24), people.ElementAt(1).DateOfBirth);
        }
    }
}
