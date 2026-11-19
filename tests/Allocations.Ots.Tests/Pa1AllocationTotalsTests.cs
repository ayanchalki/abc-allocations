using Abc.Allocations.Ots;
using Abc.Allocations.RefData;

namespace Abc.Allocations.Ots.Tests;

public class Pa1AllocationTotalsTests
{
    private static StoreDirectory ThreeEqualStores() => new([
        new Store("S01", "Store 1", 1),
        new Store("S02", "Store 2", 1),
        new Store("S03", "Store 3", 1),
    ]);

    [Fact]
    public void All_allocated_units_sum_to_po_total()
    {
        var svc = new AllocationService(ThreeEqualStores());
        var lines = svc.Allocate("551204", 10, 1);

        // The total allocated units across stores must equal the PO units.
        Assert.Equal(10, lines.Sum(l => l.Units));
    }
}
