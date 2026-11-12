using Abc.Allocations.Ots;
using Abc.Allocations.RefData;

namespace Abc.Allocations.Ots.Tests;

public class PA8PoNumberCommaTests
{
    private static StoreDirectory Stores() => new([
        new Store("S01", "Leeds", 1),
        new Store("S02", "York", 1),
        new Store("S03", "Hull", 2, Active: false),
    ]);

    [Fact]
    public void PoNumber_with_comma_gets_quoted_in_csv()
    {
        var service = new AllocationService(Stores());
        var lines = service.Allocate("PO,2", 12, 6);
        var csv = AllocationReport.ToCsv("PO,2", lines);

        // When a PO number contains a comma it must be quoted in CSV so downstream systems
        // do not split the PO value and report a PO number mismatch.
        Assert.Equal("po,store,units,packs\n\"PO,2\",S01,6,1\n\"PO,2\",S02,6,1\n", csv);
    }
}
