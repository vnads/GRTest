using System;
using System.Collections.Generic;
using System.Linq;
using GRTest.API.Controllers;
using GRTest.API.Models;
using GRTest.Data.InMemory;
using GRTest.Data.Models;
using NUnit.Framework;

namespace GRTest.API.Tests
{
    [TestFixture]
    class RecordsControllerTests
    {
        [SetUp]
        public void Setup()
        {
            //as usual, make sure each test starts with a fresh store
            new InMemoryPersonRepository().ClearDataStore();
        }

        [Test]
        public void InvalidData_ThrowsException()
        {
            var controller = new RecordsController();

            Assert.Throws(typeof(InvalidOperationException), () => controller.Add(new AddRecordRequestModel
            {
              data  = "asdioudolcl"
            }));
        }

        [Test]
        public void MultipleRecords_ThrowsException()
        {
            var controller = new RecordsController();

            var ex = Assert.Throws(typeof(InvalidOperationException), () => controller.Add(new AddRecordRequestModel
            {
                data = "Pippen, Scottie, Male, Red, 10/25/1965\r\nFranklin, Aretha, Female, White, 3/25/1942"
            }));

            Assert.AreEqual("Only one record can be added at a time", ex.Message);
        }

        [Test]
        public void AddOne_Comma_AddsSuccessfully()
        {
            var controller = new RecordsController();
            controller.Add(new AddRecordRequestModel
            {
                data = "Pippen, Scottie, Male, Red, 10/25/1965"
            });

            var output = controller.GetByBirthDate();

            var result =
                output as
                    System.Web.Http.Results.OkNegotiatedContentResult
                        <IOrderedEnumerable<Person>>;

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.Count());

            var pippen = result.Content.ElementAt(0);

            Assert.AreEqual("Pippen", pippen.LastName);
            Assert.AreEqual("Scottie", pippen.FirstName);
            Assert.AreEqual(Gender.Male, pippen.Gender);
            Assert.AreEqual("Red", pippen.FavoriteColor);
            Assert.AreEqual(new DateTime(1965, 10, 25), pippen.DateOfBirth);

        }

        [Test]
        public void AddOne_Pipe_AddsSuccessfully()
        {
            var controller = new RecordsController();
            controller.Add(new AddRecordRequestModel
            {
                data = "Pippen | Scottie | Male | Red | 10/25/1965"
            });

            var output = controller.GetByBirthDate();

            var result =
                output as
                    System.Web.Http.Results.OkNegotiatedContentResult
                        <IOrderedEnumerable<Person>>;

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.Count());

            var pippen = result.Content.ElementAt(0);

            Assert.AreEqual("Pippen", pippen.LastName);
            Assert.AreEqual("Scottie", pippen.FirstName);
            Assert.AreEqual(Gender.Male, pippen.Gender);
            Assert.AreEqual("Red", pippen.FavoriteColor);
            Assert.AreEqual(new DateTime(1965, 10, 25), pippen.DateOfBirth);

        }


        [Test]
        public void AddOne_Space_AddsSuccessfully()
        {
            var controller = new RecordsController();
            controller.Add(new AddRecordRequestModel
            {
                data = "Pippen Scottie Male Red 10/25/1965"
            });

            var output = controller.GetByBirthDate();

            var result =
                output as
                    System.Web.Http.Results.OkNegotiatedContentResult
                        <IOrderedEnumerable<Person>>;

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Content.Count());

            var pippen = result.Content.ElementAt(0);

            Assert.AreEqual("Pippen", pippen.LastName);
            Assert.AreEqual("Scottie", pippen.FirstName);
            Assert.AreEqual(Gender.Male, pippen.Gender);
            Assert.AreEqual("Red", pippen.FavoriteColor);
            Assert.AreEqual(new DateTime(1965, 10, 25), pippen.DateOfBirth);

        }

        [Test]
        public void Gender_SuccessfulSort()
        {
            var controller = new RecordsController();
            controller.Add(new AddRecordRequestModel
            {
                data = "Nadimpalli Vamsi Male Blue 6/2/1983"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Hoschek Meagan Female Pink 08/07/1985"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Dude Some Male Black 1/1/1971"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jordan Michael Male Red 2/17/1963"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jones Norah Female Green 3/30/1979"
            });

            var output = controller.GetByGender();

            var result =
                output as
                    System.Web.Http.Results.OkNegotiatedContentResult
                        <IOrderedEnumerable<Person>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Content.Count());
            Assert.AreEqual(Gender.Female, result.Content.ElementAt(0).Gender);
            Assert.AreEqual(Gender.Female, result.Content.ElementAt(1).Gender);
            Assert.AreEqual(Gender.Male, result.Content.ElementAt(2).Gender);
            Assert.AreEqual(Gender.Male, result.Content.ElementAt(3).Gender);
            Assert.AreEqual(Gender.Male, result.Content.ElementAt(4).Gender);
        }

        [Test]
        public void Birthdate_SuccessfulSort()
        {
            var controller = new RecordsController();
            controller.Add(new AddRecordRequestModel
            {
                data = "Nadimpalli Vamsi Male Blue 6/2/1983"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Hoschek Meagan Female Pink 08/07/1985"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Dude Some Male Black 1/1/1971"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jordan Michael Male Red 2/17/1963"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jones Norah Female Green 3/30/1979"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jones Other Female Green 3/30/1979"
            });

            var output = controller.GetByBirthDate();

            var result =
                output as
                    System.Web.Http.Results.OkNegotiatedContentResult
                        <IOrderedEnumerable<Person>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Content.Count());

            for(var i = 0; i < result.Content.Count() - 1; i++)
                Assert.IsTrue(result.Content.ElementAt(i).DateOfBirth <= result.Content.ElementAt(i + 1).DateOfBirth);
        }

        [Test]
        public void Name_SuccessfulSort()
        {
            var controller = new RecordsController();
            controller.Add(new AddRecordRequestModel
            {
                data = "Nadimpalli Vamsi Male Blue 6/2/1983"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Hoschek Meagan Female Pink 08/07/1985"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Dude Some Male Black 1/1/1971"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jordan Michael Male Red 2/17/1963"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jones Norah Female Green 3/30/1979"
            });
            controller.Add(new AddRecordRequestModel
            {
                data = "Jones Other Female Green 3/30/1979"
            });

            var output = controller.GetByName();

            var result =
                output as
                    System.Web.Http.Results.OkNegotiatedContentResult
                        <IOrderedEnumerable<Person>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Content.Count());

            //expected order:
            //Some Dude
            //Meagan Hoschek
            //Norah Jones
            //Other Jones
            //Michael Jordan
            //Vamsi Nadimpalli

            Assert.AreEqual("Some", result.Content.ElementAt(0).FirstName); 
            Assert.AreEqual("Meagan", result.Content.ElementAt(1).FirstName);
            Assert.AreEqual("Norah", result.Content.ElementAt(2).FirstName);
            Assert.AreEqual("Other", result.Content.ElementAt(3).FirstName);
            Assert.AreEqual("Michael", result.Content.ElementAt(4).FirstName);
            Assert.AreEqual("Vamsi", result.Content.ElementAt(5).FirstName);
        }
    }
}
