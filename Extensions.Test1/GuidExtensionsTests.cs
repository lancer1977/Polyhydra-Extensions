using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1
{
    public class GuidExtensionsTests
    {
        [TestCase("c210ec9f-b4d6-40d0-86a1-b1a6d922a418", ExpectedResult = false),
         TestCase("00000000-0000-0000-0000-000000000000", ExpectedResult = true),
        ]
        public bool IsEmpty(string value)
        {
            var guid = Guid.Parse(value);
            return guid.IsEmpty();
        }
        
        [Test]
        public void ToGuid_ValidString_ReturnsGuid()
        {
            // Arrange
            var validGuidString = Guid.NewGuid().ToString();

            // Act
            var result = validGuidString.ToGuid();

            // Assert
            Assert.IsInstanceOf<Guid>(result);
            Assert.AreNotEqual(Guid.Empty, result);
        }

        [Test]
        public void ToGuid_InvalidString_ReturnsEmptyGuid()
        {
            // Arrange
            var invalidGuidString = "invalid guid string";

            // Act
            var result = invalidGuidString.ToGuid();

            // Assert
            Assert.IsInstanceOf<Guid>(result);
            Assert.AreEqual(Guid.Empty, result);
        }

        [Test]
        public void ToGuid_EmptyString_ReturnsEmptyGuid()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act
            var result = emptyString.ToGuid();

            // Assert
            Assert.IsInstanceOf<Guid>(result);
            Assert.AreEqual(Guid.Empty, result);
        }
    }
}
