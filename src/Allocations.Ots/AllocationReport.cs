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
            sb.Append(CultureInfo.InvariantCulture, $"{EscapeCsv(poNumber)},{EscapeCsv(line.StoreCode)},{EscapeCsv(line.Units)},{EscapeCsv(line.Packs)}\n");
        }
        return sb.ToString();
    }

    public static int TotalUnits(IEnumerable<AllocationLine> lines) => lines.Sum(l => l.Units);

    private static string EscapeCsv(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var needsQuotes = value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r');
        if (!needsQuotes)
        {
            return value;
        }

        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }

    private static string EscapeCsv(int value) => value.ToString(CultureInfo.InvariantCulture);
}
