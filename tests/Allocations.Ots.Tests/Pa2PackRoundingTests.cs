using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRoundingTests
{
    [Fact]
    public void Small_allocations_round_up_to_one_pack()
    {
        // An allocation smaller than one pack should still result in one pack being sent.
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));

        // An allocation that is not an exact multiple should round up to the next full pack.
        Assert.Equal(2, PackRounder.ToPacks(7, 6));
        Assert.Equal(12, PackRounder.ShippedUnits(7, 6));
    }
}
