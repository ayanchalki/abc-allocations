using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2SmallAllocationRoundingTests
{
    [Fact]
    public void Small_allocations_round_up_to_one_pack()
    {
        // A small allocation that is less than a full pack should result in 1 pack being sent
        // (round up to the next full pack), and shipped units should reflect that pack.
        int units = 5;
        int packSize = 6;

        Assert.Equal(1, PackRounder.ToPacks(units, packSize));
        Assert.Equal(6, PackRounder.ShippedUnits(units, packSize));
    }
}
