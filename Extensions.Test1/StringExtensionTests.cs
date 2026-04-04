
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
    }
}
