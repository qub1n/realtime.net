using System;
using JetBrains.dotMemoryUnit;
using LegCounterService.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemoryUnitTest
{
    [TestClass]
    public class UnitTestAllocated
    {
        private readonly LegServiceSpan _serviceSpan = new LegServiceSpan();
        private readonly string _animals = "dog,cat,spider";

        [DotMemoryUnit(CollectAllocations = true)]
        [TestMethod]
        public void TestLegServiceSpanNoAllocations()
        {
            var checkpoint1 = dotMemory.Check();

            _serviceSpan.NumberOfLegs(_animals.AsSpan());

            dotMemory.Check((Memory memory) =>
            {
                var allocatedBytes = memory.GetDifference(checkpoint1).GetNewObjects().SizeInBytes;
                Assert.IsTrue(allocatedBytes < 1000);
            });
        }       
    }
}
