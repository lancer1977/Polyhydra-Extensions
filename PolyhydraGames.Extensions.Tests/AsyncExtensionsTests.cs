
using System.Threading.Tasks;
using NUnit.Framework;
using PolyhydraGames.Extensions;
using System;

namespace Extensions.Test1
{
    [TestFixture]
    public class AsyncExtensionsTests
    {
        [Test]
        public async Task WhenAll_Success_ReturnsResults()
        {
            var tasks = new[] { Task.FromResult(1), Task.FromResult(2) };
            var results = await tasks.WhenAll();
            Assert.That(results, Is.EqualTo(new[] { 1, 2 }));
        }

        [Test]
        public void WithTimeout_TimesOut_Throws()
        {
            Assert.ThrowsAsync<TimeoutException>(async () =>
            {
                await Task.Delay(100).WithTimeout(10);
            });
        }
    }
}
