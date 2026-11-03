using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRounderRoundingTests
{
    [Fact]
    public void Partial_pack_is_rounded_up_to_one_pack()
    {
        // An allocation of 5 units with a pack size of 6 should result in 1 pack being shipped
        // and 6 units actually shipped. Current implementation rounds down and returns 0 packs.
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));
    }
}
