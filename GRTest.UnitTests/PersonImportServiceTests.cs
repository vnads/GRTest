using System;
using System.Linq;
using GRTest.Data.InMemory;
using GRTest.Data.Interfaces;
using GRTest.Services;
using NUnit.Framework;

namespace GRTest.UnitTests
{
    [TestFixture]
    public class PersonImportServiceTests
    {
        private IPersonRepository _personRepository;

        [SetUp]
        public void Setup()
        {
            //let's just use the in-memory repository
            _personRepository = new InMemoryPersonRepository();
        }

        [Test]
        public void EmptyString_ThrowsException()
        {
            var service = new PersonImportService(this._personRepository);
            var data = string.Empty;

            Assert.Throws(typeof (InvalidOperationException), () => service.ImportPeople(data));

            Assert.AreEqual(0, this._personRepository.GetPeople().Count());
        }


        [Test]
        public void SingleSpace_ThrowsException()
        {
            var service = new PersonImportService(this._personRepository);
            var data = " ";

            var ex = (AggregateException)Assert.Throws(typeof(AggregateException), () => service.ImportPeople(data));

            Assert.AreEqual(1, ex.InnerExceptions.Count);
            Assert.AreEqual("Invalid Person Record:  ", ex.InnerExceptions.ElementAt(0).Message);

            Assert.AreEqual(0, this._personRepository.GetPeople().Count());

        }



        [Test]
        public void Invalid_SecondRecord_ThrowsException_SavesOne()
        {
            var service = new PersonImportService(this._personRepository);
            var data = " ";

            var ex = (AggregateException)Assert.Throws(typeof(AggregateException), () => service.ImportPeople(data));

            Assert.AreEqual(1, ex.InnerExceptions.Count);
            Assert.AreEqual("Invalid Person Record:  ", ex.InnerExceptions.ElementAt(0).Message);

            Assert.AreEqual(0, this._personRepository.GetPeople().Count());

        }
    }
}
