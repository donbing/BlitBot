using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitBot.Data;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        readonly ChartService svc = new(null, new ChartConfig());
        IEnumerable<string>? tzs;

        [OneTimeSetUp] public async Task Setup() => 
            tzs = await svc.SearchTimeZones("AMeriCa");

        [Test] public void GetTimeZones() => 
            Assert.That(tzs, Is.Not.Empty);

        [Test] public void AreOrdered() => 
            Assert.That(tzs, Is.Ordered);
    }
}