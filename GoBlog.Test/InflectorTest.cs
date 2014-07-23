using NUnit.Framework;

namespace GoBlog.Test
{
    [TestFixture]
    public class InflectorTest
    {
        [Test]
        public void CapitalizeFirstLetter_ReturnsExpected()
        {
            var acutal = "programming".CapitalizeFirstLetter();
            Assert.That(acutal, Is.EqualTo("Programming"));
        }

        [Test]
        public void CapitalizeFirstLetter_FirstLetterAlreadyCapitalized_ReturnsExpected()
        {
            var acutal = "Programming".CapitalizeFirstLetter();
            Assert.That(acutal, Is.EqualTo("Programming"));
        }

        [Test]
        public void CapitalizeFirstLetter_ManyWords_ReturnsExpected()
        {
            var acutal = "software development".CapitalizeFirstLetter();
            Assert.That(acutal, Is.EqualTo("Software development"));
        }
    }
}