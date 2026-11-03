using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa1AllocationTotalsTests
{
    [Fact]
    public void Missing_units_are_lost_when_units_do_not_divide_evenly()
    {
        // Three stores, equal weight, 10 units cannot be divided evenly -> expect total still equals 10
        var result = AllocationSplitter.Allocate(10, new Dictionary<string, decimal> { ["S1"] = 1, ["S2"] = 1, ["S3"] = 1 });

        // The bug causes truncation of fractional shares so the sum is less than the original units (e.g. 9)
        Assert.Equal(10, result["S1"] + result["S2"] + result["S3"]);
    }
}
