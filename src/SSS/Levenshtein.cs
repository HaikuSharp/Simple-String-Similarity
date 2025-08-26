using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

/// <summary>
/// Levenshtein edit distance normalized by max string length.
/// </summary>
public class Levenshtein : IStringDistance
{
    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static Levenshtein Default => field ??= new();

    /// <inheritdoc/>
    public double Distance(string s1, string s2) => Distance(s1, s2, int.MaxValue);

    /// <summary>
    /// Computes the Levenshtein distance with an early-exit limit.
    /// </summary>
    /// <param name="s1">First string.</param>
    /// <param name="s2">Second string.</param>
    /// <param name="limit">Early-exit threshold; if the minimal possible value exceeds it, returns 1.0.</param>
    /// <returns>Normalized distance in [0, 1].</returns>
    public static double Distance(string s1, string s2, int limit)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 0;
        if(s1.Length == 0) return 1.0;
        if(s2.Length == 0) return 1.0;

        int maxLen = Math.Max(s1.Length, s2.Length);
        int[] v0 = new int[s2.Length + 1];
        int[] v1 = new int[s2.Length + 1];

        for(int i = 0; i < v0.Length; i++) v0[i] = i;

        for(int i = 0; i < s1.Length; i++)
        {
            v1[0] = i + 1;
            int minv1 = v1[0];

            for(int j = 0; j < s2.Length; j++)
            {
                int cost = s1[i].Equals(s2[j]) ? 0 : 1;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
                minv1 = Math.Min(minv1, v1[j + 1]);
            }

            if(minv1 >= limit) return 1.0;
            (v0, v1) = (v1, v0);
        }

        return (double)v0[s2.Length] / maxLen;
    }
}
