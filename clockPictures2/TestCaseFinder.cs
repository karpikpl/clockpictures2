using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace clockPictures2
{
    public class TestCase
    {
        public TestCase(string @in, string @out)
        {
            Out = @out;
            In = @in;
        }

        public string In { get; private set; }
        public string Out { get; private set; }
    }

    public class TestCaseFinder
    {
        public static IEnumerable<TestCase> GetTestCases()
        {
            var solutionDir = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent;
            var inputFiles = solutionDir.GetFiles("*.in", SearchOption.AllDirectories);

            return inputFiles.Select(p => new TestCase(p.FullName, InToOutChanger(p.FullName)));
        }

        private static string InToOutChanger(string inPath)
        {
            return inPath.Substring(0, inPath.Length - 2) + "ans";
        }
    }
}
