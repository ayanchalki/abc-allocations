using Abc.Allocations.RefData;

namespace Abc.Allocations.RefData.Tests;

public class PA3StoreLookupTests
{
    [Fact]
    public void Store_lookup_is_case_insensitive()
    {
        var dir = new StoreDirectory(new[] {
            new Store("S01", "Leeds", 1m),
        });

        // Expectation: lookup should ignore casing and find the store when given lower-case code
        var found = dir.Find("s01");
        Assert.NotNull(found);
        Assert.Equal("S01", found!.Code);

        // And Get should also succeed for lower-case input
        var gotten = dir.Get("s01");
        Assert.Equal("S01", gotten.Code);
    }
}
