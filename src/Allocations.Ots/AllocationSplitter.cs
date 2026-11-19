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
        if (units == 0)
            return weights.Keys.ToDictionary(k => k, _ => 0);

        var total = weights.Values.Sum();
        if (total <= 0)
            throw new ArgumentException("weights must sum to a positive number", nameof(weights));

        var allocations = new Dictionary<string, int>(weights.Count);
        var fractionalParts = new List<(string Store, decimal Fraction, int Index)>(weights.Count);

        var index = 0;
        var allocated = 0;
        foreach (var (store, weight) in weights)
        {
            var rawShare = units * weight / total;
            var baseUnits = (int)decimal.Floor(rawShare);
            allocations[store] = baseUnits;
            allocated += baseUnits;

            fractionalParts.Add((store, rawShare - baseUnits, index));
            index++;
        }

        var remainder = units - allocated;
        if (remainder > 0)
        {
            foreach (var (store, _, _) in fractionalParts
                         .OrderByDescending(x => x.Fraction)
                         .ThenBy(x => x.Index)
                         .Take(remainder))
            {
                allocations[store]++;
            }
        }

        return allocations;
    }
}
