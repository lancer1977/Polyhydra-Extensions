
using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void Truncate_LongString_ReturnsTruncated()
        {
            var longString = "This is a long string";
            var truncated = longString.Truncate(10);
            Assert.That(truncated, Is.EqualTo("This is..."));
        }

        [Test]
        public void Truncate_ShortString_ReturnsOriginal()
        {
            var shortString = "Short";
            var truncated = shortString.Truncate(10);
            Assert.That(truncated, Is.EqualTo("Short"));
        }

        [Test]
        public void IsNullOrEmpty_WithWhitespace_ReturnsExpected()
        {
            string whitespace = "   ";
            Assert.That(whitespace.IsNullOrEmpty(false), Is.False);
            Assert.That(whitespace.IsNullOrEmpty(true), Is.True);
        }

        [TestCase("http:// hiimacheep.com", ExpectedResult = true)]
        [TestCase("https://hiimacheep.com", ExpectedResult = true)]
        [TestCase("hiimacheep.com", ExpectedResult = true)]
        [TestCase("hiimacheep.com/testyy", ExpectedResult = true)]
        [TestCase("hiimacheep.co", ExpectedResult = true)]
        [TestCase("dumbpeopletrap.com/scam", ExpectedResult = true)]
        [TestCase("Cheap viewers on topgaming77a.net/hz7h", ExpectedResult = true)]
        [TestCase("hiimacheep.co/testyy", ExpectedResult = true)]
        [TestCase("Hi there chief. comrade beatums", ExpectedResult = false)]
        [TestCase("Hows it going.corn today is pretty good", ExpectedResult = false)]
        public bool IsUrl(string value)
        {
            return value.IsUrl();
        }
    }
}
