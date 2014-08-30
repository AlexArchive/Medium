using NUnit.Framework;

namespace GoBlog.UnitTest
{
    public class SlugConverterTest
    {
        [TestCase("Hello World", ExpectedResult = "hello-world")]
        [TestCase("Hello, World!", ExpectedResult = "hello-world")]
        [TestCase(" Hello  World ", ExpectedResult = "hello-world")]
        [TestCase("Hello-World", ExpectedResult = "hello-world")]
        public string Convert_ReturnsExpected(string input)
        {
            return SlugConverter.Convert(input);
        }
    }
}