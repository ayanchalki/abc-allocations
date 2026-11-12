using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRounderTests
{
    [Fact]
    public void Allocations_less_than_pack_round_up()
    {
        // An allocation smaller than one pack should still result in sending one full pack.
        var packs = PackRounder.ToPacks(1, 6);
        Assert.Equal(1, packs);

        var shipped = PackRounder.ShippedUnits(1, 6);
        Assert.Equal(6, shipped);

        // Also verify non-exact multiples round up to the next whole pack.
        Assert.Equal(2, PackRounder.ToPacks(7, 6));
        Assert.Equal(12, PackRounder.ShippedUnits(7, 6));
    }
}
