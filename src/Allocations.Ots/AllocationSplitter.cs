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

        // Compute exact fractional shares, take floors, then distribute remaining units
        // to the largest fractional remainders (stable tie-break by store key).
        var exactShares = new Dictionary<string, decimal>(weights.Count);
        var allocations = new Dictionary<string, int>(weights.Count);

        var allocated = 0;
        foreach (var (store, weight) in weights)
        {
            var exact = units * weight / total;
            exactShares[store] = exact;

            var baseUnits = (int)decimal.Floor(exact);
            allocations[store] = baseUnits;
            allocated += baseUnits;
        }

        var remaining = units - allocated;
        if (remaining > 0)
        {
            foreach (var store in exactShares
                         .Select(kvp => new { Store = kvp.Key, Remainder = kvp.Value - decimal.Floor(kvp.Value) })
                         .OrderByDescending(x => x.Remainder)
                         .ThenBy(x => x.Store, StringComparer.Ordinal)
                         .Take(remaining)
                         .Select(x => x.Store))
            {
                allocations[store]++;
            }
        }

        return allocations;
    }
}
