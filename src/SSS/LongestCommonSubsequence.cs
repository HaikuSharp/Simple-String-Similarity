using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

/// <summary>
/// Distance based on the length of the Longest Common Subsequence (LCS).
/// </summary>
public class LongestCommonSubsequence : IStringDistance
{
    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static LongestCommonSubsequence Default => field ??= new();

    /// <inheritdoc/>
    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);
        if(s1.SequenceEqual(s2)) return 0;
        int maxLen = Math.Max(s1.Length, s2.Length);
        return (s1.Length + s2.Length - 2 * InternalLength(s1, s2)) / (double)maxLen;
    }

    /// <summary>
    /// Returns the LCS length for two strings.
    /// </summary>
    public static int Length(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);
        return InternalLength(s1, s2);
    }

    private static int InternalLength(string s1, string s2)
    {
        int length1 = s1.Length;
        int length2 = s2.Length;

        int[,] c = new int[length1 + 1, length2 + 1];

        for(int i = 0; i <= length1; i++) c[i, 0] = 0;
        for(int j = 0; j <= length2; j++) c[0, j] = 0;
        for(int i = 1; i <= length1; i++)for(int j = 1; j <= length2; j++) c[i, j] = s1[i - 1].Equals(s2[j - 1]) ? c[i - 1, j - 1] + 1 : Math.Max(c[i, j - 1], c[i - 1, j]);

        return c[length1, length2];
    }
}