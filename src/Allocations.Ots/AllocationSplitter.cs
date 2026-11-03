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

        // First pass: assign the integer part of each proportional share.
        var shares = new Dictionary<string, int>(weights.Count);
        var remainders = new List<(string Store, decimal Remainder)>(weights.Count);

        var assigned = 0;
        foreach (var (store, weight) in weights)
        {
            var exact = units * weight / total;
            var whole = (int)decimal.Floor(exact);
            shares[store] = whole;
            assigned += whole;
            remainders.Add((store, exact - whole));
        }

        // Second pass: distribute remaining units to the largest fractional remainders.
        var remaining = units - assigned;
        if (remaining > 0)
        {
            foreach (var (store, _) in remainders
                         .OrderByDescending(r => r.Remainder)
                         .ThenBy(r => r.Store)
                         .Take(remaining))
            {
                shares[store]++;
            }
        }

        return shares;
    }
}
