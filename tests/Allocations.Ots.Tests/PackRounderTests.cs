using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class PackRounderTests
{
    [Theory]
    [InlineData(12, 6, 2)]
    [InlineData(24, 12, 2)]
    [InlineData(0, 6, 0)]
    public void Exact_multiples_give_whole_packs(int units, int packSize, int packs)
    {
        Assert.Equal(packs, PackRounder.ToPacks(units, packSize));
    }

    [Fact]
    public void Pack_size_must_be_positive()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => PackRounder.ToPacks(6, 0));
    }
}
