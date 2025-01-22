using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1;

[TestFixture]
public class StringExtensionTests
{
    [TestCase("This is a sample string", "is a", "sample", ExpectedResult = " ")]
    [TestCase("Hello, world!", "Hello", "world", ExpectedResult = ", ")]
    [TestCase("Start [content] End", "[", "]", ExpectedResult = "content")]
    [TestCase("NoDelimiter", "start", "end", ExpectedResult = "")]
    [TestCase("EmptyString", "", "", ExpectedResult = "EmptyString")]
    [TestCase("", "start", "end", ExpectedResult = "")]
    [TestCase("Substring without include characters", "start", "end", false, ExpectedResult = "")]
    [TestCase("Substring with include characters", "start", "end", true, ExpectedResult = "")]
    public string Between_ShouldReturnCorrectSubstring(string input, string character1, string character2, bool includeCharacters = false)
    {
        var result = input.Between(character1, character2, includeCharacters);
        Console.WriteLine(result);
        return result;
    }
}


[TestFixture]
public class ComparersTests
{
    [Test]
    public void FindMostSimilar_ShouldReturnMostSimilarString()
    {
        // Arrange
        var list = new List<string> { "apple", "banana", "cherry" };
        var value = "appel";
        // Act
        var result = list.FindMostSimilar(value);
        // Assert
        Assert.That("apple" == result);
    }
    [Test]
    public void GetLevenshteinDistance_ShouldReturnTrueIfDistanceIsLessThanOrEqualToThreshold()
    {
        // Arrange
        var s1 = "apple";
        var s2 = "appel";
        // Act
        var result = s1.GetLevenshteinDistance(s2);
        // Assert
        Assert.That(result);
    }
    [Test]
    public void LevenshteinDistance_ShouldReturnCorrectDistance()
    {
        // Arrange
        var s1 = "apple";
        var s2 = "appel";
        // Act
        var result = s1.LevenshteinDistance(s2);
        // Assert
        Assert.That(2 == result);
    }
}


[TestFixture]
public class HtmlStyleHelpersTests
{
    [Test]
    public void Show_ShouldReturnEmptyStringIfTrue()
    {
        // Arrange
        var value = true;
        // Act
        var result = HtmlStyleHelpers.Show(value);
        // Assert
        Assert.That("" == result);
    }
    [Test]
    public void Show_ShouldReturnDisplayNoneIfFalse()
    {
        // Arrange
        var value = false;
        // Act
        var result = HtmlStyleHelpers.Show(value);
        // Assert
        Assert.That("display:none;" == result);
    }

    [TestCase("HelloIAmText", ExpectedResult = "Hello I Am Text")]
    [TestCase("", ExpectedResult = "")]
    [TestCase("A", ExpectedResult = "A")]
    [TestCase("AB", ExpectedResult = "A B")]
    [TestCase(null, ExpectedResult = null)]
    public string? Show_ShouldReturnDisplayNoneIfFalse(string? input) => input.InsSpace();
}
 
[TestFixture]
public class HttpEncodingTests
{
    [Test]
    public void ToUrlEncoded_ShouldReturnUrlEncodedString()
    {
        // Arrange
        var value = "Hello World!";
        // Act
        var result = value.ToUrlEncoded();
        // Assert
        Assert.That("Hello%20World%21"== result);
    }
}
