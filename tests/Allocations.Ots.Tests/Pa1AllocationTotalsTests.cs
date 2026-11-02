using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa1AllocationTotalsTests
{
    [Fact]
    public void Allocation_totals_equal_original_units_even_when_uneven()
    {
        var result = AllocationSplitter.Allocate(5, new Dictionary<string, decimal> { ["S1"] = 1, ["S2"] = 1 });
        Assert.Equal(5, result.Values.Sum());
    }
}
