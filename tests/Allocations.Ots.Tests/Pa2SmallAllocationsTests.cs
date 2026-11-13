using Abc.Allocations.Ots;

namespace Abc.Allocations.Ots.Tests;

public class Pa2SmallAllocationsTests
{
    [Fact]
    public void Partial_allocations_round_up_to_one_pack()
    {
        // A store allocated 5 units with pack size 6 should receive 1 pack (rounded up),
        // but the current implementation returns 0.
        Assert.Equal(1, PackRounder.ToPacks(5, 6));
    }
}
