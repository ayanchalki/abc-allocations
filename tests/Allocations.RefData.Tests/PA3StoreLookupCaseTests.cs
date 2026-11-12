using Abc.Allocations.RefData;
using Xunit;

namespace Abc.Allocations.RefData.Tests
{
    public class PA3StoreLookupCaseTests
    {
        [Fact]
        public void Store_lookup_accepts_codes_in_any_case()
        {
            var dir = new StoreDirectory(new[] {
                new Store("S01", "Leeds", 1),
                new Store("S02", "York", 1)
            });

            Assert.Equal(2, dir.Count);

            // lookups using lower-case codes should succeed
            var s1 = dir.Find("s01");
            Assert.NotNull(s1);
            Assert.Equal("S01", s1.Code);

            var s2 = dir.Get("s02");
            Assert.Equal("S02", s2.Code);
        }
    }
}
