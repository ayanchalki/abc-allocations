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
        if (weights is null)
            throw new ArgumentNullException(nameof(weights));

        // Preserve existing contract: zero/negative total weight is rejected.
        var totalWeight = weights.Values.Sum();
        if (totalWeight <= 0)
            throw new ArgumentException("weights must sum to a positive number", nameof(weights));

        foreach (var (_, weight) in weights)
        {
            if (weight < 0)
                throw new ArgumentOutOfRangeException(nameof(weights), "weights must be non-negative");
        }

        // Hamilton / largest-remainder method:
        // 1) floor each exact share
        // 2) distribute remaining units to largest fractional remainders
        var allocations = new Dictionary<string, int>(weights.Count);
        var remainders = new List<(string Store, decimal FractionalPart)>(weights.Count);

        var allocated = 0;
        foreach (var (store, weight) in weights)
        {
            var exactShare = units * weight / totalWeight;
            var integerPart = (int)decimal.Floor(exactShare);

            allocations[store] = integerPart;
            allocated += integerPart;

            remainders.Add((store, exactShare - integerPart));
        }

        var remainderUnits = units - allocated;
        if (remainderUnits > 0)
        {
            // Deterministic tie-breaker: store id ascending.
            foreach (var (store, _) in remainders
                         .OrderByDescending(r => r.FractionalPart)
                         .ThenBy(r => r.Store)
                         .Take(remainderUnits))
            {
                allocations[store] = allocations[store] + 1;
            }
        }

        return allocations;
    }
}
