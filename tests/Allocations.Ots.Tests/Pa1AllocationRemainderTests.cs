using Abc.Allocations.Ots;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Allocations.Ots.Tests;

public class Pa1AllocationRemainderTests
{
    [Fact]
    public void Allocates_all_units_when_not_evenly_divisible()
    {
        var weights = new Dictionary<string, decimal> { ["S01"] = 1, ["S02"] = 1, ["S03"] = 1 };
        var result = AllocationSplitter.Allocate(10, weights);

        // The total allocated units must equal the PO units even when they don't divide evenly.
        Assert.Equal(10, result.Values.Sum());
    }
}
