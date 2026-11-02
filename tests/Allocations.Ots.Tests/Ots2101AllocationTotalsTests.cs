using Abc.Allocations.Ots;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Allocations.Ots.Tests;

public class Ots2101AllocationTotalsTests
{
    [Fact]
    public void Uneven_units_are_fully_allocated()
    {
        // A PO of 10 units split evenly across 3 stores should allocate all 10 units.
        // Currently the allocator uses integer truncation which causes a loss of 1 unit
        // (10 * 1/3 -> 3 each => 9 total). This test reproduces that failure.
        var result = AllocationSplitter.Allocate(10, new Dictionary<string, decimal> { ["S1"] = 1, ["S2"] = 1, ["S3"] = 1 });
        Assert.Equal(10, result.Values.Sum());
    }
}
