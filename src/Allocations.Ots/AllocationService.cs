using Abc.Allocations.RefData;

namespace Abc.Allocations.Ots;

/// <summary>One store's line on an allocation.</summary>
public sealed record AllocationLine(string StoreCode, int Units, int Packs);

/// <summary>Builds the allocation for a purchase order across the active stores.</summary>
public sealed class AllocationService(StoreDirectory stores)
{
    public IReadOnlyList<AllocationLine> Allocate(string poNumber, int units, int packSize)
    {
        if (string.IsNullOrWhiteSpace(poNumber))
            throw new ArgumentException("PO number is required", nameof(poNumber));
        var active = stores.Active();
        if (active.Count == 0)
            throw new InvalidOperationException("no active stores to allocate to");

        var shares = AllocationSplitter.Allocate(units, active.ToDictionary(s => s.Code, s => s.Weight));
        return active
            .Select(s => new AllocationLine(s.Code, shares[s.Code], PackRounder.ToPacks(shares[s.Code], packSize)))
            .ToList();
    }

    /// <summary>Allocation for a hand-picked list of store codes, as typed on the OTS screen.</summary>
    public IReadOnlyList<AllocationLine> AllocateTo(IEnumerable<string> storeCodes, int units, int packSize)
    {
        var chosen = storeCodes.Select(stores.Get).ToList();
        var shares = AllocationSplitter.Allocate(units, chosen.ToDictionary(s => s.Code, s => s.Weight));
        return chosen
            .Select(s => new AllocationLine(s.Code, shares[s.Code], PackRounder.ToPacks(shares[s.Code], packSize)))
            .ToList();
    }
}
