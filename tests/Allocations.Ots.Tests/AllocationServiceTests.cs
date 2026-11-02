using Abc.Allocations.Ots;
using Abc.Allocations.RefData;

namespace Abc.Allocations.Ots.Tests;

public class AllocationServiceTests
{
    private static StoreDirectory Stores() => new([
        new Store("S01", "Leeds", 1),
        new Store("S02", "York", 1),
        new Store("S03", "Hull", 2, Active: false),
    ]);

    [Fact]
    public void Inactive_stores_get_nothing()
    {
        var lines = new AllocationService(Stores()).Allocate("PO-1", 12, 6);
        Assert.Equal(["S01", "S02"], lines.Select(l => l.StoreCode));
        Assert.All(lines, l => Assert.Equal(6, l.Units));
    }

    [Fact]
    public void Report_lists_every_line()
    {
        var lines = new AllocationService(Stores()).Allocate("PO-2", 12, 6);
        var csv = AllocationReport.ToCsv("PO-2", lines);
        Assert.Equal("po,store,units,packs\nPO-2,S01,6,1\nPO-2,S02,6,1\n", csv);
    }
}
