using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2SmallAllocationsRoundUpTests
{
    [Fact]
    public void Small_allocation_rounds_up_to_one_pack()
    {
        // A small allocation of 5 units with packs of 6 should result in 1 pack being sent
        // and therefore 6 units actually shipped. Current implementation returns 0 packs.
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));
    }
}
