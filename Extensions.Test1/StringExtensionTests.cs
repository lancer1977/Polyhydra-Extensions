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