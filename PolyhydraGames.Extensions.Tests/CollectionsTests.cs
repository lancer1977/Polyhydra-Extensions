
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace Extensions.Test1
{
    [TestFixture]
    public class CollectionsTests
    {
        [Test]
        public void Each_ForEach_ExecutesAction()
        {
            var items = new List<int> { 1, 2, 3 };
            var sum = 0;
            items.Each(i => sum += i);
            Assert.That(sum, Is.EqualTo(6));
        }

        [Test]
        public void FirstOr_Empty_ReturnsDefault()
        {
            var items = new List<int>();
            var result = items.FirstOr(5);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Shuffle_ChangesOrder()
        {
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var original = new List<int>(items);
            items.Shuffle();
            Assert.That(items, Has.Count.EqualTo(original.Count));
            Assert.That(items.OrderBy(x => x), Is.EqualTo(original));
        }
    }
}
