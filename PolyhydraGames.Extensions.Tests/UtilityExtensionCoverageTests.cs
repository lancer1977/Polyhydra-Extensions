using System.Collections.Specialized;
using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace PolyhydraGames.Extensions.Tests;

[TestFixture]
public sealed class UtilityExtensionCoverageTests
{
    [Test]
    public void NumericDateGuardAndFormattingExtensions_ReturnExpectedValues()
    {
        var now = new DateTime(2026, 6, 5, 12, 0, 0);

        Assert.Multiple(() =>
        {
            Assert.That(12.Clamp(0, 10), Is.EqualTo(10));
            Assert.That((-1).Clamp(0, 10), Is.EqualTo(0));
            Assert.That(5.5d.Clamp(0, 5), Is.EqualTo(5));
            Assert.That(7.Squared(), Is.EqualTo(49));
            Assert.That(now.IsTimeGreaterThanOrEqual(now), Is.True);
            Assert.That(now.IsTimeGreaterThan(now.AddSeconds(-1)), Is.True);
            Assert.That(((int?)61000).MSToTime(), Is.EqualTo("01:01"));
            Assert.That(((int?)null).MSToTime(), Is.EqualTo("00:00"));
            Assert.That(90000.MSToTime(), Is.EqualTo("01:30"));
            Assert.That(3.PrefixMod(), Is.EqualTo("+3"));
            Assert.That((-2).PrefixMod(), Is.EqualTo("-2"));
        });

        Assert.DoesNotThrow(() => Guard.AgainstNull("value", "value"));
        Assert.That(() => Guard.AgainstNull<string>(null, "value"), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public async Task ObjectExtensions_CopyCloneAndRunNonNullItems()
    {
        var source = new SourceModel { Name = "Ada", Age = 42, Ignored = "source" };
        var target = new TargetModel { Name = "old", Age = 1, Ignored = 99 };
        target.CopySharedProperties(source);
        var clone = source.Clone<TargetModel>();
        var observed = string.Empty;

        await source.RunAsync(item => observed = item.Name);
        await ObjectExtensions.RunAsync<SourceModel>(null!, _ => observed = "should-not-run");

        Assert.Multiple(() =>
        {
            Assert.That(target.Name, Is.EqualTo("Ada"));
            Assert.That(target.Age, Is.EqualTo(42));
            Assert.That(target.Ignored, Is.EqualTo(99));
            Assert.That(clone.Name, Is.EqualTo("Ada"));
            Assert.That(clone.Age, Is.EqualTo(42));
            Assert.That(observed, Is.EqualTo("Ada"));
        });
    }

    [Test]
    public void NameValueCollectionExtensions_BuildQueryStringsAndDictionaries()
    {
        var collection = new NameValueCollection
        {
            ["name"] = "Ada Lovelace",
            ["role"] = "math&code"
        };

        var query = collection.ToQueryString();
        var dictionary = collection.ToDictionary();

        Assert.Multiple(() =>
        {
            Assert.That(query, Is.EqualTo("?name=Ada%20Lovelace&role=math%26code"));
            Assert.That(dictionary["name"], Is.EqualTo("Ada Lovelace"));
            Assert.That(dictionary["role"], Is.EqualTo("math&code"));
        });
    }

    [Test]
    public void Calculations_CoverCommonMathAndListHelpers()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Calculations.EnumTypeToList<SampleEnum>(), Is.EqualTo(new[] { "First Value", "Second Value" }));
            Assert.That(Calculations.EnumTypeToList<SampleEnum>(insertSpace: false), Is.EqualTo(new[] { "FirstValue", "SecondValue" }));
            Assert.That(Calculations.SplitFrontStack("front rest"), Is.EqualTo(new[] { "front", "rest" }));
            Assert.That(Calculations.SplitFrontStack("single"), Is.EqualTo(new string[2]));
            Assert.That(((Action)(() => { })).TimeEllapsed(), Is.EqualTo(1));
            Assert.That(12.Max(10), Is.EqualTo(10));
            Assert.That(7.Max(10), Is.EqualTo(7));
            Assert.That(2.MinMax(5, 10), Is.EqualTo(5));
            Assert.That(12.MinMax(5, 10), Is.EqualTo(10));
            Assert.That(Calculations.InStr("abcdef", "cd"), Is.EqualTo(2));
            Assert.That(Calculations.InStr("abcdef", "xy"), Is.EqualTo(-1));
            Assert.That(4.Buff(9), Is.EqualTo(9));
            Assert.That(9.Buff(4), Is.EqualTo(9));
            Assert.That(new[] { 1, 2, 3 }.Total(), Is.EqualTo(6));
            Assert.That(3.MinimumValue(5), Is.EqualTo(5));
            Assert.That(7.MinimumValue(5), Is.EqualTo(7));
            Assert.That(new[] { 3, 9, 1 }.GreaterOf(), Is.EqualTo(9));
            Assert.That(new[] { 3, 9, 1 }.LesserOf(), Is.EqualTo(1));
            Assert.That(Calculations.RemoveDuplicates(new[] { "a", "b", "c" }, new[] { "b" }), Is.EqualTo(new[] { "a", "c" }));
            Assert.That(Calculations.Dec2Frac(.5f), Is.EqualTo("1/2"));
            Assert.That(Calculations.CompoundInterestAnnual(100, .1, 1), Is.EqualTo(110).Within(.0001));
            Assert.That(Calculations.CompoundInterestMonthly(100, .12, 1), Is.EqualTo(Calculations.CompoundInterest(100, .12, 1, 12)).Within(.0001));
            Assert.That(Calculations.CompoundInterestDaily(100, .12, 1), Is.EqualTo(Calculations.CompoundInterest(100, .12, 1, 365)).Within(.0001));
            Assert.That(Calculations.ReverseCompoundInterest(110, .1, 1, 1), Is.EqualTo(100).Within(.0001));
            Assert.That(Calculations.CompoundInterestContinuously(100, .1, 1), Is.EqualTo(100 * Math.Exp(.1)).Within(.0001));
        });
    }

    private enum SampleEnum
    {
        FirstValue,
        SecondValue
    }

    private sealed class SourceModel
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Ignored { get; set; } = string.Empty;
    }

    private sealed class TargetModel
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Ignored { get; set; }
    }
}
