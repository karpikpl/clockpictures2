using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace clockPictures2
{
    [TestFixture]
    public class FileInputTest
    {
        private IEnumerable<TestCase> _testCases;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _testCases = TestCaseFinder.GetTestCases();
        }

        [Test]
        public void AllCasesFromFiles_Should_Pass()
        {
            foreach (var testCase in _testCases)
            {
                Console.WriteLine("Running for {0}", testCase.In);
                // Arrange
                var expectedResult = File.ReadAllText(testCase.Out);

                using (var dataStream = File.OpenRead(testCase.In))
                using (var outStream = new MemoryStream())
                {
                    // Act
                    Program.Solve(dataStream, outStream);
                    var result = Encoding.UTF8.GetString(outStream.ToArray());

                    // Assert
                    Assert.That(result, Is.EqualTo(expectedResult));
                }
            }

        }

        [Test]
        public void Find_Bug_in_KMP()
        {
            int mine = 0, theirs = 0, count = 0;
            string a = null, b = null;
            Random rand = new Random();
            while (mine == theirs && count < 10000)
            {
                a = rand.Next(1000000000, Int32.MaxValue).ToString();
                b = rand.Next(1000, 9999).ToString();

                var stringSearcher = new StringSearcher(b.ToCharArray());
                theirs = stringSearcher.Search(a.ToCharArray());
                mine = StringSearcher.Kmp(a, b);
                count++;
            }

            if (count != 10000)
            {
                Console.WriteLine(a);
                Console.WriteLine(b);
            }
        }

        [Test]
        public void KMP_Should_FindSubstring()
        {
            // Arrange
            const string text = "1707077560";
            const string toFind = "7077";
            const int expectedResult = 3;

            // Act
            var result = StringSearcher.Kmp(text, toFind);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
