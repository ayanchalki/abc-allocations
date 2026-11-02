using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class AllocationSplitterTests
{
    [Fact]
    public void Even_split_gives_equal_shares()
    {
        var result = AllocationSplitter.Allocate(12, new Dictionary<string, decimal> { ["S1"] = 1, ["S2"] = 1, ["S3"] = 1 });
        Assert.Equal(4, result["S1"]);
        Assert.Equal(4, result["S2"]);
        Assert.Equal(4, result["S3"]);
    }

    [Fact]
    public void Shares_follow_weights()
    {
        var result = AllocationSplitter.Allocate(100, new Dictionary<string, decimal> { ["S1"] = 3, ["S2"] = 1 });
        Assert.Equal(75, result["S1"]);
        Assert.Equal(25, result["S2"]);
    }

    [Fact]
    public void Negative_units_are_rejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            AllocationSplitter.Allocate(-1, new Dictionary<string, decimal> { ["S1"] = 1 }));
    }

    [Fact]
    public void Zero_total_weight_is_rejected()
    {
        Assert.Throws<ArgumentException>(() =>
            AllocationSplitter.Allocate(10, new Dictionary<string, decimal> { ["S1"] = 0 }));
    }
}
