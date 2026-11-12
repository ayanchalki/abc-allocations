using Abc.Allocations.Ots;
using Xunit;

namespace Abc.Allocations.Ots.Tests;

public class Pa9PackRounderRoundUpTests
{
    [Theory]
    [InlineData(1, 6, 1)]
    [InlineData(7, 6, 2)]
    [InlineData(11, 6, 2)]
    [InlineData(12, 6, 2)]
    public void Non_multiple_units_round_up(int units, int packSize, int expectedPacks)
    {
        // The specification requires rounding up so partial packs are shipped.
        Assert.Equal(expectedPacks, PackRounder.ToPacks(units, packSize));

        // Shipped units should be the number of packs times pack size, and must be
        // at least the originally allocated units.
        Assert.Equal(expectedPacks * packSize, PackRounder.ShippedUnits(units, packSize));
        Assert.True(PackRounder.ShippedUnits(units, packSize) >= units);
    }
}
