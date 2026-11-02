using System.Collections.Generic;
using System.Linq;
using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa1AllocationTotalsTests
{
    [Fact]
    public void Allocate_handles_non_divisible_units_and_sums_to_total()
    {
        // PO 551204 had 10 units split across three equal-weight stores S01,S02,S03
        // The allocation must sum to the original PO quantity (no units lost).
        var weights = new Dictionary<string, decimal>
        {
            ["S01"] = 1m,
            ["S02"] = 1m,
            ["S03"] = 1m,
        };

        var result = AllocationSplitter.Allocate(10, weights);

        // Expect the whole-unit shares to add up to the original units
        Assert.Equal(10, result.Values.Sum());
    }
}
