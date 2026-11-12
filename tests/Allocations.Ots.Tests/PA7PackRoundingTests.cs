using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class PA7PackRoundingTests
{
    [Fact]
    public void Partial_packs_round_up()
    {
        // 7 units with pack size 6 should require 2 packs (round up), shipping 12 units total.
        Assert.Equal(2, PackRounder.ToPacks(7, 6));
        Assert.Equal(12, PackRounder.ShippedUnits(7, 6));
    }
}
