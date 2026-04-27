using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1;

public class ExtensionTests
{
    [TestCase(123456, "02:03")]
    [TestCase(0, "00:00")]
    [TestCase(null, "00:00")]
    [TestCase(int.MaxValue, "31:23")]
    public void MSToTime_ShouldReturnFormattedTime(int? milliseconds, string expectedTime)
    {
        // Act
        string result = milliseconds.MSToTime();

        // Assert
        Assert.That(result, Is.EqualTo(expectedTime));
    }
}