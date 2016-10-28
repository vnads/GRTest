using System;
using GRTest.Services;
using NUnit.Framework;

namespace GRTest.UnitTests
{
    [TestFixture]
    public class DelimitorSelectorTests
    {
        [Test]
        public void SpaceDelimter()
        {
            const string data = "LastName FirstName Gender FavoriteColor DateOfBirth";

            var delimiter = new DelimitorSelector().GetDelimiter(data);

            Assert.AreEqual(' ', delimiter);
        }

        [Test]
        public void PipeDelimter()
        {
            const string data = "LastName | FirstName | Gender | FavoriteColor | DateOfBirth";

            var delimiter = new DelimitorSelector().GetDelimiter(data);

            Assert.AreEqual('|', delimiter);
        }

        [Test]
        public void CommaDelimter()
        {
            const string data = "LastName, FirstName, Gender, FavoriteColor, DateOfBirth";

            var delimiter = new DelimitorSelector().GetDelimiter(data);

            Assert.AreEqual(',', delimiter);
        }

        [Test]
        public void Invalid_ThrowsException()
        {
            const string data = "LastName+FirstName+Gender+FavoriteColor+DateOfBirth";

            var ex = Assert.Throws(typeof (InvalidOperationException), () => new DelimitorSelector().GetDelimiter(data));

            Assert.AreEqual("No valid delimiters found", ex.Message);
        }
    }
}
