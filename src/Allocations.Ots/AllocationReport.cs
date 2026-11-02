using System.Globalization;
using System.Text;

namespace Abc.Allocations.Ots;

/// <summary>The CSV export the OTS team sends to ADH API.</summary>
public static class AllocationReport
{
    public static string ToCsv(string poNumber, IEnumerable<AllocationLine> lines)
    {
        var sb = new StringBuilder("po,store,units,packs\n");
        foreach (var line in lines)
        {
            sb.Append(CultureInfo.InvariantCulture, $"{poNumber},{line.StoreCode},{line.Units},{line.Packs}\n");
        }
        return sb.ToString();
    }

    public static int TotalUnits(IEnumerable<AllocationLine> lines) => lines.Sum(l => l.Units);
}
