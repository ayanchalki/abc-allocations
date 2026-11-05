using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRounderTests
{
    [Fact]
    public void Partial_and_non_multiple_allocations_round_up()
    {
        // Allocation less than one pack should still ship one full pack
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));

        // Allocation that is not an exact multiple should round up to the next pack
        Assert.Equal(2, PackRounder.ToPacks(7, 6));
        Assert.Equal(12, PackRounder.ShippedUnits(7, 6));
    }
}
