using Abc.Allocations.RefData;

namespace Abc.Allocations.RefData.Tests;

public class PA3StoreDirectoryCaseInsensitiveTests
{
    [Fact]
    public void Finds_store_by_code_case_insensitive()
    {
        var dir = new StoreDirectory([
            new Store("S01", "Leeds", 1),
        ]);

        // Expectation: lookup should be case-insensitive and find the store regardless of casing.
        Assert.Equal("Leeds", dir.Get("s01").Name);
    }
}
