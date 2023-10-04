namespace SimpleTest.Utils;

public class WordComparer : IWordComparer
{
    public int Compare(string? x, string? y)
    {
        var comparison = string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        return comparison != 0 ? comparison : string.CompareOrdinal(x, y);
    }
}
