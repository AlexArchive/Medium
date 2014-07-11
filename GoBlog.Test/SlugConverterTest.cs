using NUnit.Framework;

namespace GoBlog.Test
{
    [TestFixture]
    public class SlugConverterTest
    {
        [TestCase("Hello World", ExpectedResult = "hello-world")]
        [TestCase("Hello, World!", ExpectedResult = "hello-world")]
        [TestCase(" Hello  World ", ExpectedResult = "hello-world")]
        public string SlugConversion(string input)
        {
            return SlugConverter.Convert(input);
        }
    }
}