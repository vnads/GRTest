using System;
using System.Collections.Generic;
using System.Linq;
using GRTest.Data.InMemory;
using GRTest.Data.Models;
using NUnit.Framework;

namespace GRTest.UnitTests
{
    [TestFixture]
    class InMemoryPersonRepositoryTests
    {
        [SetUp]
        public void SetUp()
        {
            //start each test with a fresh store
            new InMemoryPersonRepository().ClearDataStore();
        }

        [Test]
        public void EmptyStore_GetPeople_ReturnsEmptyList()
        {
            var people = new InMemoryPersonRepository().GetPeople();

            Assert.AreEqual(0, people.Count());
        }

        [Test]
        public void AddPeople_AddOne_Persists()
        {
            var repo = new InMemoryPersonRepository();

            repo.AddPeople(new List<Person>
            {
                new Person
                {
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1983, 6, 2),
                    FavoriteColor = "Blue",
                    FirstName = "Vamsi",
                    LastName = "Nadimpalli"
                }

            });

            var people = repo.GetPeople();

            Assert.AreEqual(1, people.Count());

            var vamsi = people.ElementAt(0);
            Assert.AreEqual("Vamsi", vamsi.FirstName);
            Assert.AreEqual("Nadimpalli", vamsi.LastName);
            Assert.AreEqual("Blue", vamsi.FavoriteColor);
            Assert.AreEqual(Gender.Male, vamsi.Gender);
            Assert.AreEqual(new DateTime(1983, 6, 2), vamsi.DateOfBirth);
        }

        [Test]
        public void AddPeople_AddMultiple_Persists()
        {
            var repo = new InMemoryPersonRepository();

            repo.AddPeople(new List<Person>
            {
                new Person
                {
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1983, 6, 2),
                    FavoriteColor = "Blue",
                    FirstName = "Vamsi",
                    LastName = "Nadimpalli"
                },
                new Person
                {
                    Gender = Gender.Female,
                    DateOfBirth = new DateTime(1985, 8, 7),
                    FavoriteColor = "Pink",
                    FirstName = "Meagan",
                    LastName = "Hoschek"
                },
                new Person
                {
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1971, 1, 1),
                    FavoriteColor = "Black",
                    FirstName = "Some",
                    LastName = "Dude"
                }

            });

            var people = repo.GetPeople();

            Assert.AreEqual(3, people.Count());

            var vamsi = people.ElementAt(0);
            Assert.AreEqual("Vamsi", vamsi.FirstName);
            Assert.AreEqual("Nadimpalli", vamsi.LastName);
            Assert.AreEqual("Blue", vamsi.FavoriteColor);
            Assert.AreEqual(Gender.Male, vamsi.Gender);
            Assert.AreEqual(new DateTime(1983, 6, 2), vamsi.DateOfBirth);

            var meagan = people.ElementAt(1);
            Assert.AreEqual("Meagan", meagan.FirstName);
            Assert.AreEqual("Hoschek", meagan.LastName);
            Assert.AreEqual("Pink", meagan.FavoriteColor);
            Assert.AreEqual(Gender.Female, meagan.Gender);
            Assert.AreEqual(new DateTime(1985, 8, 7), meagan.DateOfBirth);

            var dude = people.ElementAt(2);
            Assert.AreEqual("Some", dude.FirstName);
            Assert.AreEqual("Dude", dude.LastName);
            Assert.AreEqual("Black", dude.FavoriteColor);
            Assert.AreEqual(Gender.Male, dude.Gender);
            Assert.AreEqual(new DateTime(1971, 1, 1), dude.DateOfBirth);
        }

        [Test]
        public void MultipleRepositories_ShareDataStore()
        {
            new InMemoryPersonRepository().AddPeople(new List<Person>
            {
                new Person
                {
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1983, 6, 2),
                    FavoriteColor = "Blue",
                    FirstName = "Vamsi",
                    LastName = "Nadimpalli"
                }

            });

            var people = new InMemoryPersonRepository().GetPeople();

            Assert.AreEqual(1, people.Count());

            var vamsi = people.ElementAt(0);
            Assert.AreEqual("Vamsi", vamsi.FirstName);
            Assert.AreEqual("Nadimpalli", vamsi.LastName);
            Assert.AreEqual("Blue", vamsi.FavoriteColor);
            Assert.AreEqual(Gender.Male, vamsi.Gender);
            Assert.AreEqual(new DateTime(1983, 6, 2), vamsi.DateOfBirth);
        }
    }
}
