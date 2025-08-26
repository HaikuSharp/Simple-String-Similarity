using SSS.Abstraction;
using System;
using System.Linq;

namespace SSS;

/// <summary>
/// Metric based on LCS similarity: 1 - LCS(s1, s2) / max(|s1|, |s2|).
/// </summary>
public class MetricLcs : IStringDistance
{
    /// <summary>
    /// Gets a shared default instance.
    /// </summary>
    public static MetricLcs Default => field ??= new();

    /// <inheritdoc/>
    public double Distance(string s1, string s2)
    {
        InternalNullStringsHelper.ThrowIfArgumentsIsNull(s1, s2);

        if(s1.SequenceEqual(s2)) return 0;

        int length = Math.Max(s1.Length, s2.Length);

        return length is 0 ? 0.0 : 1.0 - (1.0 * LongestCommonSubsequence.Length(s1, s2)) / length;
    }

}
