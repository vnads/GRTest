using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GRTest.Data.Models;
using GRTest.Services;

namespace GRTest.CLI
{
    class Program
    {
        //All files stored in /Data directory of this project
        //To make it easy, we're just looking for any file in that directory
        static void Main(string[] args)
        {
            var directoryPath = AppDomain.CurrentDomain.BaseDirectory + "/Data";
            var filePaths = Directory.GetFiles(directoryPath);

            var people = new List<Person>();

            foreach (var filePath in filePaths)
            {
                try
                {
                    var data = File.ReadAllText(filePath);
                    people.AddRange(new PersonParsingService().ParsePeopleData(data));
                }
                catch (Exception ex)
                {
                    //simply output any errors we get
                    Console.WriteLine(ex.Message);
                }
            }

            PrintSet(people.OrderBy(c => c.Gender).ThenBy(c => c.LastName), "Ordered by Gender then Last Name");
            PrintSet(people.OrderBy(c => c.DateOfBirth), "Ordered by Birth Date");
            PrintSet(people.OrderByDescending(c => c.LastName), "Ordered by Last Name descending");

            //wait for response to exit
            Console.WriteLine("Press any key to exit");

            Console.ReadKey();
        }

        static void PrintSet(IEnumerable<Person> dataSet, string title)
        {
            //add a new line just for separation
            Console.WriteLine();
            Console.WriteLine(title);
            foreach (var person in dataSet)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}, {person.Gender}, Favorite Color: {person.FavoriteColor}, DOB: {person.DateOfBirth.ToString("M/d/yyyy")}");
            }
        }
    }
}
