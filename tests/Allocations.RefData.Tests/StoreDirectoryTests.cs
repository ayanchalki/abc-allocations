using Abc.Allocations.RefData;

namespace Abc.Allocations.RefData.Tests;

public class StoreDirectoryTests
{
    private static StoreDirectory Directory() => new([
        new Store("S01", "Leeds", 1),
        new Store("S02", "York", 2, Active: false),
    ]);

    [Fact]
    public void Finds_store_by_code()
    {
        Assert.Equal("Leeds", Directory().Get("S01").Name);
    }

    [Fact]
    public void Unknown_code_returns_null()
    {
        Assert.Null(Directory().Find("S99"));
    }

    [Fact]
    public void Active_excludes_inactive_stores()
    {
        Assert.Equal(["S01"], Directory().Active().Select(s => s.Code));
    }

    [Fact]
    public void Store_needs_a_positive_weight()
    {
        Assert.Throws<ArgumentException>(() => new StoreDirectory([new Store("S05", "Bath", 0)]));
    }
}
