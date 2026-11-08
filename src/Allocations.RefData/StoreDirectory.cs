namespace Abc.Allocations.RefData;

/// <summary>
/// Reference data for stores, loaded from the nightly store feed.
/// Store codes are case-insensitive: the feed and the OTS screens do not agree on casing.
/// </summary>
public sealed class StoreDirectory
{
    private readonly Dictionary<string, Store> _stores;

    public StoreDirectory(IEnumerable<Store> stores)
    {
        _stores = new Dictionary<string, Store>(StringComparer.OrdinalIgnoreCase);
        foreach (var store in stores)
        {
            if (string.IsNullOrWhiteSpace(store.Code))
                throw new ArgumentException("store code is required", nameof(stores));
            if (store.Weight <= 0)
                throw new ArgumentException($"store {store.Code} must have a positive weight", nameof(stores));
            _stores[store.Code.Trim()] = store;
        }
    }

    public int Count => _stores.Count;

    /// <summary>Finds a store by code, or returns null when it is not known.</summary>
    public Store? Find(string code) =>
        _stores.TryGetValue(code.Trim(), out var store) ? store : null;

    /// <summary>Finds a store by code, or throws when it is not known.</summary>
    public Store Get(string code)
    {
        if (_stores.TryGetValue(code.Trim(), out var store))
            return store;

        throw new KeyNotFoundException($"unknown store code '{code}'");
    }

    /// <summary>Active stores only, in code order.</summary>
    public IReadOnlyList<Store> Active() =>
        _stores.Values.Where(s => s.Active).OrderBy(s => s.Code, StringComparer.OrdinalIgnoreCase).ToList();
}
