using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRounderRoundingTests
{
    [Fact]
    public void Small_allocation_rounds_up_to_one_pack()
    {
        // A store allocated fewer units than a pack should still receive one full pack.
        // Observed: 5 units with pack size 6 produced 0 packs (store received nothing).
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        // And the shipped units should reflect that one full pack is sent (6 units).
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));
    }
}
