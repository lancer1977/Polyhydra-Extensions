using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1;

[TestFixture]
public class EnumerableExtensionsTests
{
    [Test]
    public void GetFirstAndLastT_ShouldReturnFirstAndLastElements()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4, 5 };
        Func<int, int> func = x => x;
        // Act
        var result = list.GetFirstAndLastT(func);
        // Assert
        Assert.AreEqual((1, 5), result);
    }
    [Test]
    public void GetFirstAndLastT_ShouldReturnDefaultValuesForEmptyList()
    {
        // Arrange
        var list = new List<int>();
        Func<int, int> func = x => x;
        // Act
        var result = list.GetFirstAndLastT(func);
        // Assert
        Assert.AreEqual((0, 0), result);
    }
}

