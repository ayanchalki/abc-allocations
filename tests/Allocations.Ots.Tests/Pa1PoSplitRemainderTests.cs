using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa1PoSplitRemainderTests
{
    [Fact]
    public void Remainder_units_are_preserved_across_stores()
    {
        var result = AllocationSplitter.Allocate(10, new Dictionary<string, decimal> { ["S01"] = 1, ["S02"] = 1, ["S03"] = 1 });
        var total = result["S01"] + result["S02"] + result["S03"];
        Assert.Equal(10, total);
    }
}
