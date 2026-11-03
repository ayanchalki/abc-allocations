namespace Abc.Allocations.Ots;

/// <summary>Splits a purchase-order quantity across stores in proportion to their weights.</summary>
public static class AllocationSplitter
{
    /// <summary>
    /// Returns whole units per store, proportional to each store's weight.
    /// The shares always add up to <paramref name="units"/>: no unit of a PO may be lost.
    /// </summary>
    public static IReadOnlyDictionary<string, int> Allocate(int units, IReadOnlyDictionary<string, decimal> weights)
    {
        if (units < 0)
            throw new ArgumentOutOfRangeException(nameof(units), "units must be non-negative");
        var total = weights.Values.Sum();
        if (total <= 0)
            throw new ArgumentException("weights must sum to a positive number", nameof(weights));

        // Compute exact shares, take floors as base allocations, then distribute leftovers
        // to the largest fractional remainders (deterministic tie-break by store key).
        var baseAllocations = new Dictionary<string, int>(weights.Count);
        var remainders = new List<(string Store, decimal Remainder)>(weights.Count);

        var allocated = 0;
        foreach (var (store, weight) in weights)
        {
            var exact = units * weight / total;
            var floor = (int)decimal.Floor(exact);
            baseAllocations[store] = floor;
            allocated += floor;
            remainders.Add((store, exact - floor));
        }

        var leftover = units - allocated;
        if (leftover > 0)
        {
            foreach (var (store, _) in remainders
                         .OrderByDescending(r => r.Remainder)
                         .ThenBy(r => r.Store, StringComparer.Ordinal)
                         .Take(leftover))
            {
                baseAllocations[store]++;
            }
        }

        return baseAllocations;
    }
}
