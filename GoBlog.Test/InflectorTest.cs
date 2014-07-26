using GoBlog.Common;
using NUnit.Framework;

namespace GoBlog.Test
{
    [TestFixture]
    public class InflectorTest
    {
        [TestCase("programming", ExpectedResult = "Programming")]
        [TestCase("Programming", ExpectedResult = "Programming")]
        [TestCase("unit testing", ExpectedResult = "Unit testing")]
        public string CapitalizeFirstLetter_ReturnsExpected(string sentence)
        {
            return sentence.CapitalizeFirstLetter();
        }
    }
}