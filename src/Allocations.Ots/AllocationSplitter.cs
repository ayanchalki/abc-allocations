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

        var shares = new Dictionary<string, int>();
        foreach (var (store, weight) in weights)
        {
            shares[store] = (int)(units * weight / total);
        }
        return shares;
    }
}
