using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRounderTests
{
    [Fact]
    public void Small_and_fractional_allocations_round_up_to_packs()
    {
        // Allocations less than one full pack should round up to one pack
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));

        // Allocations that are not exact multiples should round up to the next full pack
        Assert.Equal(2, PackRounder.ToPacks(7, 6));
        Assert.Equal(12, PackRounder.ShippedUnits(7, 6));

        // Zero units should remain zero packs
        Assert.Equal(0, PackRounder.ToPacks(0, 6));
        Assert.Equal(0, PackRounder.ShippedUnits(0, 6));
    }
}
