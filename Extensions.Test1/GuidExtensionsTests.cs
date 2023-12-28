using NUnit.Framework;
using PolyhydraGames.Extensions;
using System;
using System.Diagnostics;

namespace Extensions.Test1
{
    public class GuidExtensionsTests
    {
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
