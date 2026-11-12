using Abc.Allocations.Ots;
using Abc.Allocations.RefData;

namespace Abc.Allocations.Ots.Tests;

public class Pa6DuplicateStoreCodesTests
{
    private static StoreDirectory Stores() => new([
        new Store("S01", "Leeds", 1),
        new Store("S02", "York", 1),
        new Store("S03", "Hull", 2, Active: false),
    ]);

    [Fact]
    public void Duplicate_store_codes_are_treated_once()
    {
        var service = new AllocationService(Stores());

        // Duplicate S01 should be treated as a single store (S01, S02)
        var lines = service.AllocateTo(new[] { "S01", "S01", "S02" }, 12, 6);

        Assert.Equal(new[] { "S01", "S02" }, lines.Select(l => l.StoreCode));
        // With two unique stores of equal weight, each should get half the units
        Assert.All(lines, l => Assert.Equal(6, l.Units));
    }
}
