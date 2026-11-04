using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2PackRounderTests
{
    [Fact]
    public void Small_allocations_round_up_to_one_pack()
    {
        // An allocation smaller than a pack should still result in one pack being sent.
        // Expected behaviour: roundup to next full pack so store receives at least one pack.
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
        Assert.Equal(6, PackRounder.ShippedUnits(5, 6));
    }
}
