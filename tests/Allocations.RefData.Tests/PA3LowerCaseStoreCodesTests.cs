using Abc.Allocations.RefData;

namespace Abc.Allocations.RefData.Tests;

public class PA3LowerCaseStoreCodesTests
{
    private static StoreDirectory Directory() => new([
        new Store("S01", "Leeds", 1),
        new Store("S02", "York", 2, Active: false),
    ]);

    [Fact]
    public void Finds_store_by_code_case_insensitive()
    {
        // Lower-case code should resolve to the same store as the upper-case code.
        Assert.Equal("Leeds", Directory().Get("s01").Name);
    }
}
