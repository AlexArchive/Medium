using NUnit.Framework;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class SummaryConverterTest
    {
        [TestCase("", ExpectedResult = "")]
        [TestCase("foo", ExpectedResult = "foo")]
        [TestCase("foo\r\nbar", ExpectedResult = "foo")]
        [TestCase("foo\r\nbar\r\n", ExpectedResult = "foo")]
        public string Convert_ReturnsExpected(string input)
        {
            return SummaryConverter.Convert(input);
        }
    }
}