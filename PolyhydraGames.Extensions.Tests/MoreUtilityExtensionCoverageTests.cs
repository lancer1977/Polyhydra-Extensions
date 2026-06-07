using System.Reflection;
using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace PolyhydraGames.Extensions.Tests;

[TestFixture]
public sealed class MoreUtilityExtensionCoverageTests
{
    [Test]
    public async Task ActionAndTryHelpers_ExecuteSuccessAndFailurePaths()
    {
        var order = new List<string>();
        Action<string, Action> wrapper = (prefix, followup) =>
        {
            order.Add(prefix);
            followup?.Invoke();
        };
        var continued = wrapper.ToContinueAction("first");
        ActionExtensions.BuildAction(continued, () => order.Add("second"))();

        var count = 0;
        ((Action)(() => count++)).Execute(3);
        ((Action)(() => count++)).Execute(0);

        var asyncCount = 0;
        await ActionExtensions.ExecuteAsync((Func<Task>)(() =>
        {
            asyncCount++;
            return Task.CompletedTask;
        }), 2);
        await ActionExtensions.ExecuteAsync((Func<Task>)(() =>
        {
            asyncCount++;
            return Task.CompletedTask;
        }), 0);

        await Task.CompletedTask.Try();
        await Task.FromException(new InvalidOperationException("boom")).Try();
        var value = await Task.FromResult(5).Try();
        var missing = await Task.FromException<int>(new InvalidOperationException("boom")).Try();

        Assert.Multiple(() =>
        {
            Assert.That(order, Is.EqualTo(new[] { "first", "second" }));
            Assert.That(count, Is.EqualTo(3));
            Assert.That(asyncCount, Is.EqualTo(2));
            Assert.That(value, Is.EqualTo(5));
            Assert.That(missing, Is.EqualTo(0));
        });
    }

    [Test]
    public void EnumExtensions_ConvertBetweenStringsIntsAndArrays()
    {
        Assert.Multiple(() =>
        {
            Assert.That(SampleEnum.FirstValue.ToEnum<OtherEnum>(), Is.EqualTo(OtherEnum.FirstValue));
            Assert.That(SampleEnum.Unmatched.ToEnum<OtherEnum>(), Is.EqualTo(OtherEnum.FirstValue));
            Assert.That(1.ToEnum<SampleEnum>(), Is.EqualTo(SampleEnum.SecondValue));
            Assert.That("second value".ToEnum<SampleEnum>(), Is.EqualTo(SampleEnum.SecondValue));
            Assert.That("".ToEnum<SampleEnum>(), Is.EqualTo(default(SampleEnum)));
            Assert.That(EnumExtensions.EnumStringCompare(SampleEnum.SecondValue, "second value"), Is.True);
            Assert.That(new[] { "FirstValue", "SecondValue" }.ToEnum<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }));
            Assert.That(((string[])null!).ToIEnum<SampleEnum>(), Is.Empty);
            Assert.That(new[] { "FirstValue", "SecondValue" }.AsEnumerable().ToEnum<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }));
            Assert.That(new[] { 0, 1 }.ToEnum<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }));
            Assert.That("FirstValue#SecondValue".ToEnumList<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }));
            Assert.That(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }.EnumToArray(), Is.EqualTo(new[] { "First Value", "Second Value" }));
            Assert.That(new[] { SampleEnum.FirstValue }.EnumToList(insertSpaces: false), Is.EqualTo(new[] { "FirstValue" }));
            Assert.That(EnumExtensions.EnumTypeToArray<SampleEnum>(), Is.EqualTo(new[] { "First Value", "Second Value", "Unmatched" }));
            Assert.That(EnumExtensions.EnumTypeToList<SampleEnum>(insertSpace: false), Is.EqualTo(new[] { "FirstValue", "SecondValue", "Unmatched" }));
            Assert.That(EnumExtensions.EnumerateEnumType<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue, SampleEnum.Unmatched }));
            Assert.That(new[] { 0, 1 }.FromIntArray<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }));
            Assert.That(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue }.ToIntArray(), Is.EqualTo(new[] { 0, 1 }));
            Assert.That(EnumExtensions.EnumOptionArray<SampleEnum>(), Is.EqualTo(new[] { SampleEnum.FirstValue, SampleEnum.SecondValue, SampleEnum.Unmatched }));
        });
    }

    [Test]
    public void TypeExtensions_FilterTypesAndCheckAssignableRelationships()
    {
        var types = Assembly.GetExecutingAssembly().CreatableTypes().ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(types, Does.Contain(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(types.StartingWith("MoreUtility").Single(), Is.EqualTo(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(types.EndingWith("CoverageTests"), Does.Contain(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(types.Containing("UtilityExtension"), Does.Contain(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(types.InNamespace("PolyhydraGames.Extensions.Tests"), Does.Contain(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(types.Except(typeof(MoreUtilityExtensionCoverageTests), typeof(UtilityExtensionCoverageTests)), Does.Not.Contain(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(types.Except(typeof(MoreUtilityExtensionCoverageTests), typeof(UtilityExtensionCoverageTests), typeof(object)), Does.Not.Contain(typeof(MoreUtilityExtensionCoverageTests)));
            Assert.That(TypeExtensions.Implements<List<string>, IEnumerable<string>>(), Is.True);
            Assert.That(typeof(List<string>).Implements<IEnumerable<string>>(), Is.True);
        });
    }

    [Test]
    public void Comparers_FindSimilarStringsAndDistances()
    {
        var items = new[] { "alpha", "beta", "gamma" };

        Assert.Multiple(() =>
        {
            Assert.That(items.FindMostSimilar("alpa"), Is.EqualTo("alpha"));
            Assert.That(items.FindMostSimilar("zzzz", threshold: 1), Is.EqualTo(string.Empty));
            Assert.That("kitten".LevenshteinDistance("sitting"), Is.EqualTo(3));
            Assert.That("kitten".GetLevenshteinDistance("sitting"), Is.True);
            Assert.That("kitten".GetLevenshteinDistance("sitting", threshold: 2), Is.False);
        });
    }

    private enum SampleEnum
    {
        FirstValue,
        SecondValue,
        Unmatched
    }

    private enum OtherEnum
    {
        FirstValue,
        SecondValue
    }
}
