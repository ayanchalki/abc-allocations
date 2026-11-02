namespace Abc.Allocations.RefData;

/// <summary>A store that can receive allocations.</summary>
/// <param name="Code">Store code, e.g. "S01". Codes are case-insensitive.</param>
/// <param name="Name">Display name.</param>
/// <param name="Weight">Relative share of an allocation; must be positive.</param>
/// <param name="Active">Inactive stores never receive units.</param>
public sealed record Store(string Code, string Name, decimal Weight, bool Active = true);
