namespace Abc.Allocations.Ots;

/// <summary>Converts allocated units into whole packs for the warehouse pick list.</summary>
public static class PackRounder
{
    /// <summary>
    /// Number of packs to send for an allocation. Partial packs cannot be shipped, so this rounds up:
    /// a store never receives fewer units than it was allocated. Zero units means zero packs.
    /// </summary>
    public static int ToPacks(int units, int packSize)
    {
        if (packSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(packSize), "pack size must be positive");
        if (units < 0)
            throw new ArgumentOutOfRangeException(nameof(units), "units must be non-negative");
        if (units == 0)
            return 0;

        return (units + packSize - 1) / packSize;
    }

    /// <summary>Units actually shipped once the allocation is rounded to packs.</summary>
    public static int ShippedUnits(int units, int packSize) => ToPacks(units, packSize) * packSize;
}
