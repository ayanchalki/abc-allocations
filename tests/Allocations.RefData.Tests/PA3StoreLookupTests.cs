using Abc.Allocations.RefData;
using Xunit;

namespace Abc.Allocations.RefData.Tests;

public class PA3StoreLookupTests
{
    [Fact]
    public void Store_lookup_is_case_insensitive()
    {
        var dir = new StoreDirectory(new[] {
            new Store("S01", "Leeds", 1m),
        });

        // Should find the store even when the code is provided in lower-case
        var store = dir.Get("s01");

        Assert.Equal("S01", store.Code);
        Assert.Equal("Leeds", store.Name);
    }
}
