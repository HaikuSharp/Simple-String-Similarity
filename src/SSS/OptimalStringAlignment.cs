using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

/// <summary>
/// Optimal String Alignment distance (restricted Damerau–Levenshtein), normalized by max length.
/// </summary>
public sealed class OptimalStringAlignment : IStringDistance
{
    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static OptimalStringAlignment Default => field ??= new();

    /// <inheritdoc/>
    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 0;

        int n = s1.Length, m = s2.Length;
        int maxLen = Math.Max(n, m);

        if(n == 0) return 1.0;
        if(m == 0) return 1.0;

        int[,] d = new int[n + 2, m + 2];

        for(int i = 0; i <= n; i++) d[i, 0] = i;
        for(int j = 0; j <= m; j++) d[0, j] = j;

        int cost;

        for(int i = 1; i <= n; i++) for(int j = 1; j <= m; j++)
        {
            cost = 1;

            if(s1[i - 1].Equals(s2[j - 1])) cost = 0;

            d[i, j] = Min( d[i - 1, j - 1] + cost, d[i, j - 1] + 1, d[i - 1, j] + 1);

            if(i > 1 && j > 1 && s1[i - 1].Equals(s2[j - 2]) && s1[i - 2].Equals(s2[j - 1])) d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
        }

        return (double)d[n, m] / maxLen;
    }

    private static int Min(int a, int b, int c) => Math.Min(a, Math.Min(b, c));
}
